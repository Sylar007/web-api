﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Entities;

namespace WebApi.Services
{
    public interface IEquipmentFileService
    {
        IEnumerable<dynamic> GetEquipmentFileList(int equipmentId, string fileType);
        int AddEquipmentFile(equipment_file fileData);
        bool DeleteEquipmentFile(int[] equipmentFileIds);
    }
}