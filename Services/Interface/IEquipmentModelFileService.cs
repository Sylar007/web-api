using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Entities;
using WebApi.Models.Tasks;

namespace WebApi.Services
{
    public interface IEquipmentModelFileService
    {
        IEnumerable<dynamic> GetFileList(int equipmentModelId);
        FileDownload GetMediaName(int mediaId);
        int AddEquipmentModelFile(equipment_model_file fileData);
        bool DeleteEquipmentModelFile(int[] equipmentFileIds);
    }
}
