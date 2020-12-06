using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Entities;

namespace WebApi.Services
{
    public interface IPartModelService
    {
        dynamic GetPartModelById(int id);
        IEnumerable<dynamic> GetPartModelList();
        int EditPartModel(part_model data);
        int AddPartModel(part_model data);
        IEnumerable<dynamic> GetPartTypeSelection();
    }
}
