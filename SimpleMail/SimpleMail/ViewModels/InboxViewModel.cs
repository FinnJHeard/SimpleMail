using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MailKit.Net.Imap;
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
        private int maxPage;
        private int folderType;
        public InboxViewModel(User user, int folderType)
        {
            UpCommand = new Command(async () => await pageUp());
            DownCommand = new Command(async () => await pageDown());
            current_user = user;
            inboxPage = 0;
            maxPage = 0;
            this.folderType = folderType;
            Items = new ObservableCollection<Email>();
            Task.Run(async () => { await Get_Emails(); });
        }

        async private Task Get_Emails() //Multiple Calls on different threads crashes
        {
            int maxCount = 7;
            Items.Clear();
            for (int i = 0; i < maxCount; i++)
            {
                Items.Add(new Email(true));
            }

            using (ImapClient emailClient = await current_user.getReceivingClient())
            {
                var inbox = getFolder(emailClient);
                inbox.Open(MailKit.FolderAccess.ReadOnly);
                maxPage = (inbox.Count - 1) / maxCount;
                int k = 0;
                for (int i = inbox.Count - (inboxPage*maxCount) - 1; i >= 0 && i >= inbox.Count - ((inboxPage+1) * maxCount); i--)
                {
                    var message = await inbox.GetMessageAsync(i);
                    var emailMessage = new Email(message.TextBody, message.Subject);
                    emailMessage.bodyHtml = message.HtmlBody;
                    emailMessage.date = message.Date.ToString("dd/MM/yyyy", new System.Globalization.CultureInfo("en-GB"));

                    emailMessage.senderName = "(No Subject)";
                    //Gets only the FIRST email ADDRESS in the list of senders and recievers
                    foreach (var mailbox in message.From.Mailboxes)
                    {
                        emailMessage.senderAddress = mailbox.Address; //mailbox.Name for sender's name
                        emailMessage.senderName = mailbox.Name;
                        if(emailMessage.senderName == null)
                        {
                            emailMessage.senderName = emailMessage.senderAddress;
                        }
                        else if(emailMessage.senderName == "")
                        {
                            emailMessage.senderName = emailMessage.senderAddress;
                        }
                        break; //loop and store as list for multiple addresses?
                    }
                    foreach (var mailbox in message.To.Mailboxes)
                    {
                        emailMessage.recipientAddress = mailbox.Address; //mailbox.Name for sender's name
                        emailMessage.recipientName = mailbox.Name;
                        if (emailMessage.recipientName == null)
                        {
                            emailMessage.recipientName = emailMessage.recipientAddress;
                        }
                        else if (emailMessage.recipientName == "")
                        {
                            emailMessage.recipientName = emailMessage.recipientAddress;
                        }
                        break; //loop and store as list for multiple addresses?
                    }
                    emailMessage.loading = false;
                    Items[k] = emailMessage;
                    k++;
                }
                while (k < maxCount)
                {
                    Items[k++] = new Email(false);
                }
            }
        }

        private MailKit.IMailFolder getFolder(ImapClient emailClient)
        {
            switch (folderType)
            {
                case 1:
                    return emailClient.GetFolder(MailKit.SpecialFolder.Sent);
                case 2:
                    return emailClient.GetFolder(MailKit.SpecialFolder.Junk);
                default:
                    return emailClient.Inbox;
            }
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
            if (inboxPage < maxPage)
            {
                inboxPage++;
                await Get_Emails();
            }
        }
    }
}
