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
    public partial class ViewContactsPage : ContentPage
    {
        User current_user;
        public ViewContactsPage(User current_user)
        {
            this.current_user = current_user;
            InitializeComponent();
        }


        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var contacts = new List<String>();
            contacts.Add("Person1");
            contacts.Add("Person2");
            contacts.Add("Person3");
            contacts.Add("Person4");
            contacts.Add("Person4");
            
            
            contactsListView.ItemsSource = await App.Database.GetUserContactsAsync(current_user.userDB.ID);
        }
    }
}