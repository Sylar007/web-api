using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Services
{
    public interface IEquipmentLinkService
    {
        IEnumerable<dynamic> GetEquipmentLinkList(int equipmentId, string linkType);
        bool EditEquipmentLink(int equipmentId, List<dynamic> dataList, string linkType);
    }
}
