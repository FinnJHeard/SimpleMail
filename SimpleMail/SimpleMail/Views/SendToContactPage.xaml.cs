using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleMail.Models;
using SimpleMail.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SimpleMail.ViewModels;

namespace SimpleMail.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SendToContactPage : ContentPage
    {
        User current_user;
        List<string> addedAddresses;
        ContactTable selectedEmail = null;
        ContactTable tappedEmail = null;
        refStr recipientsList;

        public SendToContactPage(User current_user, List<string> addedAddresses, refStr recipientsList)
        {
            InitializeComponent();
            BindingContext = new ViewContactsViewModel(current_user);
            this.current_user = current_user;
            this.addedAddresses = addedAddresses;
            this.recipientsList = recipientsList;
        }

        async void Back_Clicked(object sender, EventArgs e)
        {
            recipientsList.flag = false;
            await Navigation.PopAsync();
        }

        private void Selected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            selectedEmail = e.SelectedItem as ContactTable;
        }

        async private void Tapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            tappedEmail = e.Item as ContactTable;
            if (tappedEmail != null && tappedEmail == selectedEmail)
            {
                addedAddresses.Add(selectedEmail.ContactEmail);
                if (recipientsList.str == "Added addresses will appear here.")
                {
                    recipientsList.str = "Added: " + selectedEmail.ContactEmail;
                }
                else
                {
                    recipientsList.str += ", " + selectedEmail.ContactEmail;
                }
                recipientsList.flag = false;
                await Navigation.PopAsync();
            }
        }
    }
}