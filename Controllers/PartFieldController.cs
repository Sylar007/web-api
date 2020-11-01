using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WebApi.Services;
using Newtonsoft.Json;
using WebApi.Entities;
using System.Collections.Generic;

namespace WebApi.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class PartFieldController : ControllerBase
    {
        private IPartFieldService _partFieldService;

        public PartFieldController(
            IPartFieldService partFieldService)
        {
            _partFieldService = partFieldService;
        }

		[HttpGet]
		[Route("/PartField/GetPartFieldList/{partId}")]
		public string GetPartFieldList(int partId)
		{
			IEnumerable<object> partFieldList = _partFieldService.GetPartFieldList(partId, "Spec");
			return JsonConvert.SerializeObject(partFieldList);
		}

		[HttpPost]
		[Route("/PartField/CreatePartField")]
		public bool CreatePartField([FromBody]part_field partField)
		{
			bool createOperation = _partFieldService.CreatePartField(partField);
			return createOperation;
		}

		[HttpPut]
		[Route("/PartField/UpdatePartField")]
		public bool UpdatePartField(part_field partfield)
		{
			bool updateOperation = _partFieldService.UpdatePartField(partfield);
			return updateOperation;
		}

		[HttpDelete]
		[Route("/PartField/DeletePartField/{id}")]
		public bool DeletePartField(int id)
		{

			bool deleteOperation = _partFieldService.DeletePartField(id);
			return deleteOperation;
		}
	}
}