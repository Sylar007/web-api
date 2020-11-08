using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Services
{
    public interface ITaskSubService
    {
        IEnumerable<dynamic> GetTaskSubTree(int equipment_id, int wo_type_id);
    }
}
