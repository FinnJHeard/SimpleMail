using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SimpleMail.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
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
            await Navigation.PushAsync(new SendAddress());
        }
    }
}