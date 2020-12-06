using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Entities;

namespace WebApi.Services
{
    public interface IPartTypeService
    {
        IEnumerable<dynamic> GetPartTypeList();
        dynamic GetPartTypeById(int id);
        int EditPartType(part_type data);
        int AddPartType(part_type data);
    }
}
