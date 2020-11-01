using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Entities;
using WebApi.Helpers;

namespace WebApi.Services
{
    public class EquipmentModelPartModelService : IEquipmentModelPartModelService
    {
        private DataContext _context;

        public EquipmentModelPartModelService(DataContext context)
        {
            _context = context;
        }
        public IEnumerable<dynamic> GetEquipmentModelPartModelList(int equipmentModelId)
        {
            try
            {
                if (equipmentModelId == 0)
                {
                    return new List<part_model>();
                }
                return (from p in _context.part_model
                        join emp in _context.equipment_model_part_model on p.id equals emp.part_model_id into pJoin
                        from pj in pJoin.DefaultIfEmpty()
                        where pj.equipment_model_id == equipmentModelId
                        select new
                        {
                            part_model_id = p.id,
                            model_no = p.model_no,
                            model_name = p.model_name,
                            name = p.name,
                            code = p.code
                        }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool EditEquipmentModelPartModel(int equipmentModelId, List<dynamic> dataList)
        {
            try
            {
                List<equipment_model_part_model> list = _context.equipment_model_part_model.Where((equipment_model_part_model emp) => emp.equipment_model_id == equipmentModelId).ToList();
                foreach (equipment_model_part_model item in list)
                {
                    _context.equipment_model_part_model.Remove(item);
                    _context.SaveChanges();
                }
                foreach (dynamic data in dataList)
                {
                    equipment_model_part_model equipment_model_part_model = new equipment_model_part_model();
                    equipment_model_part_model.equipment_model_id = equipmentModelId;
                    equipment_model_part_model.part_model_id = Convert.ToInt32(data);
                    equipment_model_part_model entity = equipment_model_part_model;
                    _context.equipment_model_part_model.Add(entity);
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
