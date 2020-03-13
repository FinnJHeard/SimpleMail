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

        User current_user;

        public InboxPage(User current_user) 
        {
            InitializeComponent();
            this.current_user = current_user;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            emailsListView.ItemsSource = Get_Emails();

        }

        private List<String> Get_Emails()
        {
            int maxCount = 10;

            using (var emailClient = current_user.receivingClient)
            {

                List<Email> emails = new List<Email>();
                List<String> emailContents = new List<String>();
                for (int i = 0; i < maxCount && i < emailClient.Count; i++)
                {
                    var message = emailClient.GetMessage(i);
                    var emailMessage = new Email(message.TextBody, message.Subject);

                    //Figure out how to pull the addresses from the API
                    emailMessage.recipientAddress = null;  
                    emailMessage.senderAddress = null;

                    //Only displaying the body of the email currently (UI input pls)
                    //emails.Add(emailMessage);
                    emailContents.Add(emailMessage.body);
                }
                //return emails;
                return emailContents;
            }

        }


    }
}