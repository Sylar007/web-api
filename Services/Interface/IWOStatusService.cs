using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Entities;

namespace WebApi.Services
{
    public interface IWOStatusService
    {
        IEnumerable<wo_status> GetWOStatusList();
    }
}
