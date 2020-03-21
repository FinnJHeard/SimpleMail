using System;
using MimeKit;
using MailKit;
using MailKit.Net.Smtp;

namespace SimpleMail.Models
{
    public class Email
    {

        //public string id { get; set; }
        public string senderAddress { get; set; }
        public string recipientAddress { get; set; }
        public string subject { get; set; }
        public string body { get; set; }
        public bool sent { get; set; }

        public Email(string senderAddress)
        {
            this.senderAddress = senderAddress;
            recipientAddress = "";
            subject = "";
            body = "";
            sent = false;
        }

        public Email(string content, string subject)
        {
            this.subject = subject;
            body = content;
        }

        public void sendMessage(User user)
        {
            //Pulled from https://github.com/jstedfast/MailKit for the email API

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("John", senderAddress));
            message.To.Add(new MailboxAddress("Mrs. Chanandler Bong", recipientAddress));
            message.Subject = "Blank";
            message.Body = new TextPart("Plain")
            {
                Text = body
            };

            Console.WriteLine("message created");

            using (var client = user.sendingClient)
            {
                // For demo-purposes, accept all SSL certificates (in case the server supports STARTTLS)
                //client.ServerCertificateValidationCallback = (s, c, h, e) => true;


                // Note: only needed if the SMTP server requires authentication
                //client.Authenticate("simple123mail", "cyaggeoyfpfquwiq"); - wuth a mobile password works
                //client.Authenticate("simple123mail", "ipgroup123"); - doesn't work with normal password for yahoo

                client.Send(message);

                Console.WriteLine("message sent");

                //client.Disconnect(true);

                //Console.WriteLine("client disconnected");
            }
        }

    }
}