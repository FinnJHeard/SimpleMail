using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SimpleMail.Models;

namespace SimpleMail.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SendAddress : ContentPage
    {
        Email test;

        public SendAddress(Email test)
        {
            this.test = test;
            InitializeComponent();
        }

        async void Back_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        async void Continue_Clicked(object sender, EventArgs e)
        {
            test.recipientAddress = Recipient.Text;
            await Navigation.PushAsync(new SendBody(test));
        }
    }
}