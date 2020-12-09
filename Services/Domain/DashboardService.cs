using System;
using System.Collections.Generic;
using System.Linq;
using WebApi.Entities;
using WebApi.Helpers;

namespace WebApi.Services
{
    public class DashboardService : IDashboardService
    {
        private DataContext _context;

        public DashboardService(DataContext context)
        {
            _context = context;
        }
        public IEnumerable<dynamic> GetDashboardList(int id)
        {
            try
            {
                return (from workorder in _context.work_order
                        join equipment in _context.equipments on workorder.equipment_id equals equipment.id
                        join equipment_model in _context.equipment_model on equipment.equipment_model_id equals equipment_model.id
                        join wo_type in _context.wo_type on workorder.wo_type_id equals wo_type.id
                        where workorder.assignee_user_id == id
                        orderby workorder.dt_created
                        select new
                        {
                            id = workorder.id,
                            woNo = workorder.wo_no,
                            woType = wo_type.name,
                            woName = workorder.wo_name,
                            equipmentNo = equipment.equipment_no,
                            equipmentName = equipment_model.model_name,
                            equipmentModel = equipment_model.model_no,
                            serialNo = equipment.serial_no,
                            processName = equipment_model.process_name
                        }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
