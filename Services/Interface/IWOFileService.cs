using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Entities;

namespace WebApi.Services
{
    public interface IWOFileService
    {
        IEnumerable<dynamic> GetWOFileList(int woId, string fileType);
        int AddWOFile(wo_file fileData);
        bool DeleteWOFile(int[] woFileIds, string fileType);
    }
}
