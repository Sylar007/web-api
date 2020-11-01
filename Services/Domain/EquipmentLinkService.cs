using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Entities;
using WebApi.Helpers;

namespace WebApi.Services
{
    public class EquipmentLinkService : IEquipmentLinkService
    {
        private DataContext _context;

        public EquipmentLinkService(DataContext context)
        {
            _context = context;
        }
        public IEnumerable<dynamic> GetEquipmentLinkList(int equipmentId, string linkType)
        {
            try
            {
                try
                {
                    if (equipmentId == 0)
                    {
                        return new List<equipment_link>();
                    }
                    return _context.equipment_link.Where((equipment_link el) => el.equipment_id == equipmentId && el.link_type == linkType).ToList();
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

        public bool EditEquipmentLink(int equipmentId, List<dynamic> dataList, string linkType)
        {
            try
            {
                List<equipment_link> list = _context.equipment_link.Where((equipment_link el) => el.equipment_id == equipmentId).ToList();
                foreach (equipment_link item in list)
                {
                    _context.equipment_link.Remove(item);
                    _context.SaveChanges();
                }
                foreach (dynamic data in dataList)
                {
                    equipment_link equipment_link = new equipment_link();
                    equipment_link.equipment_id = equipmentId;
                    equipment_link.link = data.value;
                    equipment_link.link_type = linkType;
                    equipment_link entity = equipment_link;
                    _context.equipment_link.Add(entity);
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
