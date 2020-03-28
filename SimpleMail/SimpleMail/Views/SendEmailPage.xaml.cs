using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SimpleMail.Models;
using SimpleMail.Services;
using SimpleMail.ViewModels;
using System.IO;
using System.Reflection;

namespace SimpleMail.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SendEmailPage : ContentPage
    {
        User current_user;
        Email emailToSend;
        List<string> addedAddresses;
        //SendEmailViewModel viewModel;
        refStr recipientsList;

        public SendEmailPage(User current_user)
        {
            this.current_user = current_user;
            emailToSend = new Email(current_user.email);
            addedAddresses = new List<string>();
            recipientsList = new refStr("Added addresses will appear here.");
            InitializeComponent();
            //viewModel = new SendEmailViewModel(current_user, recipientsList);
            //BindingContext = viewModel;
        }

        async void Add_Contact_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SendToContactPage(current_user, addedAddresses, recipientsList));
            await Task.Run(async () => { await updateLoop(); });
        }

        async Task updateLoop()
        {
            while (recipientsList.flag)
            {
                await Task.Delay(100);
            }
            Xamarin.Essentials.MainThread.BeginInvokeOnMainThread(() => {
                AddedRecipients.Text = recipientsList.str;
            });
            recipientsList.flag = true;
        }

        async void Add_Clicked(object sender, EventArgs e)
        {
            if(await ValidateRecipient())
            {
                addedAddresses.Add(Recipient.Text);

                if (recipientsList.str == "Added addresses will appear here.")
                {
                    recipientsList.str = "Added: " + Recipient.Text;
                }
                else
                {
                    recipientsList.str += ", " + Recipient.Text;
                }

                AddedRecipients.Text = recipientsList.str;
                Recipient.Text = string.Empty;
            }
        }

        async void Back_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        async void Send_Clicked(object sender, EventArgs e)
        {
            //Send the email
            Console.WriteLine("Send button clicked"); //test

            if (await ValidateRecipient() && await ValidateFields())
            {
                if (Recipient.Text != null)
                {
                    addedAddresses.Add(Recipient.Text);
                }

                emailToSend.recipientAddress = "";
                //emailToSend.recipientAddress = "simple321mail@gmail.com";

                emailToSend.recipientAddresses = addedAddresses;
                
                emailToSend.body = Body.Text;
                emailToSend.subject = Subject.Text;

                try
                {
                    emailToSend.sendMessage(current_user);

                    var whoosh = Plugin.SimpleAudioPlayer.CrossSimpleAudioPlayer.Current;
                    whoosh.Load(GetStreamFromFile("Whoosh.mp3"));
                    whoosh.Play();

                    await DisplayAlert("Success", "Email sent successfully", "OK");

                    await Navigation.PopAsync();
                }
                catch (Exception exception)
                {
                    await DisplayAlert("Failed", "Email error", "OK");
                }
            }

        }

        private Stream GetStreamFromFile(string v)
        {
            var assembly = typeof(App).GetTypeInfo().Assembly;
            Stream audioStream = assembly.GetManifestResourceStream("SimpleMail.Services." + v);
            return audioStream;
        }

        // For validation when: sending without having added, only having added, and having added and having an additional
        async Task<bool> ValidateRecipient()
        {
            bool isValid = true;
            
            if (addedAddresses.Count == 0)
            {
                if (Recipient.Text == null)
                {
                    isValid = false;
                    await DisplayAlert("Recipient blank", "Enter a valid recipient email address.", "OK");
                }
            }

            if (isValid && Recipient.Text != null)
            {
                if (!await StrUtil.ValidateAddress(Recipient.Text))
                {
                    isValid = false;
                    await DisplayAlert("Invalid recipient format", "Recipient must be of a valid email address format.", "OK");
                }
            }

            return isValid;
        }


        async Task<bool> ValidateFields()
        {
            bool isValid = true;

            if (Subject.Text == null && Body.Text == null)
            {
                if (!await DisplayAlert("Subject and body are blank", "Send anyway?", "Yes", "No"))
                {
                    isValid = false;
                }
            }

            else if (isValid && Subject.Text == null)
            {
                if (!await DisplayAlert("Subject is blank", "Send anyway?", "Yes", "No"))
                {
                    isValid = false;
                }
            }

            else if (isValid && Body.Text == null)
            {
                if (!await DisplayAlert("Body is blank", "Send anyway?", "Yes", "No"))
                {
                    isValid = false;
                }
            }

            return isValid;
        }
    }
}