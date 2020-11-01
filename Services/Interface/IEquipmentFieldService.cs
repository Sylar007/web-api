using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Entities;

namespace WebApi.Services
{
    public interface IEquipmentFieldService
    {
        IEnumerable<dynamic> GetEquipmentFieldList(int equipmentId, string fieldType);
        bool EditEquipmentField(int equipmentId, List<dynamic> dataList, string fieldType);
        bool CreateEquipmentField(equipment_field equipment_fieldParam);
        bool UpdateEquipmentField(equipment_field equipmentField);
        bool DeleteEquipmentField(int equipmentId);
    }
}
