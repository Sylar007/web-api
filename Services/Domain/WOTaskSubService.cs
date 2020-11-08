using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Entities;
using WebApi.Helpers;

namespace WebApi.Services
{
    public class WOTaskSubService : IWOTaskSubService
	{
		private DataContext _context;

		public WOTaskSubService(DataContext context)
		{
			_context = context;
		}
		public IEnumerable<dynamic> GetWOTaskSubList(int woId, string wo_task_type)
		{
			try
			{
				if (woId == 0)
				{
					return new List<wo_task_sub>();
				}
				var item = (from wts in _context.wo_task_sub
							join ts in _context.task_sub on wts.task_sub_id equals ts.id into tsJoin
							from tsj in tsJoin.DefaultIfEmpty()
							where wts.wo_id == woId && wts.wo_task_type == wo_task_type
							orderby wts.task_sub_id
							select new
							{
								id = wts.id,
								task_sub_id = wts.task_sub_id,
								wo_task_type = wts.wo_task_type,
								wo_id = wts.wo_id,
								task_id = ((tsj != null) ? tsj.task_id : 0),
								name = ((tsj != null) ? tsj.name : "")
							}).ToList();
				var item2 = (from wts in _context.wo_task_sub_file
							 where wts.wo_id == woId
							 orderby wts.task_sub_id
							 select new
							 {
								 wts.task_sub_id,
								 wts.file_type
							 }).ToList();
				return new List<object>
			{
				item,
				item2
			};
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

	}
}
