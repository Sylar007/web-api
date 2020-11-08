using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Services
{
    public interface IWorkOrderService
    {
        dynamic GetWorkOrderByWorkOrderNo(string workorderNo);
        IEnumerable<dynamic> GetPartByWorkOrderNo(string workorderNo);
        IEnumerable<dynamic> GetWorkOrderList();
        IEnumerable<dynamic> GetWorkOrderSelection();
    }
}
