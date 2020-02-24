using System;

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

    }
}