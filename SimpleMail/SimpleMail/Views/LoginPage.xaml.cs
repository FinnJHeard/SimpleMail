﻿using System;
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
            //Email.Text = "simple321mail@gmail.com";
            //Password.Text = "ipgroup123";
        }

        async void Login_Clicked(object sender, EventArgs e)
        {
            bool willLogin = true;

            if (Email.Text == null)
            {
                willLogin = false;
                await DisplayAlert("Email blank", "Enter an email address.", "OK");
            }

            if (willLogin && !await StrUtil.ValidateAddress(Email.Text))
            {
                willLogin = false;
                await DisplayAlert("Invalid email format", "Email must be of a valid email address format.", "OK");
            }

            if (willLogin)
            {
                loading.IsRunning = true;
                loading.IsVisible = true;
                loading.IsEnabled = true;

                try
                {
                    current_user = await User.authenticate(Email.Text, Password.Text);
                    loading.IsRunning = false;
                    loading.IsVisible = false;
                    loading.IsEnabled = false;
                    Password.Text = "";
                    //Database
                    UserTable userDB = await App.Database.CheckUserAsync(Email.Text);
                    if (userDB == null)
                    {
                        await App.Database.SaveUsersAsync(new UserTable
                        {
                            UserEmail = Email.Text,
                        });
                    }
                    userDB = await App.Database.CheckUserAsync(Email.Text);
                    current_user.userDB = userDB;
                    await Navigation.PushAsync(new MainPage(current_user));
                }
                catch
                {
                    loading.IsRunning = false;
                    loading.IsVisible = false;
                    loading.IsEnabled = false;
                    await DisplayAlert("Login failed", "Please try again", "OK");
                }
            }
        }
    }
}
