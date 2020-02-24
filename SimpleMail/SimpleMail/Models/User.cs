using System;

namespace SimpleMail.Models
{
    public class User
    {
        //public int id { get; set; }
        public string email { get; set; }

        public User(String Email)
        {
            this.email = Email;
            //this.id = id;
        }
    }
}