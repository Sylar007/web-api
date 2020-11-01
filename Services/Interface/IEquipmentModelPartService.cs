using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Services
{
    public interface IEquipmentModelPartService
    {
        IEnumerable<dynamic> GetEquipmentModelPartList(int equipmentModelId);
        IEnumerable<dynamic> GetPendingEquipmentModelPartList(int equipmentModelId, int equipmentId);
        bool EditEquipmentModelPart(int equipmentModelId, List<dynamic> dataList);
    }
}
