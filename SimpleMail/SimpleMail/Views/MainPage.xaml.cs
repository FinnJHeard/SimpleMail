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
    public partial class MainPage : ContentPage
    {

        public User current_user;
        Email test;

        public MainPage(SimpleMail.Models.User current_user)
        {
            InitializeComponent();
            this.current_user = current_user;
            label_test.Text = current_user.email;
        }
        
        async void Logout_Clicked(object sender, EventArgs e)
        {
            //Clearing the stack of pages
            /*
            Console.WriteLine(label_test);
            var existingPages = Navigation.NavigationStack.ToList();
            foreach(var page in existingPages)
            {
                Navigation.RemovePage(page);
            }
            */
            await Navigation.PopAsync();
        }

        async void Send_Clicked(object sender, EventArgs e)
        {
            test = new Email(current_user.email);
            await Navigation.PushAsync(new SendAddress(test));
        }

        async void View_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new InboxPage());
        }

        async void Contacts_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ContactsPage());
        }

        

    }
}