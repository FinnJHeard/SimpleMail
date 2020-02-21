using System;

namespace SimpleMail.Models
{
    public class User
    {
        //public int Id { get; set; }
        public string Email { get; set; }

        public User(String Email)
        {
            this.Email = Email;
            //this.Id = Id;
        }
    }
}