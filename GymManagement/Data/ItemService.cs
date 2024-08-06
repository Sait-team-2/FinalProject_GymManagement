using SQLite;

namespace GymManagement.Data
{
    public class ItemService
    {
        //service layer for CRUD operations

        //Creates asyncronous (can share at anytime) connection: 
        private readonly SQLiteAsyncConnection _database;

        //Constructor of class which initializes the database:
        public ItemService(string dbPath)
        {
            //database path:
            _database = new SQLiteAsyncConnection(dbPath);
            //creating table:
            _database.CreateTableAsync<Item>().Wait(); //wait for transaction to be completed
        }
        //Fetching the table data and converting data into list:
        public Task<List<Item>> GetItemsAsync()
        {
            return _database.Table<Item>().ToListAsync();
        }
        //Get the item from the table based on ID match:
        public Task<Item> GetItemAsync(int id)
        {
            return _database.Table<Item>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }
        //Update if there is existing data update, else insert new data:
        public Task<int> SaveItemAsync(Item item)
        {
            if (item.Id != 0)
            {
                return _database.UpdateAsync(item);
            }
            else
            {
                return _database.InsertAsync(item);
            }
        }
        //Deletes items from database:
        public Task<int> DeleteItemAsync(Item item)
        {
            return _database.DeleteAsync(item);
        }
    }
}
