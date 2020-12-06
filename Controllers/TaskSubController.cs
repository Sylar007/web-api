using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using WebApi.Models.Tasks;
using WebApi.Services;

namespace WebApi.Controllers
{
	[Authorize]
	[ApiController]
	[Route("[controller]")]
	public class TaskSubController : ControllerBase
    {
		private ITaskSubService _tasksubService;

		public TaskSubController(
			ITaskSubService tasksubService)
		{
			_tasksubService = tasksubService;
		}
		[HttpPost]
		[Route("/TaskSub/GetTaskSubTree")]
		public string GetTaskSubTree([FromBody]TaskSubModel model)
		{
			dynamic val = _tasksubService.GetTaskSubTree(model.woid, model.equipmentid, model.wotypeid);
			var settings = new JsonSerializerSettings
			{
				ContractResolver = new DefaultContractResolver { NamingStrategy = new LowercaseNamingStrategy() },
			};
			return JsonConvert.SerializeObject(val, settings);
		}
	}
}