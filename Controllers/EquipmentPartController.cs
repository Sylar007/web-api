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
    [Route("api/[controller]")]
    [ApiController]
	[EnableCors()]
	public class EquipmentPartController : ControllerBase
    {
		private IEquipmentPartService _equipmentPartService;
		public EquipmentPartController(
			IEquipmentPartService equipmentPartService)
		{
			_equipmentPartService = equipmentPartService;
		}
		[HttpGet]
		[Route("/EquipmentPart/GetEquipmentPartList/{equipmentId}")]
		public string GetEquipmentPartList(int equipmentId)
		{
			IEnumerable<object> equipmentPartList = _equipmentPartService.GetEquipmentPartList(equipmentId);
			return JsonConvert.SerializeObject(equipmentPartList);
		}

		[HttpPost]
		[Route("EquipmentPart/EditEquipmentPart")]
		public bool EditEquipmentPart(dynamic jsonData)
		{
			dynamic val = Convert.ToInt32(jsonData.id);
			dynamic val2 = jsonData.dataList.ToObject<List<object>>();
			dynamic val3 = _equipmentPartService.EditEquipmentPart(val, val2);
			return val3;
		}
	}
}