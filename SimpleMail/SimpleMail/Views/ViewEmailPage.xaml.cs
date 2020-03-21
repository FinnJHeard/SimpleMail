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
    public partial class ViewEmailPage : ContentPage
    {
        User current_user;
        Email emailToDisplay;

        public ViewEmailPage(User current_user, Email emailToDisplay)
        { 
            InitializeComponent();
            
            this.current_user = current_user;
            this.emailToDisplay = emailToDisplay;
            
            ViewSender.Text = emailToDisplay.senderAddress;
            ViewSubject.Text = emailToDisplay.subject;
            ViewBody.Text = emailToDisplay.body;
        
        }


        async void Back_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}