using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Services
{
    public interface IEquipmentPartService
    {
        IEnumerable<dynamic> GetEquipmentPartList(int equipmentId);
        bool EditEquipmentPart(int equipmentId, List<dynamic> dataList);
    }
}
