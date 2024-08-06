using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;

namespace GymManagement.Data
{
    public class BookingService
    {
        private readonly SQLiteAsyncConnection _database;

        public BookingService(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Booking>().Wait();
        }

        public Task<List<Booking>> GetItemsAsync()
        {
            return _database.Table<Booking>().ToListAsync();
        }

        public Task<Booking> GetBookingAsync(int id)
        {
            return _database.Table<Booking>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveBookingAsync(Booking booking)
        {
            if (booking.Id != 0)
            {
                return _database.UpdateAsync(booking);
            }
            else
            {
                return _database.InsertAsync(booking);
            }
        }

        public Task<int> DeleteBookingAsync(Booking booking)
        {
            return _database.DeleteAsync(booking);
        }

        public async Task<List<Booking>> GetBookingsWithClientDetailsAsync()
        {
            var bookings = await GetItemsAsync();
            foreach (var booking in bookings)
            {
                var client = await _database.Table<Client>().Where(c => c.Id == booking.ClientId).FirstOrDefaultAsync();
                if (client != null)
                {
                    booking.ClientFirstName = client.FirstName;
                    booking.ClientLastName = client.LastName;
                }
            }
            return bookings;
        }

        public Task<List<Client>> GetClientsAsync()
        {
            return _database.Table<Client>().ToListAsync();
        }
    }
}
