using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Entities;

namespace WebApi.Services
{
    public interface IWOPriorityService
    {
        IEnumerable<wo_priority> GetWOPriority();
    }
}
