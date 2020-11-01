using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApi.Services;

namespace WebApi.Controllers
{
	[Authorize]
	[ApiController]
	[Route("[controller]")]
	public class QrLinkController : ControllerBase
    {
		private IQrLinkService _qrLinkService;

		public QrLinkController(
			IQrLinkService qrLinkService)
		{
			_qrLinkService = qrLinkService;
		}

		[HttpPost]
		[Route("/QrLink/GetTypeBySerialNo/{serialNo}")]
		public string GetTypeBySerialNo(string serialNo)
		{
			object qrLink = _qrLinkService.GetPartBySerialNo(serialNo);
			return JsonConvert.SerializeObject(qrLink);
		}
	}
}