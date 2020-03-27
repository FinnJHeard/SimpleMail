using System;
using MimeKit;
using MailKit;
using MailKit.Net.Smtp;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace SimpleMail.Models
{
    public class Email
    {

        //public string id { get; set; }
        public string senderAddress { get; set; }
        public string senderName { get; set; }
        public string recipientAddress { get; set; }
        public List<string> recipientAddresses { get; set; }
        public string recipientName { get; set; }
        public string subject { get; set; }
        public string body { get; set; }
        public string bodyHtml { get; set; }
        //public bool sent { get; set; }
        public string date { get; set;  }
        public bool loading { get; set; }
        public MimeMessage message { get; set; }

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
            loading = true;
            if (subject == null)
            {
                this.subject = "(No Subject)";
            }
            else if (subject == "")
            {
                this.subject = "(No Subject)";
            }
            else
            {
                this.subject = subject;
            }
            
            if(content == null)
            {
                body = "(No Body)";
            }
            else if (content == "")
            {
                body = "(No Body)";
            }
            else
            {
                body = content;
            }
        }

        public Email(bool loading)
        {
            this.loading = loading;
        }

        async public void sendMessage(User user)
        {
            //Pulled from https://github.com/jstedfast/MailKit for the email API

            var message = new MimeMessage();

            if (senderName != null)
                message.From.Add(new MailboxAddress(senderName, senderAddress));
            else
                message.From.Add(new MailboxAddress(senderAddress));

            if (recipientAddresses != null)
            {
                if (recipientAddresses.Count > 0)
                {
                    for (int i = 0; i < recipientAddresses.Count; i++)
                    {
                        message.To.Add(new MailboxAddress(recipientAddresses[i]));
                    }
                    Console.WriteLine("Sending to " + recipientAddresses.Count + " added recipientAddresses."); //test
                }
            }
            else
            {
                Console.WriteLine("Sending to the recipientAddress."); //test
                message.To.Add(new MailboxAddress(recipientAddress));
            }

            //TODO: make it work with recipient names!
            /*
            if (recipientName != null)
                message.To.Add(new MailboxAddress(recipientName, recipientAddress));
            else
                message.To.Add(new MailboxAddress(recipientAddress));
            */

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

        // The current object (i.e. 'this') is the reply email. 'message' is the previous email that this is in reply to.
        async public void sendReply(User user, MimeMessage message)
        {
            var reply = new MimeMessage();

            string textbody;

            if (body == null)
            {
                textbody = "(Blank)";
            }
            else
            {
                textbody = body;
            }

            // reply to the sender of the message
            if(message.ReplyTo == null)
            {
                reply.To.AddRange(message.From);
                reply.From.Add(message.Sender);
            }
            else if (message.ReplyTo.Count > 0)
            {
                reply.To.AddRange(message.ReplyTo);
                reply.From.AddRange(message.From);
            }
            else if (message.From.Count > 0)
            {
                reply.To.AddRange(message.From);
                reply.From.AddRange(message.From);
            }
            else if (message.Sender != null)
            {
                reply.To.Add(message.Sender);
                reply.From.Add(message.Sender);
            }
            else
            {
                Console.WriteLine("Email -> sendReply -> sender/recipients -> else - should not be called"); //test
                reply.To.Add(new MailboxAddress("simple321mail@gmail.com"));
                reply.From.Add(new MailboxAddress("simple321mail@gmail.com"));
            }

            // set the reply subject
            if (!message.Subject.StartsWith("Re:", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Message subject: '" + message.Subject + "'."); //test
                String subject = "Re: " + message.Subject;
                reply.Subject = subject;
            }
            else
            {
                reply.Subject = message.Subject;
            }

            // construct the In-Reply-To and References headers
            if (!string.IsNullOrEmpty(message.MessageId))
            {
                reply.InReplyTo = message.MessageId;
                foreach (var id in message.References)
                    reply.References.Add(id);
                reply.References.Add(message.MessageId);
            }


            // quote the original message text
            using (var quoted = new StringWriter())
            {
                var sender = message.Sender ?? message.From.Mailboxes.FirstOrDefault();

                quoted.WriteLine("On {0}, {1} wrote:", message.Date.ToString("f"), !string.IsNullOrEmpty(sender.Name) ? sender.Name : sender.Address);
                using (var reader = new StringReader(message.TextBody))
                {
                    string line;

                    while ((line = reader.ReadLine()) != null)
                    {
                        quoted.Write("> ");
                        quoted.WriteLine(line);
                    }
                }

                reply.Body = new TextPart("plain")
                {
                    Text = textbody + "\n\n" + quoted.ToString()
                };
            }


            using (var client = await user.getSendingClient())
            {
                // For demo-purposes, accept all SSL certificates (in case the server supports STARTTLS)
                //client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                // Note: only needed if the SMTP server requires authentication
                //client.Authenticate("simple123mail", "cyaggeoyfpfquwiq"); - wuth a mobile password works
                //client.Authenticate("simple123mail", "ipgroup123"); - doesn't work with normal password for yahoo

                client.Send(reply);

                Console.WriteLine("reply sent"); //test

                client.Disconnect(true);

                Console.WriteLine("client disconnected"); //test
            }

            //return reply;
        }

    }
}