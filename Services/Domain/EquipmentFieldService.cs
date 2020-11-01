using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Entities;
using WebApi.Helpers;

namespace WebApi.Services
{
    public class EquipmentFieldService : IEquipmentFieldService
    {
        private DataContext _context;

        public EquipmentFieldService(DataContext context)
        {
            _context = context;
        }
        public IEnumerable<dynamic> GetEquipmentFieldList(int equipmentId, string fieldType)
        {
            try
            {
                if (equipmentId == 0)
                {
                    return new List<equipment_field>();
                }
                return _context.equipment_field.Where((equipment_field ef) => ef.equipment_id == equipmentId && ef.field_type == fieldType).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool EditEquipmentField(int equipmentId, List<dynamic> dataList, string fieldType)
        {
            try
            {
                List<equipment_field> list = _context.equipment_field.Where((equipment_field ef) => ef.equipment_id == equipmentId).ToList();
                foreach (equipment_field item in list)
                {
                    _context.equipment_field.Remove(item);
                    _context.SaveChanges();
                }
                foreach (dynamic data in dataList)
                {
                    equipment_field equipment_field = new equipment_field();
                    equipment_field.equipment_id = equipmentId;
                    equipment_field.name = data.name;
                    equipment_field.field_value = data.value;
                    equipment_field.field_type = fieldType;
                    equipment_field entity = equipment_field;
                    _context.equipment_field.Add(entity);
                    _context.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool CreateEquipmentField(equipment_field equipment_fieldParam)
        {
            try
            {
                _context.equipment_field.Add(equipment_fieldParam);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool UpdateEquipmentField(equipment_field equipment_fieldParam)
        {           
            try
            {
                _context.equipment_field.Update(equipment_fieldParam);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool DeleteEquipmentField(int id)
        {
            try
            {
                equipment_field equipmentToDelete = _context.equipment_field.Where((equipment_field ef) => ef.id == id).FirstOrDefault();
                _context.equipment_field.Remove(equipmentToDelete);
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
