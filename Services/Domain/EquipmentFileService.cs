using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Entities;
using WebApi.Helpers;

namespace WebApi.Services
{
    public class EquipmentFileService : IEquipmentFileService
	{
		private DataContext _context;

		public EquipmentFileService(DataContext context)
		{
			_context = context;
		}
		public IEnumerable<dynamic> GetEquipmentFileList(int equipmentId, string fileType)
		{
			try
			{
				try
				{
					if (equipmentId == 0)
					{
						return new List<equipment_file>();
					}
					return (from ef in _context.equipment_file
							join m in _context.media on ef.media_id equals m.id
							where ef.equipment_id == equipmentId && ef.file_type == fileType
							select new
							{
								ef.file_type,
								m.file_name,
								ef.id
							}).ToList();
				}
				catch (Exception ex)
				{
					throw ex;
				}
			}
			catch (Exception ex2)
			{
				throw ex2;
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
