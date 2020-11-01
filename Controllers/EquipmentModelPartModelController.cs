using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApi.Services;

namespace WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
	[EnableCors()]
	public class EquipmentModelPartModelController : ControllerBase
    {
		private IEquipmentModelPartModelService _equipmentModelPartModelService;
		public EquipmentModelPartModelController(
			IEquipmentModelPartModelService equipmentModelPartModelService)
		{
			_equipmentModelPartModelService = equipmentModelPartModelService;
		}
		[HttpPost]
		[HttpGet]
		[Route("EquipmentModelPartModel/GetEquipmentModelPartModelList/{equipmentModelId}")]
		public string GetEquipmentModelPartModelList(int equipmentModelId)
		{
			IEnumerable<object> equipmentModelPartModelList = _equipmentModelPartModelService.GetEquipmentModelPartModelList(equipmentModelId);
			return JsonConvert.SerializeObject(equipmentModelPartModelList);
		}

		[HttpPost]
		[Route("EquipmentModelPartModel/EditEquipmentModelPartModel")]
		public bool EditEquipmentModelPartModel(dynamic jsonData)
		{
			dynamic val = Convert.ToInt32(jsonData.id);
			dynamic val2 = jsonData.dataList.ToObject<List<object>>();
			dynamic val3 = _equipmentModelPartModelService.EditEquipmentModelPartModel(val, val2);
			return val3;
		}
	}
}