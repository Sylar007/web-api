using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Entities;
using WebApi.Helpers;

namespace WebApi.Services
{
    public class PartModelFieldService : IPartModelFieldService
    {
        private DataContext _context;
        public PartModelFieldService(DataContext context)
        {
            _context = context;
        }
        public IEnumerable<dynamic> GetPartModelFieldList(int partmodelId, string fieldType)
        {
            try
            {
                if (partmodelId == 0)
                {
                    return new List<part_model_field>();
                }
                return _context.part_model_field.Where((part_model_field ef) => ef.part_model_id == partmodelId && ef.field_type == fieldType).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool CreatePartModelField(part_model_field partmodel_fieldParam)
        {
            try
            {
                _context.part_model_field.Add(partmodel_fieldParam);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool UpdatePartModelField(part_model_field partmodel_fieldParam)
        {
            try
            {
                _context.part_model_field.Update(partmodel_fieldParam);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool DeletePartModelField(int id)
        {
            try
            {
                part_model_field partToDelete = _context.part_model_field.Where((part_model_field ef) => ef.id == id).FirstOrDefault();
                _context.part_model_field.Remove(partToDelete);
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
