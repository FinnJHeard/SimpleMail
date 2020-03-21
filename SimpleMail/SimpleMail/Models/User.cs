using System;
using MimeKit;
using MailKit;
using MailKit.Net.Smtp;
using MailKit.Net.Pop3;
using SimpleMail.Services;
using System.Threading.Tasks;

namespace SimpleMail.Models
{
    public class User
    {
        //public int id { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public SmtpClient sendingClient { get; set; }
        public Pop3Client receivingClient { get; set; }
        //public bool valid { get; set; }

        async public static Task<User> authenticate(String email, String password)
        {
            SmtpClient sendingClient = new SmtpClient();
            Pop3Client receivingClient = new Pop3Client();

            await sendingClient.ConnectAsync("smtp.gmail.com", 465, true);
            await sendingClient.AuthenticateAsync(StrUtil.removeDomain(email), password);
            await receivingClient.ConnectAsync("pop.gmail.com", 995, true);
            await receivingClient.AuthenticateAsync(StrUtil.removeDomain(email), password);

            return new User(email, password, sendingClient, receivingClient);
        }

        //public async Task<async> User(String email, String password)
        public User(String email, String password, SmtpClient sendingClient, Pop3Client receivingClient)
        {
            //this.id = id;
            this.email = email;
            this.password = password;
            this.sendingClient = sendingClient;
            this.receivingClient = receivingClient;
            //await this.authenticate();

        }

    }
}