using SQLite;
using System;

namespace GymManagement.Data
{
    public class Booking
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int ClientId { get; set; }
        public DateTime BookingStartDate { get; set; }
        public DateTime BookingEndDate { get; set; }
        public int? EquipmentId { get; set; } // Optional Equipment ID

        // These properties are not stored in the database but are used for display purposes
        [Ignore]
        public string ClientFirstName { get; set; }
        [Ignore]
        public string ClientLastName { get; set; }
        [Ignore]
        public string EquipmentName { get; set; } // Equipment name for display
    }
}
