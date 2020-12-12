using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApi.Entities;
using WebApi.Models.WorkOrder;
using WebApi.Services;

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class WOExecutionController : ControllerBase
    {
        private IWOExecutionService _woExecutionService;
        private IMapper _mapper;

        public WOExecutionController(
            IWOExecutionService woExecutionService, IMapper mapper)
        {
            _woExecutionService = woExecutionService;
            _mapper = mapper;
        }
        [HttpGet]
        [Route("/WOExecution/GetExecutionById/{id}")]
        public string GetExecutionById(int id)
        {
            object woExecution = _woExecutionService.GetExecutionById(id);
            return JsonConvert.SerializeObject(woExecution);
        }

        [HttpPost]
        [Route("/WOExecution/UpdateWorkExecution")]
        public int UpdateWorkExecution([FromBody]WOExecutionModel model)
        {
            try
            {
                // update work order
                work_order work_order = new work_order();
                work_order.id = model.id;
                work_order.action_taken = model.action;
                work_order.wo_status_id = model.status_id;
                if (model.assign_user != 0)
                {
                    work_order.approve_user_id = model.assign_user;
                    work_order.dt_end_actual = Convert.ToDateTime(model.dateFinish);
                }
                int wo_id = _woExecutionService.EditWorkExecution(work_order);
                return wo_id;
            }
            catch (Exception ex)
            {
                // return error message if there was an exception
                return 0;
            }
        }
    }
}