using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Entities;
using WebApi.Helpers;

namespace WebApi.Services
{
    public class PartTypeService : IPartTypeService
    {
        private DataContext _context;
        public PartTypeService(DataContext context)
        {
            _context = context;
        }

        public IEnumerable<dynamic> GetPartTypeList()
        {
            try
            {
                return (from part_type in _context.part_type
                        select new
                        {
                            id = part_type.id,
                            name = part_type.name,
                            description = part_type.description
                        }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public dynamic GetPartTypeById(int id)
        {
            try
            {
                return (from part_type in _context.part_type
                        where part_type.id == id
                        select new
                        {
                            id = id,
                            name = part_type.name,
                            description = part_type.description
                        }).First();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int EditPartType(part_type data)
        {
            try
            {

                part_type part_type = _context.part_type.Where(x => x.id == data.id).First();
                part_type.name = data.name;
                part_type.description = data.description;
                part_type.dt_modified = data.dt_modified;
                part_type.modified_by = data.modified_by;
                int num = _context.SaveChanges();
                if (num > 0)
                {
                    return data.id;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return data.id;
        }
        public int AddPartType(part_type data)
        {
            try
            {
                _context.part_type.Add(data);
                int num = _context.SaveChanges();
                _context.SaveChanges();
                if (num > 0)
                {
                    return data.id;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return data.id;
        }
    }
}
