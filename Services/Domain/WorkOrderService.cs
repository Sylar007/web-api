using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Entities;
using WebApi.Helpers;
using WebApi.Models.WorkOrder;

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
                        join period in _context.period on workorder.freq_period_id equals period.id into periods
                        from freqPeriod in periods.DefaultIfEmpty()
                        join periodRemind in _context.period on workorder.reminder_period_id equals periodRemind.id into rPeriods
                        from reminderPeriod in rPeriods.DefaultIfEmpty()
                        where workorder.wo_no == workorderNo
                        select new
                        {
                            id = workorder.id,
                            equipment_no = equipment.equipment_no,
                            wo_name = workorder.wo_name,
                            wo_no = workorder.wo_no,
                            wo_ordertype = wo_type.type_desc,
                            wo_type_id = wo_type.id,
                            equipment_id = equipment.id,
                            equipment_serialno = equipment.serial_no,
                            equipment_model = equipment_model.model_name,
                            action_taken = workorder.action_taken,
                            assign_to = User.FirstName,
                            status = wo_status.name,
                            equipment_name = equipment_model.name,
                            remarks = workorder.remarks,
                            frequency = freqPeriod.name,
                            frequency_total = workorder.freq_total,
                            remindBefore = reminderPeriod.name,
                            remindBefore_total = workorder.reminder_total,
                            plannedExecutionDateFrom = workorder.dt_start_planned,
                            plannedExecutionDateTo = workorder.dt_end_planned,
                            actualExecutionDateFrom = workorder.dt_start_actual,
                            actualExecutionDateTo = workorder.dt_end_actual
                        }).First();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic GetEditWorkOrderByWorkOrderNo(string workorderNo)
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
                            wo_name = workorder.wo_name,
                            wo_type_id = wo_type.id,
                            equipment_id = equipment.id,
                            asignee_user_id = User.Id,
                            wo_priority_id = workorder.wo_priority_id,
                            remarks = workorder.remarks,
                            dt_start_planned = workorder.dt_start_planned.ToString("yyyy-MM-dd"),
                            dt_end_planned = workorder.dt_end_planned.ToString("yyyy-MM-dd"),
                            timeFrom = workorder.dt_start_planned.ToString("HH:mm"),
                            timeTo = workorder.dt_end_planned.ToString("HH:mm")
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
                            startDate = ((w.dt_start_actual != null) ? w.dt_start_actual : null),
                            completedDate = ((w.dt_end_actual != null) ? w.dt_end_actual : null),
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

        public int AddWorkOrder(work_order data)
        {
            try
            {
                data.is_deleted = 0;
                data.dt_created = DateTime.Now;
                data.wo_action_id = 1;
                data.wo_status_id = 7;

                int? num = (from w in _context.work_order
                            where w.is_deleted == 0 && w.dt_start_planned.Year == data.dt_start_planned.Year && w.dt_start_planned.Month == data.dt_start_planned.Month && w.dt_start_planned.Day == data.dt_start_planned.Day
                            select w into p
                            select p.daily_no).Max();
                data.daily_no = (num ?? new int?(0)) + 1;
                if (data.wo_type_id == 3)
                {
                    int? num2 = (from w in _context.work_order
                                 where w.is_deleted == 0
                                 select w into p
                                 select p.series_no).DefaultIfEmpty(0).Max();
                    data.series_no = (num2 ?? new int?(0)) + 1;
                    string[] value = new string[4]
                    {
                    "WO",
                    data.series_no.ToString().PadLeft(4, '0'),
                    data.dt_start_planned.Date.ToString("yyMMdd"),
                    data.daily_no.ToString().PadLeft(2, '0')
                    };
                    data.wo_no = string.Join("-", value);
                }
                else
                {
                    string[] value = new string[3]
                    {
                    "WO",
                    data.dt_start_planned.Date.ToString("yyMMdd"),
                    data.daily_no.ToString().PadLeft(2, '0')
                    };
                    data.wo_no = string.Join("-", value);
                }
                _context.work_order.Add(data);
                int num3 = _context.SaveChanges();
                work_order work_order = _context.work_order.Where((work_order w) => w.id == data.id).First();
                if (work_order.wo_type_id == 3 && work_order.wo_status_id != 1)
                {
                    equipment equipment = _context.equipments.Where((equipment eq) => eq.id == data.equipment_id).First();
                    DateTime dateTime = work_order.dt_start_planned;
                    DateTime dateTime2 = Convert.ToDateTime(equipment.dt_warranty_exp);
                    int days = (dateTime2 - dateTime).Days;
                    while (dateTime < dateTime2)
                    {
                        if (data.freq_period_id == 1)
                        {
                            dateTime = dateTime.AddDays(work_order.freq_total);
                        }
                        else if (data.freq_period_id == 2)
                        {
                            dateTime = dateTime.AddDays(work_order.freq_total * 7);
                        }
                        else if (data.freq_period_id == 3)
                        {
                            dateTime = dateTime.AddMonths(work_order.freq_total);
                        }
                        else if (data.freq_period_id == 4)
                        {
                            dateTime = dateTime.AddYears(work_order.freq_total);
                        }
                        DateTime dt_end_planned = dateTime.AddDays(days);
                        data.dt_start_planned = dateTime;
                        data.dt_end_planned = dt_end_planned;
                        data.dt_start_actual = null;
                        data.dt_end_actual = null;
                        num = (from w in _context.work_order
                               where w.is_deleted == 0 && w.dt_start_planned.Year == data.dt_start_planned.Year && w.dt_start_planned.Month == data.dt_start_planned.Month && w.dt_start_planned.Day == data.dt_start_planned.Day
                               select w into p
                               select p.daily_no).DefaultIfEmpty(0).Max();
                        string[] value = new string[4]
                        {
                        "WO",
                        data.series_no.ToString().PadLeft(4, '0'),
                        data.dt_start_planned.ToString("yyMMdd"),
                        data.daily_no.ToString().PadLeft(2, '0')
                        };
                        data.wo_no = string.Join("-", value);
                        _context.work_order.Add(data);
                        _context.SaveChanges();
                    }
                }
                if (num3 > 0)
                {
                    return data.id;
                }
            }
            catch (Exception ex)
            {                
                throw;
            }
            return -1;
        }

        public int EditWorkOrder(work_order data)
        {
            try
            {
                work_order work_order = _context.work_order.Where((work_order w) => w.id == data.id).First();
                work_order.wo_name = data.wo_name;
                work_order.wo_type_id = data.wo_type_id;
                work_order.assignee_user_id = data.assignee_user_id;
                work_order.equipment_id = data.equipment_id;
                work_order.freq_total = data.freq_total;
                work_order.freq_period_id = data.freq_period_id;
                work_order.reminder_total = data.reminder_total;
                work_order.reminder_period_id = data.reminder_period_id;
                work_order.dt_start_planned = data.dt_start_planned;
                work_order.dt_end_planned = data.dt_end_planned;
                work_order.dt_start_actual = data.dt_start_actual;
                work_order.dt_end_actual = data.dt_end_actual;
                work_order.wo_priority_id = data.wo_priority_id;
                work_order.dt_modified = data.dt_modified;
                //work_order.modified_by = data.modified_by;
                if (data.wo_status_id <= 2 && work_order.wo_status_id < data.wo_status_id && work_order.wo_status_id < 5)
                {
                    work_order.wo_status_id = data.wo_status_id;
                }
                _context.SaveChanges();
                return data.id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool EditSubTaskTree(int woid, List<EventModel> model)
        {
            try
            {
                List<wo_task_sub> list = _context.wo_task_sub.Where((wo_task_sub wtc) => wtc.wo_id == woid).ToList();
                foreach (wo_task_sub item in list)
                {
                    _context.wo_task_sub.Remove(item);
                    _context.SaveChanges();
                }

                foreach (var data in model)
                {
                    wo_task_sub wo_task_sub = new wo_task_sub();
                    wo_task_sub.task_sub_id = Convert.ToInt32(data.id);
                    wo_task_sub.wo_id = woid;
                    wo_task_sub entity = wo_task_sub;
                    _context.wo_task_sub.Add(entity);
                    _context.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
