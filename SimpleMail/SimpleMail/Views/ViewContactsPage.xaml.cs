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
    public partial class ViewContactsPage : ContentPage
    {
        User current_user;
        public ViewContactsPage(User current_user)
        {
            this.current_user = current_user;
            InitializeComponent();
            BindingContext = new ViewContactsViewModel(current_user);
        }

        async void Back_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}