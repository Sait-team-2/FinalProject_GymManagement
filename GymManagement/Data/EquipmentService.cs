using SQLite;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymManagement.Data
{
    public class EquipmentService
    {
        private List<Equipment> equipments = new List<Equipment>();
                private readonly SQLiteAsyncConnection _database;

        public EquipmentService(string dbPath)
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
            await _database.CreateTableAsync<Equipment>();
        }

        public Task<List<Equipment>> GetAllEquipmentsAsync()
        {
            return Task.FromResult(equipments);
        }

        public Task<Equipment> GetEquipmentByIdAsync(int id)
        {
            return Task.FromResult(equipments.FirstOrDefault(e => e.Id == id));
        }

        public Task AddEquipmentAsync(Equipment equipment)
        {
            equipments.Add(equipment);
            return Task.CompletedTask;
        }

        public Task UpdateEquipmentAsync(Equipment equipment)
        {
            var existingEquipment = equipments.FirstOrDefault(e => e.Id == equipment.Id);
            if (existingEquipment != null)
            {
                existingEquipment.Name = equipment.Name;
                existingEquipment.Type = equipment.Type;
                existingEquipment.LastMaintenanceDate = equipment.LastMaintenanceDate;
                existingEquipment.Description = equipment.Description;
                existingEquipment.CostPerDay = equipment.CostPerDay;
                existingEquipment.ImageUrl = equipment.ImageUrl;
            }
            return Task.CompletedTask;
        }

        public Task RemoveEquipmentAsync(int equipmentId)
        {
            equipments.RemoveAll(e => e.Id == equipmentId);
            return Task.CompletedTask;
        }
    }
}
