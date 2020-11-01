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
		[HttpPost]
		[HttpGet]
		[Route("EquipmentModelField/GetEquipmentModelFieldList/{equipmentModelId}")]
		public string GetEquipmentModelFieldList(int equipmentModelId)
		{
			IEnumerable<object> equipmentModelFieldList = _equipmentModelFieldService.GetEquipmentModelFieldList(equipmentModelId, "Spec");
			return JsonConvert.SerializeObject(equipmentModelFieldList);
		}

		[HttpPost]
		[Route("EquipmentModelField/EditEquipmentModelField")]
		public bool EditEquipmentModelField(dynamic jsonData)
		{
			dynamic val = Convert.ToInt32(jsonData.id);
			dynamic val2 = Convert.ToString(jsonData.fieldType);
			dynamic val3 = jsonData.fieldList.ToObject<List<object>>();
			dynamic val4 = _equipmentModelFieldService.EditEquipmentModelField(val, val3, val2);
			return val4;
		}
	}
}