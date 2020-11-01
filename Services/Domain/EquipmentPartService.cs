using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Entities;
using WebApi.Helpers;

namespace WebApi.Services
{
	public class EquipmentPartService : IEquipmentPartService
	{
		private DataContext _context;

		public EquipmentPartService(DataContext context)
		{
			_context = context;
		}
		public IEnumerable<dynamic> GetEquipmentPartList(int equipmentId)
		{
			try
			{
				if (equipmentId == 0)
				{
					return new List<equipment_part>();
				}
				return (from emp in _context.equipment_part
						join p in _context.parts on emp.part_id equals p.id into pJoin
						from pj in pJoin.DefaultIfEmpty()
						join pa in _context.part_model on pj.part_model_id equals pa.id into paJoin
						from paj in paJoin.DefaultIfEmpty()
						where emp.equipment_id == equipmentId
						select new
						{
							id = emp.id,
							equipment_id = emp.equipment_id,
							part_id = emp.part_id,
							model_no = ((paj != null) ? paj.model_no : ""),
							model_name = ((paj != null) ? paj.model_name : ""),
							part_name = ((paj != null) ? paj.name : ""),
							part_code = ((paj != null) ? paj.code : ""),
							serial_no = pj.serial_no
						}).ToList();
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public bool EditEquipmentPart(int equipmentId, List<dynamic> dataList)
		{
			try
			{
					List<equipment_part> list = _context.equipment_part.Where((equipment_part emp) => emp.equipment_id == equipmentId).ToList();
					foreach (equipment_part item in list)
					{
					_context.equipment_part.Remove(item);
					_context.SaveChanges();
					}
					foreach (dynamic data in dataList)
					{
						equipment_part equipment_part = new equipment_part();
						equipment_part.equipment_id = equipmentId;
						equipment_part.part_id = Convert.ToInt32(data);
						equipment_part entity = equipment_part;
					_context.equipment_part.Add(entity);
					_context.SaveChanges();
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
