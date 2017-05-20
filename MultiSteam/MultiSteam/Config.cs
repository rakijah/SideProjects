using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiSteam2
{
    public static class Config
    {
        private static Dictionary<string, object> Values = new Dictionary<string, object>();

        public static void Init()
        {
            Values.Clear();
            string sql = "SELECT * FROM " + Database.CONFIG_TABLE;
            var reader = Database.Query(sql);
            while (reader.Read())
            {
                Values.Add(reader.GetString(1), reader[2]);
            }
        }

        #region getters
        public static object Get(string Name)
        {
            return Values[Name];
        }

        /// <summary>
        /// Gets the requested value as a string.
        /// If value is not present, returns empty string ("")
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public static string GetString(string Name)
        {
            if(!Exists(Name))
                return "";

            return Values[Name].ToString();
        }

        public static long GetLong(string Name)
        {
            return Convert.ToInt64(Values[Name]);
        }
        #endregion

        public static bool Exists(string Name)
        {
            return Values.ContainsKey(Name);
        }

        public static void Set(string Name, string Value)
        {
            if(Values.ContainsKey(Name))
            {
                Values[Name] = Value;
                string sql = string.Format("UPDATE {0} SET value=\"{1}\" WHERE name=\"{2}\"", Database.CONFIG_TABLE, Value, Name);
                Database.NonQuery(sql);
            }
            else
            {
                Values.Add(Name, Value);
                string sql = string.Format("INSERT INTO {0}(name, value) VALUES(\"{1}\", \"{2}\")", Database.CONFIG_TABLE, Name, Value);
                Database.NonQuery(sql);
            }
        }
    }
}
