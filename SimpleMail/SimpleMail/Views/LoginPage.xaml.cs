using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleMail.Models;
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
            Console.WriteLine("DEBUG - login button clicked");
            //Debug.WriteLine("Login clicked");
            //Call check login credentials
            

            //Create new user
            current_user = new User(Email.Text, Password.Text);

            current_user.email = "simple123mail@yahoo.com";
            current_user.password = "ipgroup123";

            await Navigation.PushAsync(new MainPage(current_user));
        }
    }
}