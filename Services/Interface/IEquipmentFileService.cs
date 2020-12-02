using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Entities;
using WebApi.Models.Tasks;

namespace WebApi.Services
{
    public interface IEquipmentFileService
    {
        IEnumerable<dynamic> GetFileList(int equipmentId);
        FileDownload GetMediaName(int mediaId);
        int AddEquipmentFile(equipment_file fileData);
        bool DeleteEquipmentFile(int[] equipmentFileIds);
    }
}
