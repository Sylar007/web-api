using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Helpers;

namespace WebApi.Services
{
    public class TaskSubService : ITaskSubService
	{
		private DataContext _context;

		public TaskSubService(DataContext context)
		{
			_context = context;
		}
		public IEnumerable<dynamic> GetTaskSubTree(int equipment_id, int wo_type_id)
		{
			try
			{
				return (from ts in _context.task_sub
						join t in _context.task on ts.task_id equals t.id into tJoin
						from tj in tJoin.DefaultIfEmpty()
						join e in _context.equipments on tj.equipment_model_id equals e.equipment_model_id into eJoin
						from ej in eJoin.DefaultIfEmpty()
						where ej.id == equipment_id && tj.wo_type_id == wo_type_id
						orderby tj.task_no
						select new
						{
							id = ts.id,
							task_id = ts.task_id,
							name = ts.name,
							task_full_name = string.Concat(((tj != null) ? tj.task_no : "") + " - ", (tj != null) ? tj.name : ""),
							task_sub_id = ts.id
						}).ToList();
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}
