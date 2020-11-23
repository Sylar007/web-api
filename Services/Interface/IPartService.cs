using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Services
{
    public interface IPartService
    {
        dynamic GetPartBySerialNo(string serialNo);
        IEnumerable<dynamic> GetPartList();
        dynamic GetPartById(int id);
    }
}
