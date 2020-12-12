using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Entities;
using WebApi.Helpers;

namespace WebApi.Services
{
    public class WOStatusService : IWOStatusService
    {
        private DataContext _context;

        public WOStatusService(DataContext context)
        {
            _context = context;
        }
		public IEnumerable<wo_status> GetWOStatusList()
		{
			try
			{
				IOrderedQueryable<wo_status> source = _context.wo_status.OrderBy((wo_status us) => us.name);
				return source.ToList();
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}
