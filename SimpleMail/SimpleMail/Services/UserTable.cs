using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace SimpleMail.Services
{
    public class UserTable
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string UserEmail { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<ContactTable> ContactTable { get; set; }
    }
}
