using SQLite;
using System;

namespace GymManagement.Data
{
    public class Equipment
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public DateTime LastMaintenanceDate { get; set; }
        public string Description { get; set; }
        public decimal CostPerDay { get; set; }
        public string ImageUrl { get; set; }
    }
}
