using System;
using MimeKit;
using MailKit;
using MailKit.Net.Smtp;
using MailKit.Net.Pop3;
using SimpleMail.Services;
using System.Threading.Tasks;
using MailKit.Net.Imap;

namespace SimpleMail.Models
{
    public class User
    {
        //public int id { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public UserTable userDB { get; set; }
        //public bool valid { get; set; }

        async public static Task<User> authenticate(String email, String password)
        {
            SmtpClient sendingClient = new SmtpClient();
            Pop3Client receivingClient = new Pop3Client();

            await sendingClient.ConnectAsync("smtp.gmail.com", 465, true);
            await sendingClient.AuthenticateAsync(StrUtil.removeDomain(email), password);
            await receivingClient.ConnectAsync("pop.gmail.com", 995, true);
            await receivingClient.AuthenticateAsync(StrUtil.removeDomain(email), password);

            sendingClient.Disconnect(true);
            receivingClient.Disconnect(true);

            return new User(email, password);
        }

        async public Task<SmtpClient> getSendingClient()
        {
            SmtpClient sendingClient = new SmtpClient();

            await sendingClient.ConnectAsync("smtp.gmail.com", 465, true);
            await sendingClient.AuthenticateAsync(StrUtil.removeDomain(email), password);

            return sendingClient;
        }

        async public Task<ImapClient> getReceivingClient()
        {
            ImapClient receivingClient = new ImapClient();

            await receivingClient.ConnectAsync("imap.gmail.com", 993, true);
            await receivingClient.AuthenticateAsync(StrUtil.removeDomain(email), password);

            return receivingClient;
        }

        //public async Task<async> User(String email, String password)
        public User(String email, String password)
        {
            //this.id = id;
            this.email = email;
            this.password = password;
            //await this.authenticate();

        }

    }
}