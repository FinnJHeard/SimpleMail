using System;
using MimeKit;
using MailKit;
using MailKit.Net.Smtp;
using MailKit.Net.Pop3;
using SimpleMail.Services;

namespace SimpleMail.Models
{
    public class User
    {
        //public int id { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public SmtpClient sendingClient { get; set; }
        public Pop3Client receivingClient { get; set; }
        public bool valid { get; set; }

        public async void authenticate()
        {
            await sendingClient.ConnectAsync("smtp.gmail.com", 465, true);
            await sendingClient.AuthenticateAsync(StrUtil.removeDomain(email), password);
            await receivingClient.ConnectAsync("pop.gmail.com", 995, true);
            await receivingClient.AuthenticateAsync(StrUtil.removeDomain(email), password);
        }

        public async User(String email, String password)
        {
            valid = false;
            this.email = email;
            this.password = password;
            sendingClient = new SmtpClient();
            receivingClient = new Pop3Client();
            await this.authenticate();
            valid = true;
            //this.id = id;
        }

    }
}