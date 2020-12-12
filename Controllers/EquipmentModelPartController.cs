using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Services;
using WebApi.Entities;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Cors;

namespace WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
	[EnableCors()]
	public class EquipmentModelPartController : ControllerBase
    {
		private IEquipmentModelPartService _equipmentModelPartService;
		public EquipmentModelPartController(
			IEquipmentModelPartService equipmentModelPartService)
		{
			_equipmentModelPartService = equipmentModelPartService;
		}
		[HttpGet]
		[Route("/EquipmentModelPart/GetEquipmentModelPartList/{id}")]
		public string GetEquipmentModelPartList(int id)
		{
			IEnumerable<object> equipmentModelPartList = _equipmentModelPartService.GetEquipmentModelPartList(id);
			return JsonConvert.SerializeObject(equipmentModelPartList);
		}

		[HttpPost]
		[Route("EquipmentModelPart/GetPendingEquipmentModelPartList")]
		public string GetPendingEquipmentModelPartList(dynamic jsonData)
		{
			dynamic val = Convert.ToInt32(jsonData.eqmodel_id);
			dynamic val2 = Convert.ToInt32(jsonData.eq_id);
			dynamic val3 = _equipmentModelPartService.GetPendingEquipmentModelPartList(val, val2);
			dynamic val4 = JsonConvert.SerializeObject(val3);
			return val4;
		}

		[HttpPost]
		[Route("EquipmentModelPart/EditEquipmentModelPart")]
		public bool EditEquipmentModelPart(dynamic jsonData)
		{
			dynamic val = Convert.ToInt32(jsonData.id);
			dynamic val2 = jsonData.dataList.ToObject<List<object>>();
			dynamic val3 = _equipmentModelPartService.EditEquipmentModelPart(val, val2);
			return val3;
		}
	}
}