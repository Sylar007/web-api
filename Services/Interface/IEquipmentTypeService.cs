using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Entities;

namespace WebApi.Services
{
    public interface IEquipmentTypeService
    {
        IEnumerable<dynamic> GetEquipmentTypeList();
        equipment_type GetEquipmentTypeById(int id);
        int AddEquipmentType(equipment_type data);
        int EditEquipmentType(equipment_type data);
    }
}
