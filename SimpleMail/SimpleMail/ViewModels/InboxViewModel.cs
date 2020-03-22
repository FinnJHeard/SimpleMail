using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MailKit.Net.Pop3;
using SimpleMail.Models;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SimpleMail.ViewModels
{
    class InboxViewModel : BaseViewModel
    {
        public ObservableCollection<Email> Items { get; private set; }
        public ICommand UpCommand { get; }
        public ICommand DownCommand { get; }
        public User current_user;
        public int inboxPage;
        private bool loadingFlag;
        public bool loading { get { return loadingFlag; } set { loadingFlag = value; OnPropertyChanged(); } }
        public InboxViewModel(User user)
        {
            UpCommand = new Command(async () => await pageUp());
            DownCommand = new Command(async () => await pageDown());
            current_user = user;
            loading = false;
            inboxPage = 0;
            Items = new ObservableCollection<Email>();
            Task.Run(async () => { await Get_Emails(); });
        }

        async private Task Get_Emails()
        {
            int maxCount = 7;
            loading = true;
            using (Pop3Client emailClient = await current_user.getReceivingClient())
            {
                if (inboxPage * maxCount < emailClient.Count)
                {
                    Items.Clear();
                    for (int i = inboxPage * maxCount; i < (inboxPage + 1) * maxCount && i < emailClient.Count; i++)
                    {
                        var message = await emailClient.GetMessageAsync(i);
                        var emailMessage = new Email(message.TextBody, message.Subject);
                        emailMessage.bodyHtml = message.HtmlBody;

                        //Gets only the FIRST email ADDRESS in the list of senders and recievers
                        foreach (var mailbox in message.From.Mailboxes)
                        {
                            emailMessage.senderAddress = mailbox.Address; //mailbox.Name for sender's name
                            emailMessage.senderName = mailbox.Name;
                            break; //loop and store as list for multiple addresses?
                        }
                        foreach (var mailbox in message.To.Mailboxes)
                        {
                            emailMessage.recipientAddress = mailbox.Address; //mailbox.Name for recipient name
                            break;
                        }
                        Items.Add(emailMessage);
                    }
                }
                else if(inboxPage > 0)
                {
                    inboxPage--;
                }
            }
            loading = false;
        }

        async private Task pageUp()
        {
            if (inboxPage > 0)
            {
                inboxPage--;
                await Get_Emails();
            }
        }

        async private Task pageDown()
        {
            inboxPage++;
            await Get_Emails();
        }
    }
}
