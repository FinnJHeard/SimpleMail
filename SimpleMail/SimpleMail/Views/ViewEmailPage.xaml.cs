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

        public ViewEmailPage(User current_user, Email emailToDisplay, bool sentEmail)
        { 
            InitializeComponent();
            
            this.current_user = current_user;
            this.emailToDisplay = emailToDisplay;
            if (sentEmail)
            {
                ViewSender.Text = emailToDisplay.recipientAddress;
                helpText.Text = "This is the email that you sent";
            }
            else
            {
                ViewSender.Text = emailToDisplay.senderAddress;
                helpText.Text = "This is the email that you recieved";
            }
            ViewSubject.Text = emailToDisplay.subject;
            var html = new HtmlWebViewSource();
            if (emailToDisplay.bodyHtml != null)
            {
                html.Html = emailToDisplay.bodyHtml;
            }
            else
            {
                html.Html = "<html><body><p>"+emailToDisplay.body+"</p></body></html>";
            }
            webView.Source = html;
        }


        async void Back_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}