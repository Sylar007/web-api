using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Entities;

namespace WebApi.Services
{
    public interface IEquipmentModelService
    {
        IEnumerable<dynamic> GetEquipmentModelList();
        IEnumerable<dynamic> GetEquipmentModelProcessList();
        dynamic GetEquipmentModelById(int id);
        int AddEquipmentModel(equipment_model data);
        int EditEquipmentModel(equipment_model data);
        IEnumerable<dynamic> GetTotalEquipmentModel();
    }
}
