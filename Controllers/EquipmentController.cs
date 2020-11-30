using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WebApi.Services;
using WebApi.Entities;
using Newtonsoft.Json;
using WebApi.Models.Equipment;

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class EquipmentController : ControllerBase
    {
        private IEquipmentService _equipmentService;

        public EquipmentController(
            IEquipmentService equipmentService)
        {
            _equipmentService = equipmentService;
        }

        [HttpPost]
        [HttpGet]
        [Route("/Equipment/GetEquipmentList")]
        public string GetEquipmentList()
        {
            IEnumerable<object> equipmentList = _equipmentService.GetEquipmentList();
            return JsonConvert.SerializeObject(equipmentList);
        }

        [HttpGet]
        [Route("/Equipment/GetEquipmentLog/{equipmentId}")]
        public string GetEquipmentLog(int equipmentId)
        {
            IEnumerable<object> equipmentList = _equipmentService.GetEquipmentLog(equipmentId);
            return JsonConvert.SerializeObject(equipmentList);
        }        

        [Route("Equipment/GetEquipmentLocationList")]
        public string GetEquipmentLocationList()
        {
            IEnumerable<object> equipmentLocationList = _equipmentService.GetEquipmentLocationList();
            return JsonConvert.SerializeObject(equipmentLocationList);
        }

        [HttpPost]
        [HttpGet]
        [Route("Equipment/GetEquipmentRepairReplaceList")]
        public string GetEquipmentRepairReplaceList()
        {
            IEnumerable<object> equipmentRepairReplaceList = _equipmentService.GetEquipmentRepairReplaceList();
            return JsonConvert.SerializeObject(equipmentRepairReplaceList);
        }

        [HttpPost("GetEquipmentListById")]
        public string GetEquipmentListById(int id)
        {
            IEnumerable<object> equipmentListById = _equipmentService.GetEquipmentListById(id);
            return JsonConvert.SerializeObject(equipmentListById);
        }

        [HttpPost]
        [Route("/Equipment/GetEquipmentById/{id}")]
        public string GetEquipmentById(int id)
        {
            dynamic equipmentById = _equipmentService.GetEquipmentById(id);
            dynamic val = JsonConvert.SerializeObject(equipmentById);
            return val;
        }

        [HttpPost]
        [Route("/Equipment/GetEquipmentBySerialNo/{serialNo}")]
        public string GetEquipmentBySerialNo(string serialNo)
        {
            object equipmentBySerialNo = _equipmentService.GetEquipmentBySerialNo(serialNo);
            return JsonConvert.SerializeObject(equipmentBySerialNo);
        }

        [HttpGet]
        [Route("/Equipment/OpenEquipment/{equipment_no}")]
        public string OpenEquipment(string equipment_no)
        {
            object equipment = _equipmentService.OpenEquipment(equipment_no);
            return JsonConvert.SerializeObject(equipment);
        }

        [HttpPost]
        [Route("Equipment/GetEquipmentByNo")]
        public string GetEquipmentByNo([FromBody] equipment equipment)
        {
            equipment equipmentByNo = _equipmentService.GetEquipmentByNo(equipment);
            return JsonConvert.SerializeObject(equipmentByNo);
        }

        [Route("Equipment/GetEquipmentRepairReplaceById/{id}")]
        public string GetEquipmentRepairReplaceById(int id)
        {
            dynamic equipmentRepairReplaceById = _equipmentService.GetEquipmentRepairReplaceById(id);
            dynamic val = JsonConvert.SerializeObject(equipmentRepairReplaceById);
            return val;
        }

        [HttpPost]
        [Route("/Equipment/AddEquipment")]
        public int AddEquipment([FromBody]EquipmentModel model)
        {
            try
            {
                // create work equipment
                equipment equipment = new equipment();
                if (!string.IsNullOrWhiteSpace(model.acquisitionDate))
                {
                    equipment.dt_acquisition = Convert.ToDateTime(model.acquisitionDate);
                }
                if (!string.IsNullOrWhiteSpace(model.commissioningDate))
                {
                    equipment.dt_commissioning = Convert.ToDateTime(model.commissioningDate);
                }
                if (!string.IsNullOrWhiteSpace(model.deliveryDate))
                {
                    equipment.dt_site_delivery = Convert.ToDateTime(model.deliveryDate);
                }
                if (!string.IsNullOrWhiteSpace(model.installationDate))
                {
                    equipment.dt_installation = Convert.ToDateTime(model.installationDate);
                }
                if (!string.IsNullOrWhiteSpace(model.warrantyDate))
                {
                    equipment.dt_warranty_exp = Convert.ToDateTime(model.warrantyDate);
                }
                equipment.mfg_year = Convert.ToInt32(model.manufacture_year);
                equipment.equipment_model_id = model.model_id;
                equipment.sales_contact_name = model.sales_name;
                equipment.sales_contact_no = model.sales_no;
                equipment.serial_no = model.serial_no;
                equipment.equipment_status_id = model.status_id;
                equipment.support_contact_name = model.support_name;
                equipment.support_contact_no = model.support_no;

                int equipment_id = _equipmentService.AddEquipment(equipment);
                return equipment_id;
            }
            catch (Exception ex)
            {
                // return error message if there was an exception
                return 0;
            }
        }

        [HttpPost]
        [Route("/Equipment/UpdateEquipment")]
        public int UpdateEquipment([FromBody]EquipmentModel model)
        {
            try
            {
                // update equipment
                equipment equipment = new equipment();
                if (!string.IsNullOrWhiteSpace(model.acquisitionDate))
                {
                    equipment.dt_acquisition = Convert.ToDateTime(model.acquisitionDate);
                }
                if (!string.IsNullOrWhiteSpace(model.commissioningDate))
                {
                    equipment.dt_commissioning = Convert.ToDateTime(model.commissioningDate);
                }
                if (!string.IsNullOrWhiteSpace(model.deliveryDate))
                {
                    equipment.dt_site_delivery = Convert.ToDateTime(model.deliveryDate);
                }
                if (!string.IsNullOrWhiteSpace(model.installationDate))
                {
                    equipment.dt_installation = Convert.ToDateTime(model.installationDate);
                }
                if (!string.IsNullOrWhiteSpace(model.warrantyDate))
                {
                    equipment.dt_warranty_exp = Convert.ToDateTime(model.warrantyDate);
                }
                equipment.mfg_year = Convert.ToInt32(model.manufacture_year);
                equipment.equipment_model_id = model.model_id;
                equipment.sales_contact_name = model.sales_name;
                equipment.sales_contact_no = model.sales_no;
                equipment.serial_no = model.serial_no;
                equipment.equipment_status_id = model.status_id;
                equipment.support_contact_name = model.support_name;
                equipment.support_contact_no = model.support_no;
                equipment.id = model.id;

                int equipment_id = _equipmentService.EditEquipment(equipment);
                return equipment_id;
            }
            catch (Exception ex)
            {
                // return error message if there was an exception
                return 0;
            }
        }

        [HttpPost]
        [Route("/Equipment/GetEditEquipmentByEquipmentNo/{equipmentNo}")]
        public string GetEditEquipmentByEquipmentNo(string equipmentNo)
        {
            object part = _equipmentService.GetEditEquipmentByEquipmentNo(equipmentNo);
            return JsonConvert.SerializeObject(part);
        }

        [HttpPost]
        [Route("Equipment/GetHomeTotalEqLocationChart")]
        public string GetHomeTotalEquipmentLocationChart()
        {
            IEnumerable<object> homeTotalEqLocationChart = _equipmentService.GetHomeTotalEqLocationChart();
            return JsonConvert.SerializeObject(homeTotalEqLocationChart);
        }

        [HttpPost]
        [Route("Equipment/GetHomeTotalEqProcessChart")]
        public string GetHomeTotalEquipmentProcessChart()
        {
            IEnumerable<object> homeTotalEqProcessChart = _equipmentService.GetHomeTotalEqProcessChart();
            return JsonConvert.SerializeObject(homeTotalEqProcessChart);
        }

        [HttpGet]
        [Route("/Equipment/GetEquipmentSelection")]
        public string GetEquipmentSelection()
        {
            IEnumerable<object> equipmentSelectionList = _equipmentService.GetEquipmentSelection();
            return JsonConvert.SerializeObject(equipmentSelectionList);
        }
    }
}