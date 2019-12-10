using System.Collections.Generic;
using System.Threading.Tasks;

using SQLite;

namespace MyClock
{
    public class Database
    {
        readonly SQLiteAsyncConnection _database;

        public Database(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<AlarmItem>().Wait();
        }

        public Task<List<AlarmItem>> GetAllAlarmItemsAsync()
        {
            return _database.Table<AlarmItem>().ToListAsync();
        }

        public Task<int> SaveAlarmItemAsync(AlarmItem alarmItem)
        {
            if (alarmItem.ID != 0)
            {
                return _database.UpdateAsync(alarmItem);
            }
            else
            {
                return _database.InsertAsync(alarmItem);
            }
        }

        public Task<int> DeleteAlarmItemAsync(AlarmItem alarmItem)
        {
            return _database.DeleteAsync(alarmItem);
        }
    }
}
