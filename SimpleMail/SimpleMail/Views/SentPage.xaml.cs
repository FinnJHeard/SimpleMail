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
using SimpleMail.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace SimpleMail.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SentPage : ContentPage
    {

        User current_user;
        Email selectedEmail = null;
        Email tappedEmail = null;

        public SentPage(User current_user, int folderType)
        {
            InitializeComponent();
            this.current_user = current_user;
            BindingContext = new InboxViewModel(current_user, folderType);
        }

        private void Selected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            selectedEmail = e.SelectedItem as Email;
        }

        async private void Tapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            tappedEmail = e.Item as Email;
            if (tappedEmail != null && tappedEmail == selectedEmail)
            {
                await Navigation.PushAsync(new ViewEmailPage(current_user, tappedEmail, true));
            }
        }

        async void Back_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}