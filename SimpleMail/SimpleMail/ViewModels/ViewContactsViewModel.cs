using SimpleMail.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using SimpleMail.Models;

namespace SimpleMail.ViewModels
{
    class ViewContactsViewModel : BaseViewModel
    {
        public User current_user;
        public int contactsPage;
        private int maxPage;
        private int maxCount = 7;
        private List<ContactTable> allContacts;
        public ObservableCollection<ContactTable> Items { get; private set; }

        public ViewContactsViewModel(User current_user)
        {
            this.current_user = current_user;
            maxPage = 0;
            contactsPage = 0;
            Items = new ObservableCollection<ContactTable>();
            allContacts = new List<ContactTable>();
            Task.Run(async () => { await Init_Contacts(); });
        }

        async private Task Init_Contacts()
        {
            allContacts = await App.Database.GetUserContactsAsync(current_user.userDB.ID);
            maxPage = Math.Max((allContacts.Count - 1) / maxCount,0);
            Get_Contacts();
        }
        private void Get_Contacts()
        {
            
            Items.Clear();
            for (int i = contactsPage * maxCount; i < (contactsPage + 1) * maxCount && i < allContacts.Count; i++)
            {
                Items.Add(allContacts[i]);
            }
        }

        async private Task pageUp()
        {
            if (contactsPage > 0)
            {
                contactsPage--;
                Get_Contacts();
            }
        }

        async private Task pageDown()
        {
            if (contactsPage < maxPage)
            {
                contactsPage++;
                Get_Contacts();
            }
        }
    }
}
