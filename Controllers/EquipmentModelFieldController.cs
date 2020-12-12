using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
	[Authorize]
	[Route("api/[controller]")]
    [ApiController]
	[EnableCors()]
	public class EquipmentModelFieldController : ControllerBase
    {
		private IEquipmentModelFieldService _equipmentModelFieldService;
		public EquipmentModelFieldController(
			IEquipmentModelFieldService equipmentModelFieldService)
		{
			_equipmentModelFieldService = equipmentModelFieldService;
		}
		[HttpGet]
		[Route("/EquipmentModelField/GetEquipmentModelFieldList/{equipmentModelId}")]
		public string GetEquipmentModelFieldList(int equipmentModelId)
		{
			IEnumerable<object> equipmentModelFieldList = _equipmentModelFieldService.GetEquipmentModelFieldList(equipmentModelId, "Spec");
			return JsonConvert.SerializeObject(equipmentModelFieldList);
		}

		[HttpPost]
		[Route("/EquipmentModelField/CreateEquipmentModelField")]
		public bool CreateEquipmentModelField([FromBody]equipment_model_field equipmentmodelField)
		{
			equipmentmodelField.field_type = "Spec";
			bool createOperation = _equipmentModelFieldService.CreateEquipmentModelField(equipmentmodelField);
			return createOperation;
		}

		[HttpPut]
		[Route("/EquipmentModelField/UpdateEquipmentModelField")]
		public bool UpdatePartModelField(equipment_model_field equipmentmodelField)
		{
			equipmentmodelField.field_type = "Spec";
			bool updateOperation = _equipmentModelFieldService.UpdateEquipmentModelField(equipmentmodelField);
			return updateOperation;
		}

		[HttpDelete]
		[Route("/EquipmentModelField/DeleteEquipmentModelField/{id}")]
		public bool DeleteEquipmentModelField(int id)
		{

			bool deleteOperation = _equipmentModelFieldService.DeleteEquipmentModelField(id);
			return deleteOperation;
		}
	}
}