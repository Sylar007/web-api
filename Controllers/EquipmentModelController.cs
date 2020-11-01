using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using WebApi.Entities;
using WebApi.Helpers;
using WebApi.Services;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
	[EnableCors()]
	public class EquipmentModelController : ControllerBase
    {
		private IEquipmentModelService _equipmentModelService;

		public EquipmentModelController(
			IEquipmentModelService equipmentModelService)
		{
			_equipmentModelService = equipmentModelService;
		}
		[HttpPost]
		[HttpGet]
		[Route("EquipmentModel/GetEquipmentModelList")]
		public string GetEquipmentModelList()
		{
			IEnumerable<object> equipmentModelList = _equipmentModelService.GetEquipmentModelList();
			return JsonConvert.SerializeObject(equipmentModelList);
		}

		[Route("EquipmentModel/GetEquipmentModelProcessList")]
		public string GetEquipmentModelProcessList()
		{
			IEnumerable<object> equipmentModelProcessList = _equipmentModelService.GetEquipmentModelProcessList();
			return JsonConvert.SerializeObject(equipmentModelProcessList);
		}

		[Route("EquipmentModel/GetEquipmentModelById/{id}")]
		public string GetEquipmentModelById(int id)
		{
			dynamic equipmentModelById = _equipmentModelService.GetEquipmentModelById(id);
			dynamic val = JsonConvert.SerializeObject(equipmentModelById);
			return val;
		}

		[HttpPost]
		[Route("EquipmentModel/AddEquipmentModel")]
		public int AddEquipmentModel([FromBody] equipment_model equipmentmodel)
		{
			equipmentmodel.is_deleted = 0;
			equipmentmodel.dt_created = DateTime.Now;
			//equipmentmodel.created_by = _userService.GetLoggedInUserId(base.Request);
			return _equipmentModelService.AddEquipmentModel(equipmentmodel);
		}

		[HttpPost]
		[Route("EquipmentModel/EditEquipmentModel")]
		public int EditEquipmentModel([FromBody] equipment_model equipmentmodel)
		{
			equipmentmodel.dt_modified = DateTime.Now;
			//equipmentmodel.modified_by = UserService.GetLoggedInUserId(base.Request);
			return _equipmentModelService.EditEquipmentModel(equipmentmodel);
		}

		[HttpPost]
		[Route("EquipmentModel/GetTotalEquipmentModel")]
		public string GetTotalEquipmentModel()
		{
			IEnumerable<object> totalEquipmentModel = _equipmentModelService.GetTotalEquipmentModel();
			return JsonConvert.SerializeObject(totalEquipmentModel);
		}
	}
}