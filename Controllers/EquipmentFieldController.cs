using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WebApi.Services;
using Newtonsoft.Json;
using WebApi.Entities;

namespace WebApi.Controllers
{
	[Authorize]
	[Route("[controller]")]
	[ApiController]
    public class EquipmentFieldController : ControllerBase
    {
		private IEquipmentFieldService _equipmentFieldService;
		
		public EquipmentFieldController(
			IEquipmentFieldService equipmentFieldService)
		{
			_equipmentFieldService = equipmentFieldService;
		}

		[HttpGet]
		[Route("/EquipmentField/GetEquipmentFieldList/{equipmentId}")]
		public string GetEquipmentFieldList(int equipmentId)
		{
			IEnumerable<object> equipmentFieldList = _equipmentFieldService.GetEquipmentFieldList(equipmentId, "Spec");
			return JsonConvert.SerializeObject(equipmentFieldList);
		}

		//[HttpPost]
		//[Route("EquipmentField/EditEquipmentField")]
		//public bool EditEquipmentField(dynamic jsonData)
		//{
		//	dynamic val = Convert.ToInt32(jsonData.id);
		//	dynamic val2 = Convert.ToString(jsonData.fieldType);
		//	dynamic val3 = jsonData.fieldList.ToObject<List<object>>();
		//	dynamic val4 = _equipmentFieldService.EditEquipmentField(val, val3, val2);
		//	return val4;
		//}
		[HttpPost]
		[Route("/EquipmentField/CreateEquipmentField")]
		public bool CreateEquipmentField([FromBody]equipment_field equipmentField)
		{
			bool createOperation = _equipmentFieldService.CreateEquipmentField(equipmentField);
			return createOperation;
		}

		[HttpPut]
		[Route("/EquipmentField/UpdateEquipmentField")]
		public bool UpdateEquipmentField(equipment_field equipmentField)
		{
			bool updateOperation = _equipmentFieldService.UpdateEquipmentField(equipmentField);
			return updateOperation;
		}

		[HttpDelete]
		[Route("/EquipmentField/DeleteEquipmentField/{id}")]
		public bool DeleteEquipmentField(int id)
		{
			
			bool deleteOperation = _equipmentFieldService.DeleteEquipmentField(id);
			return deleteOperation;
		}
	}
}