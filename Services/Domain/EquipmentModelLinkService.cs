using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Entities;
using WebApi.Helpers;

namespace WebApi.Services
{
    public class EquipmentModelLinkService : IEquipmentModelLinkService
    {
        private DataContext _context;

        public EquipmentModelLinkService(DataContext context)
        {
            _context = context;
        }
        public IEnumerable<dynamic> GetEquipmentModelLinkList(int equipmentModelId, string linkType)
        {
            try
            {
                try
                {
                    if (equipmentModelId == 0)
                    {
                        return new List<equipment_model_link>();
                    }
                    return _context.equipment_model_link.Where((equipment_model_link eml) => eml.equipment_model_id == equipmentModelId && eml.link_type == linkType).ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            catch (Exception ex2)
            {
                throw ex2;
            }
        }

        public bool EditEquipmentModelLink(int equipmentModelId, List<dynamic> dataList, string linkType)
        {
            try
            {
                List<equipment_model_link> list = _context.equipment_model_link.Where((equipment_model_link eml) => eml.equipment_model_id == equipmentModelId).ToList();
                foreach (equipment_model_link item in list)
                {
                    _context.equipment_model_link.Remove(item);
                    _context.SaveChanges();
                }
                foreach (dynamic data in dataList)
                {
                    equipment_model_link equipment_model_link = new equipment_model_link();
                    equipment_model_link.equipment_model_id = equipmentModelId;
                    equipment_model_link.link = data.value;
                    equipment_model_link.link_type = linkType;
                    equipment_model_link entity = equipment_model_link;
                    _context.equipment_model_link.Add(entity);
                    _context.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
