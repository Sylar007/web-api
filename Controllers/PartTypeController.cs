using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApi.Models.Part;
using WebApi.Services;
using WebApi.Entities;

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class PartTypeController : ControllerBase
    {
        private IPartTypeService _parttypeService;

        public PartTypeController(
            IPartTypeService parttypeService)
        {
            _parttypeService = parttypeService;
        }
        [HttpGet]
        [Route("/PartType/GetPartTypeList")]
        public string GetPartTypeList()
        {
            IEnumerable<object> part = _parttypeService.GetPartTypeList();
            return JsonConvert.SerializeObject(part);
        }
        [HttpGet]
        [Route("/PartType/GetPartTypeById/{id}")]
        public string GetPartTypeById(int id)
        {
            object partType = _parttypeService.GetPartTypeById(id);
            return JsonConvert.SerializeObject(partType);
        }
        [HttpPost]
        [Route("/PartType/UpdatePartType")]
        public int UpdatePartType([FromBody] PartType_Model model)
        {
            part_type part_type = new part_type();

            part_type.id = model.id;
            part_type.name = model.name;
            part_type.description = model.description;
            part_type.dt_modified = DateTime.Now;
            int idClaim = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type.Equals("assigned_User_Id", StringComparison.InvariantCultureIgnoreCase)).Value);
            part_type.modified_by = idClaim;
            return _parttypeService.EditPartType(part_type);
        }
        [HttpPost]
        [Route("/PartType/AddPartType")]
        public int AddPartType([FromBody] PartType_Model model)
        {
            part_type part_type = new part_type();
            part_type.name = model.name;
            part_type.description = model.description;
            part_type.dt_created = DateTime.Now;
            part_type.dt_modified = DateTime.Now;
            int idClaim = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type.Equals("assigned_User_Id", StringComparison.InvariantCultureIgnoreCase)).Value);
            part_type.created_by = idClaim;
            part_type.modified_by = idClaim;
            return _parttypeService.AddPartType(part_type);
        }
    }
}