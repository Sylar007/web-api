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
    public class WOPriorityController : ControllerBase
    {
        private IWOPriorityService _wopriorityService;

        public WOPriorityController(
            IWOPriorityService wopriorityService)
        {
            _wopriorityService = wopriorityService;
        }
        [HttpGet]
        [Route("/WOPriority/GetPrioritySelection")]
        public string GetPrioritySelection()
        {
            IEnumerable<wo_priority> wOPriority = _wopriorityService.GetWOPriority();
            return JsonConvert.SerializeObject(wOPriority);
        }
    }
}