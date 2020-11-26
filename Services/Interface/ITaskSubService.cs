using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models.Tasks;

namespace WebApi.Services
{
    public interface ITaskSubService
    {
        IList<TaskSubs> GetTaskSubTree(int woid, int equipment_id, int wo_type_id);
    }
}
