using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiSteam2
{
    public class SteamUser
    {
        private long _ID;
        private string _Username;
        private string _Password;
        private string _Nickname;
        private long _Order;

        public long ID { get { return _ID; } }

        public string Username
        {
            get
            {
                return _Username;
            }
            set
            {
                _Username = value;
                Update("username", value);
            }
        }

        public string Password
        {
            get { return _Password; }
            set
            {
                _Password = value;
                Update("password", value);
            }
        }

        public string Nickname
        {
            get { return _Nickname; }
            set
            {
                _Nickname = value;
                Update("nickname", value);
            }
        }

        public long Order
        {
            get { return _Order; }
            set
            {
                _Order = value;
                Update("orderindex", value.ToString());
            }
        }

        public SteamUser(long ID, string Username, string Password, string Nickname)
        {
            _ID = ID;
            _Username = Username;
            _Password = Password;
            _Nickname = Nickname;
        }

        /// <summary>
        /// Inserts a new user into the database.
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="Username"></param>
        /// <param name="Password"></param>
        /// <param name="Nickname"></param>
        /// <returns></returns>
        public static SteamUser CreateNew(string Username, string Password, string Nickname)
        {
            //calculate the order (highest orderindex + 1 or 0 if first)
            long order = 0;

            object orderResult = Database.Scalar("SELECT MAX(orderindex) FROM {0}", Database.USER_TABLE);
            if(orderResult.GetType() != typeof(DBNull))
            {
                order = (long)orderResult + 1;
            }

            //actually insert the record
            Database.NonQuery("INSERT INTO {0}(id, username, password, nickname, orderindex) VALUES(NULL, \"{1}\",\"{2}\",\"{3}\",{4});", Database.USER_TABLE, Username, Password, Nickname, order);
            
            var id = Database.Scalar("SELECT MAX(id) FROM {0}", Database.USER_TABLE);
            return new SteamUser((long)id, Username, Password, Nickname);
        }

        /// <summary>
        /// Removes a user from the database.
        /// </summary>
        /// <param name="id"></param>
        public static void Remove(long id)
        {
            Database.NonQuery("DELETE FROM users WHERE id={0}", id);
        }
        
        public void Update(string field, string value)
        {
            Database.NonQuery("UPDATE users SET {0}=\"{1}\" WHERE id={2}", field, value, ID);
        }
    }
}
