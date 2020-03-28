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
    public partial class ContactsPage : ContentPage
    {
        User current_user;
        public ContactsPage(User current_user)
        {
            this.current_user = current_user;
            InitializeComponent();
        }

        async void View_Clicked(object sender, EventArgs e)
        {
            //Push to view contacts page
            await Navigation.PushAsync(new ViewContactsPage(current_user));
        }

        async void Add_Clicked(object sender, EventArgs e)
        {
            //Push to add contacts page
            await Navigation.PushAsync(new AddContactPage(current_user));
        }

        async void Back_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}