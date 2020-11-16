using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Entities;

namespace WebApi.Services
{
    public interface IEquipmentStatusService
    {
        IEnumerable<dynamic> GetEquipmentStatusList();
        equipment_status GetEquipmentStatusById(int id);
    }
}
