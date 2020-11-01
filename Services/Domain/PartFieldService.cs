using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Entities;
using WebApi.Helpers;

namespace WebApi.Services
{
    public class PartFieldService : IPartFieldService
    {
        private DataContext _context;

        public PartFieldService(DataContext context)
        {
            _context = context;
        }

        public IEnumerable<dynamic> GetPartFieldList(int partId, string fieldType)
        {
            try
            {
                if (partId == 0)
                {
                    return new List<part_field>();
                }
                return _context.part_field.Where((part_field ef) => ef.part_id == partId && ef.field_type == fieldType).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool CreatePartField(part_field part_fieldParam)
        {
            try
            {
                _context.part_field.Add(part_fieldParam);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool UpdatePartField(part_field part_fieldParam)
        {
            try
            {
                _context.part_field.Update(part_fieldParam);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool DeletePartField(int id)
        {
            try
            {
                part_field partToDelete = _context.part_field.Where((part_field ef) => ef.id == id).FirstOrDefault();
                _context.part_field.Remove(partToDelete);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
