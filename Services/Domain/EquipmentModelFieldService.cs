using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Entities;
using WebApi.Helpers;

namespace WebApi.Services
{
    public class EquipmentModelFieldService : IEquipmentModelFieldService
    {
        private DataContext _context;

        public EquipmentModelFieldService(DataContext context)
        {
            _context = context;
        }
        public IEnumerable<dynamic> GetEquipmentModelFieldList(int equipmentModelId, string fieldType)
        {
            try
            {
                if (equipmentModelId == 0)
                {
                    return new List<equipment_model_field>();
                }
                return _context.equipment_model_field.Where((equipment_model_field emf) => emf.equipment_model_id == equipmentModelId && emf.field_type == fieldType).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool EditEquipmentModelField(int equipmentModelId, List<dynamic> dataList, string fieldType)
        {
            try
            {
                List<equipment_model_field> list = _context.equipment_model_field.Where((equipment_model_field emf) => emf.equipment_model_id == equipmentModelId).ToList();
                foreach (equipment_model_field item in list)
                {
                    _context.equipment_model_field.Remove(item);
                    _context.SaveChanges();
                }
                foreach (dynamic data in dataList)
                {
                    equipment_model_field equipment_model_field = new equipment_model_field();
                    equipment_model_field.equipment_model_id = equipmentModelId;
                    equipment_model_field.name = data.name;
                    equipment_model_field.field_value = data.value;
                    equipment_model_field.field_type = fieldType;
                    equipment_model_field entity = equipment_model_field;
                    _context.equipment_model_field.Add(entity);
                    _context.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool CreateEquipmentModelField(equipment_model_field equipmentmodel_fieldParam)
        {
            try
            {
                _context.equipment_model_field.Add(equipmentmodel_fieldParam);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool UpdateEquipmentModelField(equipment_model_field equipmentmodel_fieldParam)
        {
            try
            {
                _context.equipment_model_field.Update(equipmentmodel_fieldParam);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool DeleteEquipmentModelField(int id)
        {
            try
            {
                equipment_model_field equipmentToDelete = _context.equipment_model_field.Where((equipment_model_field ef) => ef.id == id).FirstOrDefault();
                _context.equipment_model_field.Remove(equipmentToDelete);
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
