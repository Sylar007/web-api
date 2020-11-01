using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Entities;

namespace WebApi.Services
{
    public interface IPartFieldService
    {
        IEnumerable<dynamic> GetPartFieldList(int partId, string fieldType);
        bool CreatePartField(part_field part_fieldParam);
        bool UpdatePartField(part_field part_fieldParam);
        bool DeletePartField(int id);
    }
}
