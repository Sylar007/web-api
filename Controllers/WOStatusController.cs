using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApi.Entities;
using WebApi.Services;

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class WOStatusController : ControllerBase
    {
        private IWOStatusService _woStatusService;

        public WOStatusController(IWOStatusService woStatusService)
        {
            _woStatusService = woStatusService;
        }
        [HttpGet]
        [Route("/WOStatus/GetWOStatusList")]
        public string GetWOStatusList()
        {
            IEnumerable<wo_status> wOPriority = _woStatusService.GetWOStatusList();
            return JsonConvert.SerializeObject(wOPriority);
        }
    }
}