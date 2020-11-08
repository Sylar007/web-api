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
	public class EquipmentController : ControllerBase
    {
		private IEquipmentService _equipmentService;

		public EquipmentController(
			IEquipmentService equipmentService)
		{
			_equipmentService = equipmentService;
		}

		[HttpPost]
		[HttpGet]
		[Route("Equipment/GetEquipmentList")]
		public string GetEquipmentList()
		{
			IEnumerable<object> equipmentList = _equipmentService.GetEquipmentList();
			return JsonConvert.SerializeObject(equipmentList);
		}

		[Route("Equipment/GetEquipmentLocationList")]
		public string GetEquipmentLocationList()
		{
			IEnumerable<object> equipmentLocationList = _equipmentService.GetEquipmentLocationList();
			return JsonConvert.SerializeObject(equipmentLocationList);
		}

		[HttpPost]
		[HttpGet]
		[Route("Equipment/GetEquipmentRepairReplaceList")]
		public string GetEquipmentRepairReplaceList()
		{
			IEnumerable<object> equipmentRepairReplaceList = _equipmentService.GetEquipmentRepairReplaceList();
			return JsonConvert.SerializeObject(equipmentRepairReplaceList);
		}

		[HttpPost("GetEquipmentListById")]
		public string GetEquipmentListById(int id)
		{
			IEnumerable<object> equipmentListById = _equipmentService.GetEquipmentListById(id);
			return JsonConvert.SerializeObject(equipmentListById);
		}

		[HttpPost]
		[Route("/Equipment/GetEquipmentById/{id}")]
		public string GetEquipmentById(int id)
		{
			dynamic equipmentById = _equipmentService.GetEquipmentById(id);
			dynamic val = JsonConvert.SerializeObject(equipmentById);
			return val;
		}

		[HttpPost]
		[Route("/Equipment/GetEquipmentBySerialNo/{serialNo}")]
		public string GetEquipmentBySerialNo(string serialNo)
		{
			object equipmentBySerialNo = _equipmentService.GetEquipmentBySerialNo(serialNo);
			return JsonConvert.SerializeObject(equipmentBySerialNo);
		}

		[HttpPost]
		[Route("Equipment/GetEquipmentByNo")]
		public string GetEquipmentByNo([FromBody] equipment equipment)
		{
			equipment equipmentByNo = _equipmentService.GetEquipmentByNo(equipment);
			return JsonConvert.SerializeObject(equipmentByNo);
		}

		[Route("Equipment/GetEquipmentRepairReplaceById/{id}")]
		public string GetEquipmentRepairReplaceById(int id)
		{
			dynamic equipmentRepairReplaceById = _equipmentService.GetEquipmentRepairReplaceById(id);
			dynamic val = JsonConvert.SerializeObject(equipmentRepairReplaceById);
			return val;
		}

		[HttpPost]
		[Route("Equipment/AddEquipment")]
		public int AddEquipment([FromBody] equipment equipment)
		{
			equipment.is_deleted = 0;
			equipment.dt_created = DateTime.Now;
			//equipment.created_by = _userService.GetLoggedInUserId(base.Request);
			return _equipmentService.AddEquipment(equipment);
		}

		[HttpPost]
		[Route("Equipment/EditEquipment")]
		public int EditEquipment([FromBody] equipment equipment)
		{
			equipment.dt_modified = DateTime.Now;
			//equipment.modified_by = UserService.GetLoggedInUserId(base.Request);
			return _equipmentService.EditEquipment(equipment);
		}

		[HttpPost]
		[Route("Equipment/GetHomeTotalEqLocationChart")]
		public string GetHomeTotalEquipmentLocationChart()
		{
			IEnumerable<object> homeTotalEqLocationChart = _equipmentService.GetHomeTotalEqLocationChart();
			return JsonConvert.SerializeObject(homeTotalEqLocationChart);
		}

		[HttpPost]
		[Route("Equipment/GetHomeTotalEqProcessChart")]
		public string GetHomeTotalEquipmentProcessChart()
		{
			IEnumerable<object> homeTotalEqProcessChart = _equipmentService.GetHomeTotalEqProcessChart();
			return JsonConvert.SerializeObject(homeTotalEqProcessChart);
		}

		[HttpGet]
		[Route("/Equipment/GetEquipmentSelection")]
		public string GetEquipmentSelection()
		{
			IEnumerable<object> equipmentSelectionList = _equipmentService.GetEquipmentSelection();
			return JsonConvert.SerializeObject(equipmentSelectionList);
		}
	}
}