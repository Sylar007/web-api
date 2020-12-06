using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Entities;
using WebApi.Models.Tasks;

namespace WebApi.Services
{
    public interface IWOTaskSubFileService
    {
        public IEnumerable<dynamic> GetWOTaskSubFileList(int woId, int task_sub_id, int upload_type);
        int AddWOtasksubFile(wo_task_sub_file fileData);
        FileDownload GetMediaName(int mediaId);
    }
}
