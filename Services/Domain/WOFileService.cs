using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Entities;
using WebApi.Helpers;
using WebApi.Models.Tasks;

namespace WebApi.Services
{
    public class WOFileService : IWOFileService
	{
		private DataContext _context;
		private readonly AppSettings _appSettings;
		public WOFileService(DataContext context, IOptions<AppSettings> appSettings)
		{
			_context = context;
			_appSettings = appSettings.Value;
		}
		public IEnumerable<dynamic> GetWOFileList(int woId, string fileType)
		{
				try
				{					
					return (from wf in _context.wo_file
							join m in _context.media on wf.media_id equals m.id
							where wf.wo_id == woId
							select new
							{
								file_type = wf.file_type,
								file_name = m.file_name,
								id = m.id
							}).ToList();
				}
				catch (Exception ex)
				{
					throw ex;
				}
		}

		public FileDownload GetMediaName(int mediaId)
		{
			try
			{
				var query = (from wf in _context.wo_file
							  join m in _context.media on wf.media_id equals m.id
							  where m.id == mediaId
							  select new
							  {
								  wf.file_type,
								  m.file_name,
								  wf.content_type
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

		public int AddWOFile(wo_file fileData)
		{
			try
			{
				_context.wo_file.Add(fileData);
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

		public bool DeleteWOFile(int[] woFileIds, string fileType)
		{
			try
			{
				List<wo_file> list = _context.wo_file.Where((wo_file wf) => woFileIds.Contains(wf.id) && wf.file_type == fileType).ToList();
				foreach (wo_file woFile in list)
				{
					media medium = _context.media.Where((media m) => m.id == woFile.media_id).FirstOrDefault();
					_context.wo_file.Remove(woFile);
					_context.SaveChanges();
					string path = Encoding.ASCII.GetBytes(_appSettings.MediaPath) + woFile.file_type + "/" + medium.file_name;
					if (File.Exists(path))
					{
						File.Delete(path);
					}
				}
				return true;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}
