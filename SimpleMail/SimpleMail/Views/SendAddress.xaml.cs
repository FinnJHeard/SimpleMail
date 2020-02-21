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
    public partial class SendAddress : ContentPage
    {
        public SendAddress()
        {
            InitializeComponent();
        }

        async void Back_Clicked(object sender, EventArgs e)
        {
            
            await Navigation.PopAsync();
        }

        async void Continue_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SendBody());
        }
    }
}