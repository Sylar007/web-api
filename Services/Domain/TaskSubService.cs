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
                        var today = DateTime.Today;
                        var query3 = (from task_sub in _context.task_sub
                                     join wo_task_sub in _context.wo_task_sub on task_sub.id equals wo_task_sub.task_sub_id into tJoin
                                     from tj in tJoin.DefaultIfEmpty()
                                     join work_order in _context.work_order on tj.wo_id equals work_order.id into woJoin
                                     from woj in woJoin.DefaultIfEmpty()
                                     where woj.equipment_id == equipment_id && woj.wo_type_id == wo_type_id && 
                                     woj.dt_created.Value.Date == today && tj.task_sub_id == children.task_sub_id
                                      select new
                                     {
                                          wo_id = woj.id,
                                          wo_no = woj.wo_no,
                                         wo_date_created = woj.dt_created
                                     }).FirstOrDefault();

                        children subtasksingle = new children();
                        if (query3 != null)
                        {
                            if (query3.wo_id == woid)
                            {
                                subtasksingle.text = children.subtaskName;
                                subtasksingle.disabled = false;
                            }
                            else
                            {
                                subtasksingle.text = children.subtaskName + "  (Sub Task ini telah dilaksana dari " + query3.wo_no + " pada " + query3.wo_date_created.Value.ToString("dd/MM/yyyy") + ")";
                                subtasksingle.disabled = true;
                            }                           
                        }
                        else
                        {
                            subtasksingle.text = children.subtaskName;
                            subtasksingle.disabled = false;
                        }
                        
                        subtasksingle.value = children.id.ToString();
                        subtasksingle.@checked = true;
                        childrens.Add(subtasksingle);
                    }
                    tasksub.value = parent.taskId.ToString();
                    tasksub.text = parent.taskName;
                    tasksub.@checked = true;
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
    }
}
