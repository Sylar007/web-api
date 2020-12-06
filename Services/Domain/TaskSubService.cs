using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Entities;
using WebApi.Helpers;
using WebApi.Models.Tasks;
namespace WebApi.Services
{
    public class TaskSubService : ITaskSubService
    {
        private DataContext _context;

        public TaskSubService(DataContext context)
        {
            _context = context;
        }
        public IList<TaskSubs> GetTaskSubTree(int woid, int equipment_id, int wo_type_id)
        {
            try
            {
                var query = (from e in _context.equipments
                             join t in _context.task on e.equipment_model_id equals t.equipment_model_id into tJoin
                             from tj in tJoin.DefaultIfEmpty()
                             where e.id == equipment_id && tj.wo_type_id == wo_type_id
                             orderby tj.task_no
                             select new
                             {
                                 taskId = tj.id,
                                 taskName = string.Concat(((tj != null) ? tj.task_no : "") + " - ", (tj != null) ? tj.name : "")
                             }).ToList();

                IList<TaskSubs> tasksubs = new List<TaskSubs>();
                foreach (var parent in query)
                {
                    TaskSubs tasksub = new TaskSubs();
                    List<children> childrens = new List<children>();
                    var query2 = (from ts in _context.task_sub
                                  where ts.task_id == parent.taskId
                                  select new
                                  {
                                      id = ts.id,
                                      subtaskName = ts.name,
                                      task_sub_id = ts.id
                                  }).ToList();
                    foreach (var children in query2)
                    {
                        //mda: cross check if the Work Order already exist in other Work Order
                        var completedTaskSub = GetTaskSubCompleted(equipment_id, wo_type_id, children.task_sub_id).FirstOrDefault();

                        children subtasksingle = new children();
                        List<value> values = new List<value>();
                        if (completedTaskSub != null)
                        {
                            if (completedTaskSub.Item1 == woid)
                            {
                                subtasksingle.text = children.subtaskName;
                                subtasksingle.Checked = true;
                                subtasksingle.disabled = false;
                            }
                            else
                            {
                                subtasksingle.text = children.subtaskName + "  (Sub Task ini telah dilaksana dari " + completedTaskSub.Item2 + " pada " + completedTaskSub.Item3.Value.ToString("dd/MM/yyyy") + ")";
                                subtasksingle.Checked = false;
                                subtasksingle.disabled = true;
                            }
                            value subtasksingleValue = new value();
                            subtasksingleValue.id = children.id.ToString();
                            int getBeforeFiles = GetBeforeFiles(woid, children.task_sub_id).Count();
                            int getAfterFiles = GetAfterFiles(woid, children.task_sub_id).Count();
                            //onclick = "window.open('WOExUpload?type=WOExPre&tasksubid=158&woid=150', 'child', 'height=600,width=1506,directories=no,left=518,scrollbars'); return false"
                            //subtasksingleValue.url = "&emsp;<button type='button' data-toggle='modal' onclick='this.modalSubTaskFile.show()'>Before (0)</button>";
                            //subtasksingleValue.url = "&emsp;<a role='button' style='cursor: pointer; color: blue' data-toggle='modal' 'aria-hidden='true' onclick='window.open('/workorder/workordersubtaskfile;wo_id=6', 'child', 'height=600,width=1506,directories=no,left=518,scrollbars'); return false'>Before</a> (0) | <a href = 'http://www.yahoo.com' target = '_blank'>After</a> (0)";
                            subtasksingleValue.url = "&emsp;<a href = 'workorder/workordersubtaskfile;wo_id=" + woid + ";task_sub_id=" + children.task_sub_id + ";upload_type=1' style='cursor: pointer; color: blue' target = '_blank' >Before</a> ("+ getBeforeFiles +") | <a href = 'workorder/workordersubtaskfile;wo_id=" + woid + ";task_sub_id=" + children.task_sub_id + ";upload_type=2' style='cursor: pointer; color: blue' target = '_blank' >After</a> (" + getAfterFiles + ")";

                            //subtasksingleValue.url = "&emsp;<a style='cursor: pointer; color: blue' onclick='window.open('workorder/workordersubtaskfile;wo_id=6', 'child', 'height=600,width=1506,directories=no,left=518,scrollbars'); return false' target = '_blank' >Before</a> (0) | <a href = 'http://www.yahoo.com' target = '_blank'>After</a> (0)";
                            //subtasksingleValue.url= "&emsp;<a href = 'http://www.yahoo.com' target = '_blank'>Before</a> (0) | <a href = 'http://www.yahoo.com' target = '_blank'>After</a> (0)";
                            values.Add(subtasksingleValue);
                            subtasksingle.value = values;
                        }
                        else
                        {
                            //mda: cross check if the Work Order already exist in the Work Order
                            var existTaskSub = GetTaskSubExisted(woid, equipment_id, wo_type_id, children.task_sub_id).FirstOrDefault();

                            if (existTaskSub != null)
                            {
                                subtasksingle.text = children.subtaskName;
                                subtasksingle.Checked = true;
                                value subtasksingleValue = new value();
                                subtasksingleValue.id = children.id.ToString();
                                int getBeforeFiles = GetBeforeFiles(woid, children.task_sub_id).Count();
                                int getAfterFiles = GetAfterFiles(woid, children.task_sub_id).Count();

                                subtasksingleValue.url = "&emsp;<a href = 'workorder/workordersubtaskfile;wo_id=" + woid + ";task_sub_id=" + children.task_sub_id + ";upload_type=1' style='cursor: pointer; color: blue' target = '_blank' >Before</a> (" + getBeforeFiles + ") | <a href = 'workorder/workordersubtaskfile;wo_id=" + woid + ";task_sub_id=" + children.task_sub_id + ";upload_type=2' style='cursor: pointer; color: blue' target = '_blank' >After</a> (" + getAfterFiles + ")";

                                values.Add(subtasksingleValue);
                                subtasksingle.value = values;
                            }
                            else
                            {
                                subtasksingle.text = children.subtaskName;
                                subtasksingle.Checked = false;

                                value subtasksingleValue = new value();
                                subtasksingleValue.id = children.id.ToString();
                                int getBeforeFiles = GetBeforeFiles(woid, children.task_sub_id).Count();
                                int getAfterFiles = GetAfterFiles(woid, children.task_sub_id).Count();

                                subtasksingleValue.url = "&emsp;<a href = 'workorder/workordersubtaskfile;wo_id=" + woid + ";task_sub_id=" + children.task_sub_id + ";upload_type=1' style='cursor: pointer; color: blue' target = '_blank' >Before</a> (" + getBeforeFiles + ") | <a href = 'workorder/workordersubtaskfile;wo_id=" + woid + ";task_sub_id=" + children.task_sub_id + ";upload_type=2' style='cursor: pointer; color: blue' target = '_blank' >After</a> (" + getAfterFiles + ")";

                                values.Add(subtasksingleValue);
                                subtasksingle.value = values;
                            }                           
                        }
                        childrens.Add(subtasksingle);
                    }
                    tasksub.value = parent.taskId.ToString();
                    tasksub.text = parent.taskName;
                    tasksub.Checked = true;
                    tasksub.disabled = false;
                    tasksub.children = childrens;
                    tasksubs.Add(tasksub);
                }
                return tasksubs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IQueryable<Tuple<int, string, DateTime?>> GetTaskSubCompleted(int equipment_id, int wo_type_id,int task_sub_id)
        {
            var today = DateTime.Today;
            return (from task_sub in _context.task_sub
                    join wo_task_sub in _context.wo_task_sub on task_sub.id equals wo_task_sub.task_sub_id into tJoin
                    from tj in tJoin.DefaultIfEmpty()
                    join work_order in _context.work_order on tj.wo_id equals work_order.id into woJoin
                    from woj in woJoin.DefaultIfEmpty()
                    where woj.equipment_id == equipment_id && woj.wo_type_id == wo_type_id &&
                    woj.dt_created.Value.Date == today && tj.task_sub_id == task_sub_id
                    select new Tuple<int, string, DateTime?>(                    
                        woj.id,
                        woj.wo_no,
                        woj.dt_created
                    ));
        }
        public IQueryable<Tuple<int>> GetTaskSubExisted(int woid, int equipment_id, int wo_type_id, int task_sub_id)
        {
            return (from task_sub in _context.task_sub
                    join wo_task_sub in _context.wo_task_sub on task_sub.id equals wo_task_sub.task_sub_id into tJoin
                    from tj in tJoin.DefaultIfEmpty()
                    join work_order in _context.work_order on tj.wo_id equals work_order.id into woJoin
                    from woj in woJoin.DefaultIfEmpty()
                    where woj.equipment_id == equipment_id && woj.wo_type_id == wo_type_id && tj.task_sub_id == task_sub_id && woj.id == woid
                    select new Tuple<int>(
                        woj.id
                    ));
        }

        public IQueryable<Tuple<int>> GetBeforeFiles(int woid, int task_sub_id)
        {
            return (from wts in _context.wo_task_sub_file                   
                    where wts.wo_id == woid && wts.task_sub_id == task_sub_id && wts.upload_type == 1
                    select new Tuple<int>(
                        wts.id
                    ));
        }
        public IQueryable<Tuple<int>> GetAfterFiles(int woid, int task_sub_id)
        {
            return (from wts in _context.wo_task_sub_file
                    where wts.wo_id == woid && wts.task_sub_id == task_sub_id && wts.upload_type == 2
                    select new Tuple<int>(
                        wts.id
                    ));
        }
    }
}
