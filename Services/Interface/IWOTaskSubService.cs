using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Services
{
    public interface IWOTaskSubService
    {
        IEnumerable<dynamic> GetWOTaskSubList(int woId, string wo_task_type);
    }
}
