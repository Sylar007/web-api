using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Entities;
using WebApi.Helpers;

namespace WebApi.Services
{
	public class EquipmentTypeService : IEquipmentTypeService
	{
		private DataContext _context;

		public EquipmentTypeService(DataContext context)
		{
			_context = context;
		}
		public IEnumerable<dynamic> GetEquipmentTypeList()
		{
			try
			{
				return (from at in _context.equipment_type
						orderby at.name
						select new
						{
							at.id,
							at.name,
							at.description
						}).ToList();
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public equipment_type GetEquipmentTypeById(int id)
		{
			try
			{
				if (id == 0)
				{
					return new equipment_type();
				}				
				IQueryable<equipment_type> source = _context.equipment_type.Where((equipment_type at) => at.id == id);
				return source.First();
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public int AddEquipmentType(equipment_type data)
		{
			try
			{
				_context.equipment_type.Add(data);
				int num = _context.SaveChanges();
				if (num > 0)
				{
					return data.id;
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return data.id;
		}

		public int EditEquipmentType(equipment_type data)
		{
			try
			{				
				equipment_type equipment_type = _context.equipment_type.Where((equipment_type at) => at.id == data.id).First();
				equipment_type.name = data.name;
				equipment_type.description = data.description;
				equipment_type.modified_by = data.modified_by;
				equipment_type.dt_modified = data.dt_modified;
				int num = _context.SaveChanges();
				if (num > 0)
				{
					return data.id;
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return data.id;
		}
	}

}
