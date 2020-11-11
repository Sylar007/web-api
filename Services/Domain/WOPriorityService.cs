using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Entities;
using WebApi.Helpers;

namespace WebApi.Services
{
    public class WOPriorityService : IWOPriorityService
	{
		private DataContext _context;

		public WOPriorityService(DataContext context)
		{
			_context = context;
		}
		public IEnumerable<wo_priority> GetWOPriority()
		{
			try
			{
				IOrderedQueryable<wo_priority> source = _context.wo_priority.OrderBy((wo_priority us) => us.name);
				return source.ToList();
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}
