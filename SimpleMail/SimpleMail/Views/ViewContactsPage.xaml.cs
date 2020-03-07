﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SimpleMail.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewContactsPage : ContentPage
    {
        public ViewContactsPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            var contacts = new List<String>();
            contacts.Add("Person1");
            contacts.Add("Person2");
            contacts.Add("Person3");
            contacts.Add("Person4");

            contactsListView.ItemsSource = contacts;

        }
    }
}