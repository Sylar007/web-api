using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApi.Entities;
using WebApi.Services;

namespace WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EquipmentTypeController : ControllerBase
    {
		private IEquipmentTypeService _equipmentTypeService;
		public EquipmentTypeController(
			IEquipmentTypeService equipmentTypeService)
		{
			_equipmentTypeService = equipmentTypeService;
		}
		[HttpPost]
		[HttpGet]
		[Route("EquipmentType/GetEquipmentTypeList")]
		public string GetEquipmentTypeList()
		{
			IEnumerable<object> equipmentTypeList = _equipmentTypeService.GetEquipmentTypeList();
			return JsonConvert.SerializeObject(equipmentTypeList);
		}

		[Route("EquipmentType/GetEquipmentTypeById/{id}")]
		public string GetEquipmentById(int id)
		{
			equipment_type equipmentTypeById = _equipmentTypeService.GetEquipmentTypeById(id);
			return JsonConvert.SerializeObject(equipmentTypeById);
		}

		[HttpPost]
		[Route("EquipmentType/AddEquipmentType")]
		public int AddEquipmentType([FromBody] equipment_type equipmenttype)
		{
			equipmenttype.dt_created = DateTime.Now;
			//equipmenttype.created_by = UserService.GetLoggedInUserId(base.Request);
			return _equipmentTypeService.AddEquipmentType(equipmenttype);
		}

		[HttpPost]
		[Route("EquipmentType/EditEquipmentType")]
		public int EditEquipmentType([FromBody] equipment_type equipmenttype)
		{
			equipmenttype.dt_modified = DateTime.Now;
			//equipmenttype.modified_by = UserService.GetLoggedInUserId(base.Request);
			return _equipmentTypeService.EditEquipmentType(equipmenttype);
		}
	}
}