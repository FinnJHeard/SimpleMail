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
        Email selectedEmail = null;
        Email tappedEmail = null;

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

        private void Selected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            selectedEmail = e.SelectedItem as Email;
        }

        async private void Tapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            tappedEmail = e.Item as Email;
            if(tappedEmail != null && tappedEmail == selectedEmail)
            {
                await Navigation.PushAsync(new ViewEmailPage(current_user, tappedEmail));
            }
        }

        private List<Email> Get_Emails()
        {
            int maxCount = 10;

            using (var emailClient = current_user.receivingClient)
            {

                List<Email> emails = new List<Email>();
                //List<String> emailContents = new List<String>();
                for (int i = 0; i < maxCount && i < emailClient.Count; i++)  //CRASHS if view emails is used twice
                {
                    var message = emailClient.GetMessage(i);
                    var emailMessage = new Email(message.TextBody, message.Subject);

                    //Gets only the FIRST email ADDRESS in the list of senders and recievers
                    foreach (var mailbox in message.From.Mailboxes)
                    {
                        emailMessage.senderAddress = mailbox.Address; //mailbox.Name for sender's name
                        break; //loop and store as list for multiple addresses?
                    }
                    foreach (var mailbox in message.To.Mailboxes)
                    {
                        emailMessage.recipientAddress = mailbox.Address; //mailbox.Name for recipient name
                        break;
                    } 
                    

                    //Only displaying the body of the email currently (UI input pls)
                    emails.Add(emailMessage);
                    //emailContents.Add(emailMessage.body);
                }
                return emails;
                //return emailContents;
            }

        }


    }
}