using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleMail.Models;
using SimpleMail.Services;
using System.Diagnostics;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SimpleMail.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public User current_user { get; set; }

        public LoginPage()
        {
            InitializeComponent();
        }

        async void Login_Clicked(object sender, EventArgs e)
        {
			try
			{
                current_user = await User.authenticate(Email.Text, Password.Text);
                await Navigation.PushAsync(new MainPage(current_user));
            }
			catch(Exception exception)
			{
                await DisplayAlert("Login failed", "Please try again", "OK");
            }
        }
    }
}