using System;
using System.IO;
using System.Data.SQLite;
using System.Windows.Forms;
using System.Collections.Generic;

namespace MultiSteam
{
    public static class Database
    {
        public static readonly string USER_TABLE = "users";
        public static readonly string CONFIG_TABLE = "config";
        static readonly string FILENAME = "users.sqlite";
        static readonly string PASSWORD = "CHANGETHISBEFORECOMPILING";

        static SQLiteConnection Connection;
        public static bool Connected { get; private set; }

        /// <summary>
        /// Initalizes and opens the database connection.
        /// </summary>
        public static void Init()
        {
            Application.ApplicationExit += (s, e) => Disconnect();
            
            if (!File.Exists(FILENAME)) //on first launch, create file, tables and encrypt database
            {
                SQLiteConnection.CreateFile(FILENAME);
                Connection = new SQLiteConnection(string.Format("Data Source={0};Version=3;", FILENAME));
                Connection.SetPassword(PASSWORD);
                Connection.Open();
                CreateTables();
            }
            else
            {
                Connection = new SQLiteConnection(string.Format("Data Source={0};Version=3;Password={1}", FILENAME, PASSWORD));
                Connection.Open();
            }
            
            Connected = true;
        }

        public static void Disconnect()
        {
            Connection.Close();
            Connection.Dispose();
            Connection = null;
            Connected = false;
        }

        #region pass through
        public static SQLiteDataReader Query(string SQL, params object[] args)
        {
            if (!Connected) throw new Exception("Not connected to the database.");

            var command = new SQLiteCommand(string.Format(SQL, args), Connection);
            return command.ExecuteReader();
        }

        public static void NonQuery(string SQL, params object[] args)
        {
            if (!Connected) throw new Exception("Not connected to the database.");
            
            var command = new SQLiteCommand(string.Format(SQL, args), Connection);
            command.ExecuteNonQuery();
        }

        public static object Scalar(string SQL, params object[] args)
        {
            if (!Connected) throw new Exception("Not connected to the database.");

            var command = new SQLiteCommand(string.Format(SQL, args), Connection);
            return command.ExecuteScalar();
        }
        #endregion

        static void CreateTables()
        {
            var cmdCreateUsersTable = new SQLiteCommand(
                "CREATE TABLE " + USER_TABLE + " " +
                "(" +
                    "id INTEGER PRIMARY KEY," +
                    "username VARCHAR(64)," +
                    "password VARCHAR(64)," +
                    "nickname VARCHAR(64)," +
                    "orderindex INTEGER" +
                ");",
                Connection);

            cmdCreateUsersTable.ExecuteNonQuery();

            var cmdCreateConfigTable = new SQLiteCommand(
                "CREATE TABLE " + CONFIG_TABLE + " " + 
                "(" +
                    "id INTEGER PRIMARY KEY," +
                    "name VARCHAR(64)," +
                    "value VARCHAR(64)" +
                ")"
                , Connection);

            cmdCreateConfigTable.ExecuteNonQuery();
        }
    }
}
