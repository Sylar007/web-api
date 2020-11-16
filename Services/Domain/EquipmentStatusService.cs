using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Entities;
using WebApi.Helpers;

namespace WebApi.Services
{
	public class EquipmentStatusService : IEquipmentStatusService
	{
		private DataContext _context;

		public EquipmentStatusService(DataContext context)
		{
			_context = context;
		}
		public IEnumerable<dynamic> GetEquipmentStatusList()
		{
			try
			{				
				return _context.equipment_status.OrderBy((equipment_status cnt) => cnt.name).ToList();
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public equipment_status GetEquipmentStatusById(int id)
		{
			try
			{
				if (id == 0)
				{
					return new equipment_status();
				}				
				IQueryable<equipment_status> source = _context.equipment_status.Where((equipment_status ctn) => ctn.id == id);
				return source.First();
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}

}
