﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleMail.Models;

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
            //Call check login credentials
            
            //Create new user
            current_user = new User(Email.Text);

            await Navigation.PushAsync(new MainPage());
        }
    }
}