using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Entities;

namespace WebApi.Services
{
    public interface IPartService
    {
        dynamic GetPartBySerialNo(string serialNo);
        IEnumerable<dynamic> GetPartList();
        dynamic GetPartById(int id);
        int EditPart(part data);
        int AddPart(part data);
        IEnumerable<dynamic> GetPartModelSelection();
    }
}
