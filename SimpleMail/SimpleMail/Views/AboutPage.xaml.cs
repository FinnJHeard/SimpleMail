using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SimpleMail.Services;

namespace SimpleMail.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class AboutPage : ContentPage
    {
        public AboutPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            //listView.ItemsSource = await App.Database.GetUsersAsync();
            //listContacts.ItemsSource = await App.Database.GetContactsAsync();
        }

        async void OnButtonClicked(object sender, EventArgs e)
        {
            /*if (!string.IsNullOrWhiteSpace(emailEntry.Text))
            {
                await App.Database.SaveUsersAsync(new UserTable
                {
                    UserEmail = emailEntry.Text,
                });

                //emailEntry.Text = string.Empty;
                listView.ItemsSource = await App.Database.GetUsersAsync();
            }
            if (!string.IsNullOrWhiteSpace(emailEntry.Text))
            {
                await App.Database.SaveContactAsync(new ContactTable
                {
                    ContactEmail = emailEntry.Text,
                });

                emailEntry.Text = string.Empty;
                listContacts.ItemsSource = await App.Database.GetContactsAsync();
            }*/
        }
    }
}