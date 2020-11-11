using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Entities;
using WebApi.Helpers;

namespace WebApi.Services
{
    public class MediaService : IMediaService
	{
		private DataContext _context;
		public MediaService(DataContext context)
		{
			_context = context;
		}
		public int AddMedia(media data)
		{
			try
			{
				_context.media.Add(data);
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
			return -1;
		}
	}
}
