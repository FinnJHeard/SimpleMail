using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;

namespace SimpleMail.Services
{
    //Helpful info https://docs.microsoft.com/en-us/xamarin/get-started/quickstarts/database?pivots=windows
    public class Database
    {
        readonly SQLiteAsyncConnection _database;

        public Database(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<UserTable>().Wait();
            _database.CreateTableAsync<ContactTable>().Wait();
        }


        //User Table
        public Task<List<UserTable>> GetUsersAsync()
        {
            return _database.Table<UserTable>().ToListAsync();
        }

        public Task<UserTable> GetUserAsync(int id)
        {
            return _database.Table<UserTable>()
                            .Where(i => i.ID == id)
                            .FirstOrDefaultAsync();
        }

        public Task<int> SaveUsersAsync(UserTable users)
        {
            return _database.InsertAsync(users);
        }



        //Contact Table
        public Task<List<ContactTable>> GetContactsAsync()
        {
            return _database.Table<ContactTable>().ToListAsync();
        }

        public Task<ContactTable> GetContactAsync(int id)
        {
            return _database.Table<ContactTable>()
                            .Where(i => i.ContactID == id)
                            .FirstOrDefaultAsync();
        }

        public Task<int> SaveContactAsync(ContactTable contacts)
        {
            return _database.InsertAsync(contacts);
        }
    }
}