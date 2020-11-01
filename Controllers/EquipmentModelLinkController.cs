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
	public class EquipmentModelLinkController : ControllerBase
    {
		private IEquipmentModelLinkService _equipmentModelLinkService;
		public EquipmentModelLinkController(
			IEquipmentModelLinkService equipmentModelLinkService)
		{
			_equipmentModelLinkService = equipmentModelLinkService;
		}
		[HttpPost]
		[HttpGet]
		[Route("EquipmentModelLink/GetEquipmentModelLinkList/{equipmentModelId}")]
		public string GetEquipmentModelLinkList(int equipmentModelId)
		{
			IEnumerable<object> equipmentModelLinkList = _equipmentModelLinkService.GetEquipmentModelLinkList(equipmentModelId, "EquipmentModel");
			return JsonConvert.SerializeObject(equipmentModelLinkList);
		}

		[HttpPost]
		[Route("EquipmentModelLink/EditEquipmentModelLink")]
		public bool EditEquipmentModelLink(dynamic jsonData)
		{
			dynamic val = Convert.ToInt32(jsonData.id);
			dynamic val2 = Convert.ToString(jsonData.linkType);
			dynamic val3 = jsonData.linkList.ToObject<List<object>>();
			dynamic val4 = _equipmentModelLinkService.EditEquipmentModelLink(val, val3, val2);
			return val4;
		}
	}
}