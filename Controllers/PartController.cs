using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WebApi.Services;
using WebApi.Entities;
using Newtonsoft.Json;

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class PartController : ControllerBase
    {
		private IPartService _partService;

		public PartController(
			IPartService partService)
		{
			_partService = partService;
		}		

		[HttpPost]
		[Route("/Part/GetPartBySerialNo/{serialNo}")]
		public string GetPartBySerialNo(string serialNo)
		{
			object part = _partService.GetPartBySerialNo(serialNo);
			return JsonConvert.SerializeObject(part);
		}

		[HttpGet]
		[Route("/part/GetPartList")]
		public string GetPartList()
		{
			IEnumerable<object> part = _partService.GetPartList();
			return JsonConvert.SerializeObject(part);
		}

		[HttpGet]
		[Route("/Part/GetPartById/{id}")]
		public string GetPartById(int id)
		{
			object part = _partService.GetPartById(id);
			return JsonConvert.SerializeObject(part);
		}
		

		//[HttpPost]
		//[Route("/Equipment/GetEquipmentById/{id}")]
		//public string GetEquipmentById(int id)
		//{
		//	dynamic equipmentById = _equipmentService.GetEquipmentById(id);
		//	dynamic val = JsonConvert.SerializeObject(equipmentById);
		//	return val;
		//}

		//[HttpPost]
		//[Route("Equipment/GetEquipmentBySerialNo")]
		//public string GetEquipmentBySerialNo([FromBody] equipment equipment)
		//{
		//	equipment equipmentBySerialNo = _equipmentService.GetEquipmentBySerialNo(equipment);
		//	return JsonConvert.SerializeObject(equipmentBySerialNo);
		//}

		//[HttpPost]
		//[Route("Equipment/GetEquipmentByNo")]
		//public string GetEquipmentByNo([FromBody] equipment equipment)
		//{
		//	equipment equipmentByNo = _equipmentService.GetEquipmentByNo(equipment);
		//	return JsonConvert.SerializeObject(equipmentByNo);
		//}

		//[Route("Equipment/GetEquipmentRepairReplaceById/{id}")]
		//public string GetEquipmentRepairReplaceById(int id)
		//{
		//	dynamic equipmentRepairReplaceById = _equipmentService.GetEquipmentRepairReplaceById(id);
		//	dynamic val = JsonConvert.SerializeObject(equipmentRepairReplaceById);
		//	return val;
		//}

		//[HttpPost]
		//[Route("Equipment/AddEquipment")]
		//public int AddEquipment([FromBody] equipment equipment)
		//{
		//	equipment.is_deleted = 0;
		//	equipment.dt_created = DateTime.Now;
		//	//equipment.created_by = _userService.GetLoggedInUserId(base.Request);
		//	return _equipmentService.AddEquipment(equipment);
		//}

		//[HttpPost]
		//[Route("Equipment/EditEquipment")]
		//public int EditEquipment([FromBody] equipment equipment)
		//{
		//	equipment.dt_modified = DateTime.Now;
		//	//equipment.modified_by = UserService.GetLoggedInUserId(base.Request);
		//	return _equipmentService.EditEquipment(equipment);
		//}

		//[HttpPost]
		//[Route("Equipment/GetHomeTotalEqLocationChart")]
		//public string GetHomeTotalEquipmentLocationChart()
		//{
		//	IEnumerable<object> homeTotalEqLocationChart = _equipmentService.GetHomeTotalEqLocationChart();
		//	return JsonConvert.SerializeObject(homeTotalEqLocationChart);
		//}

		//[HttpPost]
		//[Route("Equipment/GetHomeTotalEqProcessChart")]
		//public string GetHomeTotalEquipmentProcessChart()
		//{
		//	IEnumerable<object> homeTotalEqProcessChart = _equipmentService.GetHomeTotalEqProcessChart();
		//	return JsonConvert.SerializeObject(homeTotalEqProcessChart);
		//}
	}
}