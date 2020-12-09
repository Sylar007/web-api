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
    public class DashboardController : ControllerBase
    {
        private IDashboardService _dashboardService;

        public DashboardController(
            IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }
        [HttpGet]
        [Route("/Dashboard/GetDashboardList/{id}")]
        public string GetDashboardList(int id)
        {
            IEnumerable<object> dashboardList = _dashboardService.GetDashboardList(id);
            return JsonConvert.SerializeObject(dashboardList);
        }
    }
}