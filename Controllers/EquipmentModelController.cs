using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using WebApi.Entities;
using WebApi.Helpers;
using WebApi.Models.EquipmentModel;
using WebApi.Services;

namespace WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
	[EnableCors()]
	public class EquipmentModelController : ControllerBase
    {
		private IEquipmentModelService _equipmentModelService;

		public EquipmentModelController(
			IEquipmentModelService equipmentModelService)
		{
			_equipmentModelService = equipmentModelService;
		}
		[HttpGet]
		[Route("/EquipmentModel/GetModelSelection")]
		public string GetModelSelection()
		{
			IEnumerable<object> equipmentModelList = _equipmentModelService.GetModelSelection();
			return JsonConvert.SerializeObject(equipmentModelList);
		}
		[HttpGet]
		[Route("/EquipmentModel/GetModelSelectionById/{id}")]
		public string GetModelSelectionById(int id)
		{
			object equipmentModel = _equipmentModelService.GetModelSelectionById(id);
			return JsonConvert.SerializeObject(equipmentModel);
		}
		[HttpGet]
		[Route("/EquipmentModel/GetEquipmentModelList")]
		public string GetEquipmentModelList()
		{
			IEnumerable<object> equipmentModelList = _equipmentModelService.GetEquipmentModelList();
			return JsonConvert.SerializeObject(equipmentModelList);
		}

		[Route("EquipmentModel/GetEquipmentModelProcessList")]
		public string GetEquipmentModelProcessList()
		{
			IEnumerable<object> equipmentModelProcessList = _equipmentModelService.GetEquipmentModelProcessList();
			return JsonConvert.SerializeObject(equipmentModelProcessList);
		}
		[HttpGet]
		[Route("/EquipmentModel/GetEquipmentModelById/{id}")]
		public string GetEquipmentModelById(int id)
		{
			dynamic equipmentModelById = _equipmentModelService.GetEquipmentModelById(id);
			dynamic val = JsonConvert.SerializeObject(equipmentModelById);
			return val;
		}

		[HttpPost]
		[Route("EquipmentModel/GetTotalEquipmentModel")]
		public string GetTotalEquipmentModel()
		{
			IEnumerable<object> totalEquipmentModel = _equipmentModelService.GetTotalEquipmentModel();
			return JsonConvert.SerializeObject(totalEquipmentModel);
		}

		[HttpPost]
		[Route("/EquipmentModel/UpdateEquipmentModel")]
		public int UpdateEquipmentModel([FromBody] Equipment_Model model)
		{
			equipment_model equipment_model = new equipment_model();

			equipment_model.id = model.id;
			equipment_model.name = model.equipmentName;
			equipment_model.equipment_type_id = model.equipmentTypeId;
			equipment_model.process_name = model.processName;
			//equipment_model.model_name = model.modelName;
			equipment_model.model_no = model.modelNo;
			equipment_model.mfg_name = model.manufacturer;
			equipment_model.sales_contact_name = model.sales_contact_name;
			equipment_model.sales_contact_no = model.sales_contact_no;
			equipment_model.support_contact_name = model.support_contact_name;
			equipment_model.support_contact_no = model.support_contact_no;
			equipment_model.remark = model.remarks;
			equipment_model.dt_modified = DateTime.Now;
			int idClaim = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type.Equals("assigned_User_Id", StringComparison.InvariantCultureIgnoreCase)).Value);
			equipment_model.modified_by = idClaim;
			return _equipmentModelService.EditEquipmentModel(equipment_model);
		}

		[HttpPost]
		[Route("/EquipmentModel/AddEquipmentModel")]
		public int AddEquipmentModel([FromBody] Equipment_Model model)
		{
			equipment_model equipment_model = new equipment_model();

			equipment_model.name = model.equipmentName;
			equipment_model.equipment_type_id = model.equipmentTypeId;
			equipment_model.process_name = model.processName;
			equipment_model.model_name = model.modelName;
			equipment_model.model_no = model.modelNo;
			equipment_model.mfg_name = model.manufacturer;
			equipment_model.sales_contact_name = model.sales_contact_name;
			equipment_model.sales_contact_no = model.sales_contact_no;
			equipment_model.support_contact_name = model.support_contact_name;
			equipment_model.support_contact_no = model.support_contact_no;
			equipment_model.remark = model.remarks;
			equipment_model.dt_created = DateTime.Now;
			int idClaim = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type.Equals("assigned_User_Id", StringComparison.InvariantCultureIgnoreCase)).Value);
			equipment_model.created_by = idClaim;
			return _equipmentModelService.AddEquipmentModel(equipment_model);
		}
	}
}