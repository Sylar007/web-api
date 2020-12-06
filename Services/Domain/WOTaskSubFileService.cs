using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Entities;
using WebApi.Helpers;
using WebApi.Models.Tasks;

namespace WebApi.Services
{
    public class WOTaskSubFileService : IWOTaskSubFileService
    {
        private DataContext _context;
        public WOTaskSubFileService(DataContext context)
        {
            _context = context;
        }
        public IEnumerable<dynamic> GetWOTaskSubFileList(int woId, int task_sub_id, int upload_type)
        {
            try
            {
                return (from wt in _context.wo_task_sub_file
                        join m in _context.media on wt.media_id equals m.id
                        where wt.wo_id == woId && wt.task_sub_id == task_sub_id && wt.upload_type == upload_type
                        select new
                        {
                            file_type = wt.file_type,
                            file_name = m.file_name,
                            id = m.id
                        }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int AddWOtasksubFile(wo_task_sub_file fileData)
        {
            try
            {
                _context.wo_task_sub_file.Add(fileData);
                int num = _context.SaveChanges();
                if (num > 0)
                {
                    return fileData.id;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return -1;
        }

        public FileDownload GetMediaName(int mediaId)
        {
            try
            {
                var query = (from wt in _context.wo_task_sub_file
                             join m in _context.media on wt.media_id equals m.id
                             where m.id == mediaId
                             select new
                             {
                                 wt.file_type,
                                 m.file_name,
                                 wt.content_type
                             }).FirstOrDefault();
                FileDownload fileDownload = new FileDownload();
                fileDownload.name = query.file_name;
                fileDownload.fileType = query.file_type;
                fileDownload.contentType = query.content_type;
                return fileDownload;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
