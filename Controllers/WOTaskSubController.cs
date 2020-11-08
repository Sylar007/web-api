using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApi.Services;

namespace WebApi.Controllers
{
	[Authorize]
	[ApiController]
	[Route("[controller]")]
	public class WOTaskSubController : ControllerBase
    {
		private IWOTaskSubService _woTaskSubService;

		public WOTaskSubController(
			IWOTaskSubService woTaskSubService)
		{
			_woTaskSubService = woTaskSubService;
		}
		[HttpPost]
		[Route("/WOTaskSub/GetWOTaskSubTree")]
		public string GetWOTaskTree(int wo_id, string task_type)
		{
			return JsonConvert.SerializeObject(_woTaskSubService.GetWOTaskSubList(wo_id, task_type));
		}
	}	
}