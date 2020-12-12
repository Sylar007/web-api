using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Entities;

namespace WebApi.Services
{
    public interface IEquipmentModelFieldService
    {
        IEnumerable<dynamic> GetEquipmentModelFieldList(int equipmentModelId, string fieldType);
        bool EditEquipmentModelField(int equipmentModelId, List<dynamic> dataList, string fieldType);
        bool CreateEquipmentModelField(equipment_model_field equipmentmodel_fieldParam);
        bool UpdateEquipmentModelField(equipment_model_field equipmentmodel_fieldParam);
        bool DeleteEquipmentModelField(int id);
    }
}
