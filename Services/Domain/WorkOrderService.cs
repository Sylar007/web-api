using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Entities;
using WebApi.Helpers;

namespace WebApi.Services
{
    public class WorkOrderService : IWorkOrderService
    {
        private DataContext _context;

        public WorkOrderService(DataContext context)
        {
            _context = context;
        }

        public dynamic GetPartByWorkOrderNo(string workorderNo)
        {
            try
            {
                return (from workorder in _context.work_order
                        join equipment in _context.equipments on workorder.equipment_id equals equipment.id
                        join User in _context.Users on workorder.assignee_user_id equals User.Id
                        where workorder.wo_no == workorderNo
                        select new
                        {
                            id = workorder.id,
                            wo_name = workorder.wo_name,
                            wo_no = workorder.wo_no,
                            equipment_serialno = equipment.serial_no,
                            action_taken = workorder.action_taken,
                            assign_to = User.FirstName
                        }).First();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
