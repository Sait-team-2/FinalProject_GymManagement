using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.Generic;
using GymManagement.Models;

namespace GymManagement.Services
{
    public class EquipmentService
    {
        private List<Equipment> equipments = new List<Equipment>();

        public List<Equipment> GetAllEquipments()
        {
            return equipments;
        }

        public void AddEquipment(Equipment equipment)
        {
            equipments.Add(equipment);
        }

        public void RemoveEquipment(int equipmentId)
        {
            equipments.RemoveAll(e => e.Id == equipmentId);
        }
    }
}

