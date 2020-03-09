using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SimpleMail.Models;

namespace SimpleMail.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SendAddress : ContentPage
    {
        User current_user;
        Email emailToSend;

        public SendAddress(User current_user, Email emailToSend)
        {
            this.current_user = current_user;
            this.emailToSend = emailToSend;
            InitializeComponent();
        }

        async void Back_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        async void Continue_Clicked(object sender, EventArgs e)
        {
            emailToSend.recipientAddress = Recipient.Text;
            //emailToSend.recipientAddress = "simple123mail@yahoo.com";

            await Navigation.PushAsync(new SendBody(current_user, emailToSend));
        }
    }
}