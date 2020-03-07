using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MimeKit;
using MailKit;
using MailKit.Net.Smtp;
using MailKit.Net.Pop3;
using SimpleMail.Models;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace SimpleMail.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InboxPage : ContentPage
    {

        public InboxPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            emailsListView.ItemsSource = get_Emails();

        }

        private List<String> get_Emails()
        {
            int maxCount = 10;

            using (var emailClient = new Pop3Client())
            {
                emailClient.Connect("pop.gmail.com", 995, true);
                //emailClient.AuthenticationMechanisms.Remove("XOAUTH2");
                emailClient.Authenticate("simple321mail", "ipgroup123");

                List<Email> emails = new List<Email>();
                List<String> emailContents = new List<String>();
                for (int i = 0; i < maxCount && i < emailClient.Count; i++)
                {
                    var message = emailClient.GetMessage(i);
                    var emailMessage = new Email(!string.IsNullOrEmpty(message.HtmlBody) ? message.HtmlBody : message.TextBody, message.Subject);
                    
                    //Figure out how to pull the addresses from the API
                    emailMessage.recipientAddress = null;  
                    emailMessage.senderAddress = null;

                    //emails.Add(emailMessage);
                    emailContents.Add(emailMessage.body);
                }
                //return emails;
                return emailContents;
            }

        }



    }
}