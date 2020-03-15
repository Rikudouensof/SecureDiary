using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecureDiary.Model
{
    public class Account
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        [Unique]
        public string UserName { get; set; }
        public string Password { get; set; }
        

        public string Hint { get; set; }
    }
}
