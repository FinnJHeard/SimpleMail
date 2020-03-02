using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SimpleMail.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SimpleMail.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SendBody : ContentPage
    {
        User current_user;
        Email emailToSend;

        public SendBody(User current_user, Email emailToSend)
        {
            InitializeComponent();
            this.current_user = current_user;
            this.emailToSend = emailToSend;
        }

        async void Back_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        async void Send_Clicked(object sender, EventArgs e)
        {
            //Send the email
            Console.WriteLine("Send button clicked");
            emailToSend.body = Body.Text;

            //emailToSend.body = "Dear simple123mail,\n\n How are you? \n\n Yours simple123mail.";
            emailToSend.subject = "An email!";
            Console.WriteLine("Email init");

            emailToSend.sendMessage(current_user);
            Console.WriteLine("Email sent?");


            await Navigation.PopAsync();
            Console.WriteLine("Gone back a page");
            //Woosh sound
        }
    }
}