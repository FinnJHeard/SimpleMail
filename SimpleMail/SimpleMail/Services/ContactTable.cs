using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace SimpleMail.Services
{
    public class ContactTable
    {
        [PrimaryKey, AutoIncrement]
        public int ContactID { get; set; }

        [ForeignKey(typeof(UserTable))]
        public int OwnerID { get; set; }

        public string Forename { get; set; }
        public string Surname { get; set; }
        public string ContactEmail { get; set; }

        [ManyToOne]
        public UserTable UserTable { get; set; }
    }
}
