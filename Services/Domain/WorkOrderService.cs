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

        public dynamic GetWorkOrderByWorkOrderNo(string workorderNo)
        {
            try
            {
                return (from workorder in _context.work_order
                        join equipment in _context.equipments on workorder.equipment_id equals equipment.id
                        join wo_type in _context.wo_type on workorder.wo_type_id equals wo_type.id
                        join equipment_model in _context.equipment_model on equipment.equipment_model_id equals equipment_model.id
                        join wo_status in _context.wo_status on workorder.wo_status_id equals wo_status.id
                        join User in _context.Users on workorder.assignee_user_id equals User.Id
                        where workorder.wo_no == workorderNo
                        select new
                        {
                            id = workorder.id,
                            equipment_no = equipment.equipment_no,
                            wo_name = workorder.wo_name,
                            wo_no = workorder.wo_no,
                            wo_ordertype = wo_type.type_desc,
                            equipment_serialno = equipment.serial_no,
                            equipment_model = equipment_model.model_name,
                            action_taken = workorder.action_taken,
                            assign_to = User.FirstName,
                            status = wo_status.name,
                            equipment_name = equipment_model.name
                        }).First();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IEnumerable<dynamic> GetPartByWorkOrderNo(string workorderNo)
        {
            try
            {
                return (from workorder in _context.work_order
                        join equipment in _context.equipments on workorder.equipment_id equals equipment.id
                        join workorder_part in _context.wo_part on workorder.id equals workorder_part.wo_id
                        join part in _context.parts on workorder_part.part_id equals part.id
                        join part_models in _context.part_model on part.part_model_id equals part_models.id
                        join User in _context.Users on workorder.assignee_user_id equals User.Id
                        where workorder.wo_no == workorderNo
                        select new
                        {
                            id = workorder.id,
                            equipment_id = equipment.id,
                            part_id = workorder_part.part_id,
                            model_no = part_models.model_no,
                            model_name = part_models.model_name,
                            part_name = part_models.name,
                            part_code = part_models.code,
                            serial_no = part.serial_no
                        }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<dynamic> GetWorkOrderList()
        {
            try
            {
                return (from w in _context.work_order
                        join ws in _context.wo_status on w.wo_status_id equals ws.id
                        join wt in _context.wo_type on w.wo_type_id equals wt.id
                        join e in _context.equipments on w.equipment_id equals e.id
                        join eqm in _context.equipment_model on e.equipment_model_id equals eqm.id
                        where w.is_deleted == 0
                        orderby w.wo_no
                        select new
                        {
                            id = w.id,
                            workorderNo = w.wo_no,
                            workorderType = ((wt != null) ? wt.name : ""),
                            equipmentName = string.Concat(string.Concat(string.Concat(string.Concat(string.Concat(((e != null) ? e.equipment_no : "") + " : ", (eqm != null) ? eqm.name : ""), " / "), (eqm != null) ? eqm.model_name : ""), " / "), (e != null) ? e.serial_no : ""),
                            plannedStartDate = ((w.dt_start_planned != null) ? w.dt_start_planned : DateTime.MinValue),
                            plannedEndDate = ((w.dt_end_planned != null) ? w.dt_end_planned : DateTime.MinValue),
                            startDate = ((w.dt_start_actual != null) ? w.dt_start_actual : DateTime.MinValue),
                            completedDate = ((w.dt_end_actual != null) ? w.dt_end_actual : DateTime.MinValue),
                            status = ((ws != null) ? ws.name : ""),
                            location = ((e != null) ? e.location : "")
                        }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IEnumerable<dynamic> GetWorkOrderSelection()
        {
            try
            {
                return (from wt in _context.wo_type                        
                        orderby wt.name
                        select new
                        {
                            id = wt.id,
                            description = wt.type_desc
                        }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
