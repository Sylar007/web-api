using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApi.Entities;
using WebApi.Services;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
	[EnableCors()]
	public class EquipmentStatusController : ControllerBase
    {
		private IEquipmentStatusService _equipmentStatusService;
		public EquipmentStatusController(
			IEquipmentStatusService equipmentStatusService)
		{
			_equipmentStatusService = equipmentStatusService;
		}
		[HttpGet]
		[Route("/EquipmentStatus/GetEquipmentStatusList")]
		public string GetEquipmentStatusList()
		{
			IEnumerable<object> equipmentStatusList = _equipmentStatusService.GetEquipmentStatusList();
			return JsonConvert.SerializeObject(equipmentStatusList);
		}

		[Route("/EquipmentStatus/GetEquipmentStatusById/{id}")]
		public string GetEquipmentStatusById(int id)
		{
			equipment_status equipmentStatusById = _equipmentStatusService.GetEquipmentStatusById(id);
			return JsonConvert.SerializeObject(equipmentStatusById);
		}
	}
}