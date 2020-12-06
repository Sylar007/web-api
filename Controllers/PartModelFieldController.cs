using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApi.Entities;
using WebApi.Services;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PartModelFieldController : ControllerBase
    {
        private IPartModelFieldService _partmodelfieldService;

        public PartModelFieldController(
            IPartModelFieldService partmodelfieldService)
        {
            _partmodelfieldService = partmodelfieldService;
        }
        [HttpGet]
        [Route("/PartModelField/GetPartModelFieldList/{partModelId}")]
        public string GetPartModelFieldList(int partModelId)
        {
            IEnumerable<object> partModelFieldList = _partmodelfieldService.GetPartModelFieldList(partModelId, "Spec");
            return JsonConvert.SerializeObject(partModelFieldList);
        }

        [HttpPost]
        [Route("/PartModelField/createPartModelField")]
        public bool createPartModelField([FromBody]part_model_field partmodelField)
        {
            partmodelField.field_type = "Spec";
            bool createOperation = _partmodelfieldService.CreatePartModelField(partmodelField);
            return createOperation;
        }

        [HttpPut]
        [Route("/PartModelField/updatePartModelField")]
        public bool UpdatePartModelField(part_model_field partmodelField)
        {
            partmodelField.field_type = "Spec";
            bool updateOperation = _partmodelfieldService.UpdatePartModelField(partmodelField);
            return updateOperation;
        }

        [HttpDelete]
        [Route("/PartModelField/deletePartModelField/{id}")]
        public bool DeletePartModelField(int id)
        {

            bool deleteOperation = _partmodelfieldService.DeletePartModelField(id);
            return deleteOperation;
        }
    }
}