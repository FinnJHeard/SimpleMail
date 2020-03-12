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
        public string OwnerID { get; set; }

        public string Forname { get; set; }
        public string Surname { get; set; }
        public string ContactEmail { get; set; }
        public string Description { get; set; }

        [ManyToOne]
        public UserTable UserTable { get; set; }
    }
}
