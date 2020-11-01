using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Services
{
    public interface IEquipmentModelPartModelService
    {
        IEnumerable<dynamic> GetEquipmentModelPartModelList(int equipmentModelId);
        bool EditEquipmentModelPartModel(int equipmentModelId, List<dynamic> dataList);
    }
}
