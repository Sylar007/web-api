using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Entities;
using WebApi.Helpers;

namespace WebApi.Services
{
    public class WOExecutionService : IWOExecutionService
    {
        private DataContext _context;

        public WOExecutionService(DataContext context)
        {
            _context = context;
        }
        public dynamic GetExecutionById(int id)
        {
            try
            {
                return (from wo in _context.work_order
                        join status in _context.wo_status on wo.wo_status_id equals status.id
                        join equipment in _context.equipments on wo.equipment_id equals equipment.id
                        join wo_type in _context.wo_type on wo.wo_type_id equals wo_type.id
                        where wo.id == id && ( status.id != 6 || status.id != 8 || status.id != 9)
                        select new
                        {
                            id = wo.id,
                            woNo = wo.wo_no,
                            name = wo.wo_status_id,
                            action = wo.action_taken,
                            status = status.name,
                            assignTo = wo.approve_user_id,
                            dateFinish = ((wo.dt_end_actual != null) ? wo.dt_end_actual : null),
                            equipment_id = equipment.id,
                            wo_type_id = wo_type.id
                        }).First();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int EditWorkExecution(work_order data)
        {
            try
            {
                work_order work_order = _context.work_order.Where((work_order w) => w.id == data.id).First();
                work_order.id = data.id;
                work_order.action_taken = data.action_taken;
                work_order.wo_status_id = data.wo_status_id;
                if (data.approve_user_id != 0)
                {
                    work_order.approve_user_id = data.approve_user_id;
                    work_order.dt_end_actual = data.dt_end_actual;
                }
                _context.SaveChanges();
                return data.id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
