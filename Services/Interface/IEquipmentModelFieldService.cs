using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Services
{
    public interface IEquipmentModelFieldService
    {
        IEnumerable<dynamic> GetEquipmentModelFieldList(int equipmentModelId, string fieldType);
        bool EditEquipmentModelField(int equipmentModelId, List<dynamic> dataList, string fieldType);
    }
}
