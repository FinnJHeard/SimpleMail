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

        void Login_Clicked(object sender, EventArgs e)
        {
			while(true)
			{
				//Call check login credentials
				try
				{
				//Create new user
					current_user = new User(Email.Text, Password.Text);
					break;
				}
				catch(Exception exception)
				{
				//Do authentification (inside user object)
				
				}
			}

            Navigation.PushAsync(new MainPage(current_user));
        }
    }
}