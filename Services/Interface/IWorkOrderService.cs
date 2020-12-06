using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Entities;
using WebApi.Models.WorkOrder;

namespace WebApi.Services
{
    public interface IWorkOrderService
    {
        dynamic GetWorkOrderByWorkOrderNo(string workorderNo);
        IEnumerable<dynamic> GetPartByWorkOrderNo(string workorderNo);
        dynamic GetEditWorkOrderByWorkOrderNo(string workorderNo);
        IEnumerable<dynamic> GetWorkOrderList();
        IEnumerable<dynamic> GetWorkOrderSelection();
        int AddWorkOrder(work_order work_order);
        int EditWorkOrder(work_order data);
        bool EditSubTaskTree(int woid, List<EventModel> model);
    }
}
