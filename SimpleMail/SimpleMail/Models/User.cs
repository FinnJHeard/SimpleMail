using System;

namespace SimpleMail.Models
{
    public class User
    {
        //public int id { get; set; }
        public string email { get; set; }
        public string password { get; set; }

        public User(String email, String password)
        {
            this.email = email;
            this.password = password;
            //this.id = id;
        }
    }
}