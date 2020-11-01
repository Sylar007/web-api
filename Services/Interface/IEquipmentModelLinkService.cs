using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Services
{
    public interface IEquipmentModelLinkService
    {
        IEnumerable<dynamic> GetEquipmentModelLinkList(int equipmentModelId, string linkType);
        bool EditEquipmentModelLink(int equipmentModelId, List<dynamic> dataList, string linkType);
    }
}
