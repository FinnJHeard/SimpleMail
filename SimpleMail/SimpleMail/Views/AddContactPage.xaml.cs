using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleMail.Models;
using SimpleMail.Services;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SimpleMail.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddContactPage : ContentPage
    {
        User current_user;
        public AddContactPage(User current_user)
        {
            this.current_user = current_user;
            InitializeComponent();
        }

        private void Back_Clicked(object sender, EventArgs e)
        {
            
        }

        async private void Confirm_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(FirstName.Text) && !string.IsNullOrWhiteSpace(LastName.Text) && !string.IsNullOrWhiteSpace(EmailAddress.Text))
            {
                await App.Database.SaveContactAsync(new ContactTable
                {
                    OwnerID = current_user.userDB.ID,
                    Forename = FirstName.Text,
                    Surname = LastName.Text,
                    ContactEmail = EmailAddress.Text,
                });
            }
        }
    }
}