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
        public ContactsPage()
        {
            InitializeComponent();
        }

        async void View_Clicked(object sender, EventArgs e)
        {
            //Push to view contacts page
            await Navigation.PushAsync(new ViewContactsPage());
        }

        async void Add_Clicked(object sender, EventArgs e)
        {
            //Push to add contacts page
            await Navigation.PushAsync(new AddContactPage());
        }
    }
}