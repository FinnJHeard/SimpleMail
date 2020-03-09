using System;
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
    public partial class SendBody : ContentPage
    {
        public Email test;

        public SendBody(Email test)
        {
            InitializeComponent();
            this.test = test;
        }

        async void Back_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        async void Send_Clicked(object sender, EventArgs e)
        {
            //Send the email
            test.body = Body.Text;
            await Navigation.PopAsync();
            //Woosh sound
        }
    }
}