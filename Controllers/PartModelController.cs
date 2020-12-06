using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WebApi.Services;
using WebApi.Entities;
using Newtonsoft.Json;
using System.Linq;
using WebApi.Models.Part;

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class PartModelController : ControllerBase
    {
        private IPartModelService _partmodelService;

        public PartModelController(
            IPartModelService partmodelService)
        {
            _partmodelService = partmodelService;
        }

        [HttpGet]
        [Route("/partmodel/GetPartModelList")]
        public string GetPartModelList()
        {
            IEnumerable<object> part = _partmodelService.GetPartModelList();
            return JsonConvert.SerializeObject(part);
        }
        [HttpGet]
        [Route("/partmodel/GetPartModelById/{id}")]
        public string GetPartModelById(int id)
        {
            object part = _partmodelService.GetPartModelById(id);
            return JsonConvert.SerializeObject(part);
        }
		[HttpPost]
		[Route("/partmodel/UpdatePartModel")]
		public int UpdatePartModel([FromBody] PartModelModel model)
		{
			part_model part_model = new part_model();

            part_model.id = model.id;
            part_model.name = model.partName;
            part_model.code = model.partCode;
            part_model.part_type_id = model.partTypeId;
            part_model.model_name = model.modelName;
            part_model.model_no = model.modelNo;
            part_model.mfg_name = model.manufacture;
            part_model.remark = model.remarks;
            part_model.sales_contact_name = model.sales_contact_name;
            part_model.sales_contact_no = model.sales_contact_no;
            part_model.support_contact_name = model.support_contact_name;
            part_model.support_contact_no = model.support_contact_no;
            part_model.dt_modified = DateTime.Now;
			int idClaim = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type.Equals("assigned_User_Id", StringComparison.InvariantCultureIgnoreCase)).Value);
            part_model.modified_by = idClaim;
			return _partmodelService.EditPartModel(part_model);
		}
		[HttpPost]
		[Route("/partmodel/AddPartModel")]
		public int AddPartModel([FromBody] PartModelModel model)
		{
            part_model part_model = new part_model();
            part_model.name = model.partName;
            part_model.code = model.partCode;
            part_model.part_type_id = model.partTypeId;
            part_model.model_name = model.modelName;
            part_model.model_no = model.modelNo;
            part_model.mfg_name = model.manufacture;
            part_model.remark = model.remarks;
            part_model.sales_contact_name = model.sales_contact_name;
            part_model.sales_contact_no = model.sales_contact_no;
            part_model.support_contact_name = model.support_contact_name;
            part_model.support_contact_no = model.support_contact_no;
            part_model.dt_created = DateTime.Now;
            part_model.dt_modified = DateTime.Now;
            int idClaim = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type.Equals("assigned_User_Id", StringComparison.InvariantCultureIgnoreCase)).Value);
            part_model.created_by = idClaim;
            part_model.modified_by = idClaim;
            return _partmodelService.AddPartModel(part_model);
		}
        [HttpGet]
        [Route("/partmodel/GetPartTypeSelection")]
        public string GetPartTypeSelection()
        {
            IEnumerable<object> partTypeSelectionList = _partmodelService.GetPartTypeSelection();
            return JsonConvert.SerializeObject(partTypeSelectionList);
        }

    }
}