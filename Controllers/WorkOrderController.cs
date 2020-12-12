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
    public class WorkOrderController : ControllerBase
    {
        private IWorkOrderService _workorderService;
        private IMapper _mapper;

        public WorkOrderController(
            IWorkOrderService workorderService, IMapper mapper)
        {
            _workorderService = workorderService;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("/WorkOrder/GetWorkOrderByWorkOrderNo/{workorderNo}")]
        public string GetWorkOrderByWorkOrderNo(string workorderNo)
        {
            object part = _workorderService.GetWorkOrderByWorkOrderNo(workorderNo);
            return JsonConvert.SerializeObject(part);
        }

        [HttpPost]
        [Route("/WorkOrder/GetEditWorkOrderByWorkOrderNo/{workorderNo}")]
        public string GetEditWorkOrderByWorkOrderNo(string workorderNo)
        {
            object part = _workorderService.GetEditWorkOrderByWorkOrderNo(workorderNo);
            return JsonConvert.SerializeObject(part);
        }

        [HttpGet]
        [Route("/WorkOrder/GetPartByWorkOrderNo/{workorderNo}")]
        public string GetPartByWorkOrderNo(string workorderNo)
        {
            object part = _workorderService.GetPartByWorkOrderNo(workorderNo);
            return JsonConvert.SerializeObject(part);
        }

        [HttpGet]
        [Route("/WorkOrder/GetPartByWorkId/{id}")]
        public string GetPartByWorkOrderNo(int id)
        {
            object part = _workorderService.GetPartByWorkId(id);
            return JsonConvert.SerializeObject(part);
        }

        [HttpGet]
        [Route("/WorkOrder/GetWorkOrderList")]
        public string GetWorkOrderList()
        {
            IEnumerable<object> workOrderList = _workorderService.GetWorkOrderList();
            return JsonConvert.SerializeObject(workOrderList);
        }

        [HttpGet]
        [Route("/WorkOrder/GetWorkOrderSelection")]
        public string GetWorkOrderSelection()
        {
            IEnumerable<object> workOrderTypeSelectionList = _workorderService.GetWorkOrderSelection();
            return JsonConvert.SerializeObject(workOrderTypeSelectionList);
        }

        [HttpPost]
        [Route("/WorkOrder/AddWorkOrder")]
        public int AddWorkOrder([FromBody]WorkOrderModel model)
        {
            try
            {
                // create work order
                work_order work_order = new work_order();
                work_order.assignee_user_id = model.asignee_user_id;
                work_order.wo_type_id = model.wo_type_id;
                work_order.equipment_id = model.equipment_id;
                work_order.wo_name = model.wo_name;
                work_order.wo_priority_id = model.wo_priority_id;
                work_order.dt_start_planned = Convert.ToDateTime(model.dt_start_planned);
                work_order.dt_end_planned = Convert.ToDateTime(model.dt_end_planned);
                work_order.remarks = model.remarks;                

                int wo_id = _workorderService.AddWorkOrder(work_order);
                return wo_id;
            }
            catch (Exception ex)
            {
                // return error message if there was an exception
                return 0;
            }
        }

        [HttpPost]
        [Route("/WorkOrder/UpdateWorkOrder")]
        public int UpdateWorkOrder([FromBody]WorkOrderModel model)
        {
            try
            {
                // update work order
                work_order work_order = new work_order();
                work_order.id = model.id;
                work_order.assignee_user_id = model.asignee_user_id;
                work_order.wo_type_id = model.wo_type_id;
                work_order.equipment_id = model.equipment_id;
                work_order.wo_name = model.wo_name;
                work_order.wo_priority_id = model.wo_priority_id;
                work_order.dt_start_planned = Convert.ToDateTime(model.dt_start_planned);
                work_order.dt_end_planned = Convert.ToDateTime(model.dt_end_planned);
                work_order.remarks = model.remarks;

                int wo_id = _workorderService.EditWorkOrder(work_order);
                return wo_id;
            }
            catch (Exception ex)
            {
                // return error message if there was an exception
                return 0;
            }
        }

        [HttpPost]
        [Route("/WorkOrder/UpdateTaskSubTree/{woid}")]
        public bool UpdateTaskSubTree(int woid,[FromBody]List<EventModel> model)
        {
            try
            {
                //// update work order
                //work_order work_order = new work_order();
                //work_order.id = model.id;
                //work_order.assignee_user_id = model.asignee_user_id;
                //work_order.wo_type_id = model.wo_type_id;
                //work_order.equipment_id = model.equipment_id;
                //work_order.wo_name = model.wo_name;
                //work_order.wo_priority_id = model.wo_priority_id;
                //work_order.dt_start_planned = Convert.ToDateTime(model.dt_start_planned);
                //work_order.dt_end_planned = Convert.ToDateTime(model.dt_end_planned);
                //work_order.remarks = model.remarks;
                
                return _workorderService.EditSubTaskTree(woid, model); ;
            }
            catch (Exception ex)
            {
                // return error message if there was an exception
                return false;
            }
        }

        [HttpPost]
        [Route("/WorkOrder/UpdateTaskExecutionSubTree/{woid}")]
        public bool UpdateTaskExecutionSubTree(int woid, [FromBody]List<EventModel> model)
        {
            try
            {
                return _workorderService.EditSubTaskExecutionTree(woid, model); ;
            }
            catch (Exception ex)
            {
                // return error message if there was an exception
                return false;
            }
        }
    }
}