using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public DateTime BookingDate { get; set; }
        public string Service { get; set; }
    }
}
