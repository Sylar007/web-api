using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public IList<TaskSubs> GetTaskSubTree(int equipment_id, int wo_type_id)
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
                                      subtaskName = ts.name
                                  }).ToList();
                    foreach (var children in query2)
                    {
                        children subtasksingle = new children();
                        subtasksingle.text = children.subtaskName;
                        subtasksingle.value = children.id.ToString();
                        childrens.Add(subtasksingle);
                    }
                    tasksub.value = parent.taskId.ToString();
                    tasksub.text = parent.taskName;
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
