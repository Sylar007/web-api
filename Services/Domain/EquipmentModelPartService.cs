using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Entities;
using WebApi.Helpers;

namespace WebApi.Services
{
	public class EquipmentModelPartService : IEquipmentModelPartService
	{
		private DataContext _context;

		public EquipmentModelPartService(DataContext context)
		{
			_context = context;
		}
		public IEnumerable<dynamic> GetEquipmentModelPartList(int equipmentModelId)
		{
			try
			{
				if (equipmentModelId == 0)
				{
					return new List<equipment_model_part>();
				}
				return (from p in _context.parts
						join pm in _context.part_model on p.part_model_id equals pm.id into pmJoin
						from pmj in pmJoin.DefaultIfEmpty()
						join e in _context.equipment_model_part_model on pmj.id equals e.part_model_id into eJoin
						from ej in eJoin.DefaultIfEmpty()
						where ej.equipment_model_id == equipmentModelId
						select new
						{
							id = p.id,
							model_no = ((pmj != null) ? pmj.model_no : ""),
							model_name = ((pmj != null) ? pmj.model_name : ""),
							part_name = ((pmj != null) ? pmj.name : ""),
							part_code = ((pmj != null) ? pmj.code : ""),
							serial_no = p.serial_no
						}).ToList();
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public IEnumerable<dynamic> GetPendingEquipmentModelPartList(int equipmentModelId, int equipmentId)
		{
			try
			{
				if (equipmentModelId == 0)
				{
					return new List<equipment_model_part>();
				}
				List<object> list = new List<object>();
				var list2 = (from p in _context.parts
							 join pm in _context.part_model on p.part_model_id equals pm.id into pmJoin
							 from pmj in pmJoin.DefaultIfEmpty()
							 join e in _context.equipment_model_part_model on pmj.id equals e.part_model_id into eJoin
							 from ej in eJoin.DefaultIfEmpty()
							 where ej.equipment_model_id == equipmentModelId
							 select new
							 {
								 id = p.id,
								 model_no = ((pmj != null) ? pmj.model_no : ""),
								 model_name = ((pmj != null) ? pmj.model_name : ""),
								 part_name = ((pmj != null) ? pmj.name : ""),
								 part_code = ((pmj != null) ? pmj.code : ""),
								 serial_no = p.serial_no
							 }).ToList();
				var list3 = (from ep in _context.equipment_part
							 join e in _context.equipments on ep.equipment_id equals e.id into pmJoin
							 from ej in pmJoin.DefaultIfEmpty()
							 where ep.equipment_id != equipmentId && ej.equipment_model_id == equipmentModelId
							 select new
							 {
								 ep.part_id
							 }).ToList();
				foreach (var assignedPart in list3)
				{
					list2.RemoveAll(p => p.id == assignedPart.part_id);
				}
				return list2;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public bool EditEquipmentModelPart(int equipmentModelId, List<dynamic> dataList)
		{
			try
			{
				{
					List<equipment_model_part> list = _context.equipment_model_part.Where((equipment_model_part emp) => emp.equipment_model_id == equipmentModelId).ToList();
					foreach (equipment_model_part item in list)
					{
						_context.equipment_model_part.Remove(item);
						_context.SaveChanges();
					}
					foreach (dynamic data in dataList)
					{
						equipment_model_part equipment_model_part = new equipment_model_part();
						equipment_model_part.equipment_model_id = equipmentModelId;
						equipment_model_part.part_id = Convert.ToInt32(data);
						equipment_model_part entity = equipment_model_part;
						_context.equipment_model_part.Add(entity);
						_context.SaveChanges();
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
