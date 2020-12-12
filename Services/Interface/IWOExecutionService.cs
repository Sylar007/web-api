using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Entities;

namespace WebApi.Services
{
    public interface IWOExecutionService
    {
        dynamic GetExecutionById(int id);
        int EditWorkExecution(work_order data);
    }
}
