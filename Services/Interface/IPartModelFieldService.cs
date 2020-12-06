using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Entities;

namespace WebApi.Services
{
    public interface IPartModelFieldService
    {
        IEnumerable<dynamic> GetPartModelFieldList(int partId, string fieldType);
        bool CreatePartModelField(part_model_field partmodel_fieldParam);
        bool UpdatePartModelField(part_model_field partmodel_fieldParam);
        bool DeletePartModelField(int id);
    }
}
