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
    public class WorkOrderController : ControllerBase
    {
        private IWorkOrderService _workorderService;

        public WorkOrderController(
            IWorkOrderService workorderService)
        {
            _workorderService = workorderService;
        }

        [HttpPost]
        [Route("/WorkOrder/GetPartByWorkOrderNo/{workorderNo}")]
        public string GetPartByWorkOrderNo(string workorderNo)
        {
            object part = _workorderService.GetPartByWorkOrderNo(workorderNo);
            return JsonConvert.SerializeObject(part);
        }
    }
}