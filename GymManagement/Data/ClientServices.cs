using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GymManagement.Data
{
    public class ClientService
    {
        private readonly SQLiteAsyncConnection _database;

        public ClientService(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            InitializeDatabase().ConfigureAwait(false);
        }

        public async Task TruncateClientsTableAsync()
        {
            await _database.ExecuteAsync("DELETE FROM Client");
            await _database.ExecuteAsync("DELETE FROM sqlite_sequence WHERE name='Client'");
        }

        private async Task InitializeDatabase()
        {
            await _database.CreateTableAsync<Client>();
        }

        public Task<List<Client>> GetItemsAsync()
        {
            return _database.Table<Client>().ToListAsync();
        }
        public Task<Client> GetItemAsync(int id)
        {
            return _database.Table<Client>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public Task<Client> SearchClients(int id)
        {
            return _database.Table<Client>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public async Task<int> SaveClient(Client client)
        {
            try
            {
                if (client.Id != 0)
                {
                    var existingClient = await _database.Table<Client>().Where(i => i.Id == client.Id).FirstOrDefaultAsync();
                    if (existingClient != null)
                    {
                        // Update the existing client
                        await _database.UpdateAsync(client);
                        Console.WriteLine($"Updated Client ID: {client.Id}");
                        return client.Id;
                    }
                }

                // Insert the client into the database
                await _database.InsertAsync(client);
                Console.WriteLine($"Inserted Client ID: {client.Id}");
                return client.Id;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving client: {ex.Message}");
                throw;
            }
        }



        public Task<int> RemoveClient(Client client)
        {
            return _database.DeleteAsync(client);
        }
    }
}