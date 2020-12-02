using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Entities;
using WebApi.Helpers;
using WebApi.Models.Tasks;

namespace WebApi.Services
{
    public class EquipmentFileService : IEquipmentFileService
	{
		private DataContext _context;

		public EquipmentFileService(DataContext context)
		{
			_context = context;
		}
		public IEnumerable<dynamic> GetFileList(int equipmentId)
		{
			try
			{
				return (from ef in _context.equipment_file
						join m in _context.media on ef.media_id equals m.id
						where ef.equipment_id == equipmentId
						select new
						{
							file_type = ef.file_type,
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
				var query = (from ef in _context.equipment_file
							 join m in _context.media on ef.media_id equals m.id
							 where m.id == mediaId
							 select new
							 {
								 ef.file_type,
								 m.file_name,
								 ef.content_type
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

		public int AddEquipmentFile(equipment_file fileData)
		{
			try
			{
				_context.equipment_file.Add(fileData);
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

		public bool DeleteEquipmentFile(int[] equipmentFileIds)
		{
			try
			{				
				List<equipment_file> list = _context.equipment_file.Where((equipment_file ef) => equipmentFileIds.Contains(ef.id)).ToList();
				foreach (equipment_file equipmentFile in list)
				{
					media medium = _context.media.Where((media m) => m.id == equipmentFile.media_id).FirstOrDefault();
					_context.equipment_file.Remove(equipmentFile);
					_context.SaveChanges();
					var mediaPath = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("AppSettings")["MediaPath"];
					string path = mediaPath + equipmentFile.file_type + "/" + medium.file_name;
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
