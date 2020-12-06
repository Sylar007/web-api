using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApi.Entities;
using WebApi.Models.Equipment;
using WebApi.Services;

namespace WebApi.Controllers
{
	[Authorize]
	[ApiController]
	[Route("[controller]")]
	public class EquipmentTypeController : ControllerBase
    {
		private IEquipmentTypeService _equipmentTypeService;
		public EquipmentTypeController(
			IEquipmentTypeService equipmentTypeService)
		{
			_equipmentTypeService = equipmentTypeService;
		}
		[HttpGet]
		[Route("/EquipmentType/GetEquipmentTypeList")]
		public string GetEquipmentTypeList()
		{
			IEnumerable<object> equipmentTypeList = _equipmentTypeService.GetEquipmentTypeList();
			return JsonConvert.SerializeObject(equipmentTypeList);
		}
		[HttpGet]
		[Route("/EquipmentType/GetEquipmentTypeById/{id}")]
		public string GetEquipmentById(int id)
		{
			equipment_type equipmentTypeById = _equipmentTypeService.GetEquipmentTypeById(id);
			return JsonConvert.SerializeObject(equipmentTypeById);
		}

		[HttpPost]
		[Route("/EquipmentType/AddEquipmentType")]
		public int AddEquipmentType([FromBody] EquipmentType_Model equipmenttype)
		{
			equipment_type equipment_type = new equipment_type();
			equipment_type.name = equipmenttype.name;
			equipment_type.description = equipmenttype.description;
			equipment_type.dt_created = DateTime.Now;
			equipment_type.dt_modified = DateTime.Now;
			int idClaim = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type.Equals("assigned_User_Id", StringComparison.InvariantCultureIgnoreCase)).Value);
			equipment_type.created_by = idClaim;
			equipment_type.modified_by = idClaim;
			return _equipmentTypeService.AddEquipmentType(equipment_type);
		}

		[HttpPost]
		[Route("/EquipmentType/UpdateEquipmentType")]
		public int UpdateEquipmentType([FromBody] EquipmentType_Model equipmenttype)
		{
			equipment_type equipment_type = new equipment_type();
			equipment_type.id = equipmenttype.id;
			equipment_type.name = equipmenttype.name;
			equipment_type.description = equipmenttype.description;
			equipment_type.dt_modified = DateTime.Now;
			int idClaim = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type.Equals("assigned_User_Id", StringComparison.InvariantCultureIgnoreCase)).Value);
			equipment_type.modified_by = idClaim; ;
			return _equipmentTypeService.EditEquipmentType(equipment_type);
		}
	}
}