using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleMail.Models;

using MailKit.Net.Smtp;
using MailKit;
using MimeKit;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SimpleMail.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {

        User current_user;
        Email email;

        public MainPage(User current_user)
        {
            InitializeComponent();
            this.current_user = current_user;
            label_test.Text = "Welcome" + current_user.email + "!";
        }
        
        async void Logout_Clicked(object sender, EventArgs e)
        {
            //Disconnect user from both servers to send and receive

            await Navigation.PopAsync(); // Needs to clear navigation stack
        }

        async void Send_Clicked(object sender, EventArgs e)
        {
            //email = new Email(current_user.email);
            await Navigation.PushAsync(new SendEmail(current_user));
        }

        async void View_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new InboxPage(current_user));
        }

        async void Contacts_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ContactsPage());
        }
    }
}