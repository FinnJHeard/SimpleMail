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
        public string senderName { get; set; }
        public string recipientAddress { get; set; }
        public string recipientName { get; set; }
        public string subject { get; set; }
        public string body { get; set; }
        public string bodyHtml { get; set; }
        //public bool sent { get; set; }

        // For SENDING an email
        public Email(string senderAddress)
        {
            this.senderAddress = senderAddress;
            senderName = null;
            recipientAddress = null;
            recipientName = null;
            subject = " ";
            body = " ";
            bodyHtml = " ";
            //sent = false;
        }

        // For displaying a RECEIVED email etc.
        public Email(string content, string subject)
        {
            this.subject = subject;
            body = content;
        }

        async public void sendMessage(User user)
        {
            //Pulled from https://github.com/jstedfast/MailKit for the email API

            var message = new MimeMessage();

            if (senderName != null)
                message.From.Add(new MailboxAddress(senderName, senderAddress));
            else
                message.From.Add(new MailboxAddress(senderAddress));

            if (recipientName != null)
                message.To.Add(new MailboxAddress(recipientName, recipientAddress));
            else
                message.To.Add(new MailboxAddress(recipientAddress));

            if (subject == null)
                message.Subject = " ";
            else
                message.Subject = subject;

            if (body == null)
            {
                message.Body = new TextPart("Plain")
                {
                    Text = " "
                };
            }
            else
            {
                message.Body = new TextPart("Plain")
                {
                    Text = body
                };
            }

            Console.WriteLine("message created"); //test

           
            using (var client = await user.getSendingClient())
            {
                // For demo-purposes, accept all SSL certificates (in case the server supports STARTTLS)
                //client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                // Note: only needed if the SMTP server requires authentication
                //client.Authenticate("simple123mail", "cyaggeoyfpfquwiq"); - wuth a mobile password works
                //client.Authenticate("simple123mail", "ipgroup123"); - doesn't work with normal password for yahoo

                client.Send(message);

                Console.WriteLine("message sent"); //test

                client.Disconnect(true);

                Console.WriteLine("client disconnected"); //test
            }
        }

    }
}