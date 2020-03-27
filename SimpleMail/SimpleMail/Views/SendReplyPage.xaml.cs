using MimeKit;
using SimpleMail.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SimpleMail.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SendReplyPage : ContentPage
    {
        User current_user;
        Email prevEmail;
        Email reply;
        
        public SendReplyPage(User current_user, Email prevEmail)
        {
            InitializeComponent();
            this.current_user = current_user;
            this.prevEmail = prevEmail;
            reply = new Email(current_user.email);
        }

        async void Back_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        async void Send_Clicked(object sender, EventArgs e)
        {
            Console.WriteLine("Send button clicked"); //test

            if (await ValidateFields())
            {
                reply.body = Body.Text;

                try
                {
                    reply.sendReply(current_user, prevEmail.message);

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
            if (Body.Text == null)
            {
                return await DisplayAlert("Body is blank", "Send anyway?", "Yes", "No");
            }

            return true;
        }
    }
}
