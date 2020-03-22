using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SimpleMail.Models;
using SimpleMail.Services;

namespace SimpleMail.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SendEmail : ContentPage
    {
        User current_user;
        Email emailToSend;

        public SendEmail(User current_user)
        {
            this.current_user = current_user;
            emailToSend = new Email(current_user.email);
            InitializeComponent();
        }

        async void Back_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        async void Send_Clicked(object sender, EventArgs e)
        {
            //Send the email
            Console.WriteLine("Send button clicked"); //test

            if (await ValidateFields())
            {
                emailToSend.recipientAddress = Recipient.Text;
                emailToSend.body = Body.Text;
                emailToSend.subject = Subject.Text;

                try
                {
                    emailToSend.sendMessage(current_user);

                    Console.WriteLine("Email sent"); //test

                    //Woosh sound here

                    await DisplayAlert("Success", "Email sent successfully", "OK");

                    await Navigation.PopAsync();
                    Console.WriteLine("Gone back a page"); //test

                }
                catch (Exception exception)
                {
                    await DisplayAlert("Failed", "Email ", "OK");
                }
            }

        }

        async Task<bool> ValidateFields()
        {
            bool willSend = true;

            if (Recipient.Text == null)
            {
                willSend = false;
                await DisplayAlert("Recipient blank", "Enter a valid recipient email address.", "OK");
            }

            if (willSend)
            {
                if (!await StrUtil.ValidateAddress(Recipient.Text))
                {
                    willSend = false;
                    await DisplayAlert("Invalid recipient format", "Recipient must be of a valid email address format.", "OK");
                }
            }

            if (willSend && Subject.Text == null && Body.Text == null)
            {
                if (!await DisplayAlert("Subject and body are blank", "Send anyway?", "Yes", "No"))
                {
                    willSend = false;
                }
            }

            else if (willSend && Subject.Text == null)
            {
                if (!await DisplayAlert("Subject is blank", "Send anyway?", "Yes", "No"))
                {
                    willSend = false;
                }
            }

            else if (willSend && Body.Text == null)
            {
                if (!await DisplayAlert("Body is blank", "Send anyway?", "Yes", "No"))
                {
                    willSend = false;
                }
            }

            return willSend;
        }
    }
}