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
    public partial class FolderPage : ContentPage
    {

        User current_user;
        Email email;

        public FolderPage(User current_user)
        {
            InitializeComponent();
            this.current_user = current_user;
            label_test.Text = "Welcome" + current_user.email + "!";
        }

        async void Back_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        async void Sent_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SentPage(current_user, 1));
        }

        async void Inbox_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new InboxPage(current_user, 0));
        }

        async void Spam_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new InboxPage(current_user, 2));
        }
    }
}