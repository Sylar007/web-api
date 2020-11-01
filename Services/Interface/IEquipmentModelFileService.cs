using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Entities;

namespace WebApi.Services
{
    public interface IEquipmentModelFileService
    {
        IEnumerable<dynamic> GetEquipmentModelFileList(int equipmentModelId, string fileType);
        int AddEquipmentModelFile(equipment_model_file fileData);
        bool DeleteEquipmentModelFile(int[] equipmentModelFileIds);
    }
}
