using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using WebApi.Helpers;
using WebApi.Services;

namespace WebApi.Controllers
{
	[Authorize]
	[Route("[controller]")]
    [ApiController]
	[EnableCors()]
	public class EquipmentLinkController : ControllerBase
    {
		private IEquipmentLinkService _equipmentLinkService;

		public EquipmentLinkController(
			IEquipmentLinkService equipmentLinkService)
		{
			_equipmentLinkService = equipmentLinkService;
		}
		[HttpPost]
		[HttpGet]
		[Route("EquipmentLink/GetEquipmentLinkList/{equipmentId}")]
		public string GetEquipmentLinkList(int equipmentId)
		{
			IEnumerable<object> equipmentLinkList = _equipmentLinkService.GetEquipmentLinkList(equipmentId, "Equipment");
			return JsonConvert.SerializeObject(equipmentLinkList);
		}

		[HttpPost]
		[Route("EquipmentLink/EditEquipmentLink")]
		public bool EditEquipmentLink(dynamic jsonData)
		{
			dynamic val = Convert.ToInt32(jsonData.id);
			dynamic val2 = Convert.ToString(jsonData.linkType);
			dynamic val3 = jsonData.linkList.ToObject<List<object>>();
			dynamic val4 = _equipmentLinkService.EditEquipmentLink(val, val3, val2);
			return val4;
		}
	}
}