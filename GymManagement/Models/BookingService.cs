using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.Generic;
using GymManagement.Models;

namespace GymManagement.Services
{
    public class BookingService
    {
        private List<Booking> bookings = new List<Booking>();

        public List<Booking> GetAllBookings()
        {
            return bookings;
        }

        public void AddBooking(Booking booking)
        {
            bookings.Add(booking);
        }

        public void RemoveBooking(int bookingId)
        {
            bookings.RemoveAll(b => b.Id == bookingId);
        }
    }
}
