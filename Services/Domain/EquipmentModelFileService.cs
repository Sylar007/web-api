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
    public class EquipmentModelFileService : IEquipmentModelFileService
    {
		private DataContext _context;

		public EquipmentModelFileService(DataContext context)
		{
			_context = context;
		}
		public IEnumerable<dynamic> GetEquipmentModelFileList(int equipmentModelId, string fileType)
		{
			try
			{
				try
				{
					if (equipmentModelId == 0)
					{
						return new List<equipment_model_file>();
					}
					return (from emf in _context.equipment_model_file
							join m in _context.media on emf.media_id equals m.id
							where emf.equipment_model_id == equipmentModelId && emf.file_type == fileType
							select new
							{
								emf.file_type,
								m.file_name,
								emf.id
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

		public int AddEquipmentModelFile(equipment_model_file fileData)
		{
			try
			{
				_context.equipment_model_file.Add(fileData);
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

		public bool DeleteEquipmentModelFile(int[] equipmentModelFileIds)
		{
			try
			{
				List<equipment_model_file> list = _context.equipment_model_file.Where((equipment_model_file emf) => equipmentModelFileIds.Contains(emf.id)).ToList();
				foreach (equipment_model_file equipmentModelFile in list)
				{
					media media = _context.media.Where((media m) => m.id == equipmentModelFile.media_id).FirstOrDefault();
					_context.equipment_model_file.Remove(equipmentModelFile);
					_context.SaveChanges();
					var mediaPath = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("AppSettings")["MediaPath"];
					string path = mediaPath + equipmentModelFile.file_type + "/" + media.file_name;
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
