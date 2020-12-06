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
    public class PartController : ControllerBase
    {
		private IPartService _partService;

		public PartController(
			IPartService partService)
		{
			_partService = partService;
		}		

		[HttpPost]
		[Route("/Part/GetPartBySerialNo/{serialNo}")]
		public string GetPartBySerialNo(string serialNo)
		{
			object part = _partService.GetPartBySerialNo(serialNo);
			return JsonConvert.SerializeObject(part);
		}

		[HttpGet]
		[Route("/part/GetPartList")]
		public string GetPartList()
		{
			IEnumerable<object> part = _partService.GetPartList();
			return JsonConvert.SerializeObject(part);
		}

		[HttpGet]
		[Route("/Part/GetPartById/{id}")]
		public string GetPartById(int id)
		{
			object part = _partService.GetPartById(id);
			return JsonConvert.SerializeObject(part);
		}

		[HttpPost]
		[Route("/Part/UpdatePart")]
		public int UpdatePart([FromBody] PartModel model)
		{
			part part = new part();
			if (!string.IsNullOrWhiteSpace(model.acquisition_date))
			{
				part.dt_acquisition = Convert.ToDateTime(model.acquisition_date);
			}
			if (!string.IsNullOrWhiteSpace(model.warranty_date))
			{
				part.dt_warranty_exp = Convert.ToDateTime(model.warranty_date);
			}
			if (!string.IsNullOrWhiteSpace(model.installation_date))
			{
				part.dt_installation = Convert.ToDateTime(model.installation_date);
			}
			if (!string.IsNullOrWhiteSpace(model.certificate_date))
			{
				part.dt_cert = Convert.ToDateTime(model.certificate_date);
			}
     		part.mfg_year = model.mfg_year;			
			part.id = model.id;
			part.cert_no = model.certificate_no;
			part.serial_no = model.serial_no;
			part.remark = model.remarks;
			part.sales_contact_name = model.sales_contact_name;
			part.sales_contact_no = model.sales_contact_no;
			part.support_contact_name = model.support_contact_name;
			part.support_contact_no = model.support_contact_no;
			part.dt_modified = DateTime.Now;
			int idClaim = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type.Equals("assigned_User_Id", StringComparison.InvariantCultureIgnoreCase)).Value);
			part.modified_by = idClaim;
			return _partService.EditPart(part);
		}

		[HttpPost]
		[Route("/Part/AddPart")]
		public int AddPart([FromBody] PartModel model)
		{
			part part = new part();
			if (!string.IsNullOrWhiteSpace(model.acquisition_date))
			{
				part.dt_acquisition = Convert.ToDateTime(model.acquisition_date);
			}
			if (!string.IsNullOrWhiteSpace(model.warranty_date))
			{
				part.dt_warranty_exp = Convert.ToDateTime(model.warranty_date);
			}
			if (!string.IsNullOrWhiteSpace(model.installation_date))
			{
				part.dt_installation = Convert.ToDateTime(model.installation_date);
			}
			if (!string.IsNullOrWhiteSpace(model.certificate_date))
			{
				part.dt_cert = Convert.ToDateTime(model.certificate_date);
			}
			part.mfg_year = model.mfg_year;
			part.part_model_id = model.model_id;
			part.cert_no = model.certificate_no;
			part.serial_no = model.serial_no;
			part.remark = model.remarks;
			part.sales_contact_name = model.sales_contact_name;
			part.sales_contact_no = model.sales_contact_no;
			part.support_contact_name = model.support_contact_name;
			part.support_contact_no = model.support_contact_no;
			part.dt_created = DateTime.Now;
			int idClaim = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type.Equals("assigned_User_Id", StringComparison.InvariantCultureIgnoreCase)).Value);
			part.created_by = idClaim;
			return _partService.AddPart(part);
		}

		[HttpGet]
		[Route("/Part/GetPartModelSelection")]
		public string GetPartModelSelection()
		{
			IEnumerable<object> partModelSelectionList = _partService.GetPartModelSelection();
			return JsonConvert.SerializeObject(partModelSelectionList);
		}
	}
}