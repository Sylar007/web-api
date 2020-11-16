using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Entities;

namespace WebApi.Services
{
    public interface IEquipmentService
    {
        IEnumerable<dynamic> GetEquipmentList();
        IEnumerable<dynamic> GetEquipmentViewList();
        IEnumerable<dynamic> GetEquipmentLocationList();
        IEnumerable<dynamic> GetEquipmentRepairReplaceList();
        dynamic GetEquipmentById(int id);
        dynamic GetEquipmentRepairReplaceById(int id);
        dynamic GetEquipmentBySerialNo(string serialNo);
        equipment GetEquipmentByNo(equipment data);
        IEnumerable<dynamic> GetEquipmentListById(int id);
        int AddEquipment(equipment data);
        int EditEquipment(equipment data);
        IEnumerable<dynamic> GetHomeTotalEqLocationChart();
        IEnumerable<dynamic> GetHomeTotalEqProcessChart();
        IEnumerable<dynamic> GetEquipmentSelection();
        dynamic OpenEquipment(string equipment_no);
        dynamic GetEditEquipmentByEquipmentNo(string equipmentNo);
    }
}
