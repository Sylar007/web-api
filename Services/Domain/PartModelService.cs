using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Entities;
using WebApi.Helpers;

namespace WebApi.Services
{
    public class PartModelService : IPartModelService
    {
        private DataContext _context;

        public PartModelService(DataContext context)
        {
            _context = context;
        }
        public IEnumerable<dynamic> GetPartModelList()
        {
            try
            {
                return (from part_model in _context.part_model
                        select new
                        {
                            id = part_model.id,
                            partName = part_model.name,
                            partCode = part_model.code
                        }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public dynamic GetPartModelById(int id)
        {
            try
            {
                return (from part_model in _context.part_model
                        join part_type in _context.part_type on part_model.part_type_id equals part_type.id into eqmJoin
                        from eqmj in eqmJoin.DefaultIfEmpty()
                        where part_model.id == id
                        select new
                        {
                            id = part_model.id,
                            partName = part_model.name,
                            partCode = part_model.code,
                            partTypeId = eqmj.id,
                            partType = eqmj.name,
                            modelName = part_model.model_name,
                            manufacture = part_model.mfg_name,
                            modelNo = part_model.model_no,
                            remarks = part_model.remark,
                            sales_contact_name = part_model.sales_contact_name,
                            sales_contact_no = part_model.sales_contact_no,
                            support_contact_name = part_model.support_contact_name,
                            support_contact_no = part_model.support_contact_no
                        }).First();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int EditPartModel(part_model data)
        {
            try
            {

                part_model part_model = _context.part_model.Where(x => x.id == data.id).First();
                part_model.id = data.id;
                part_model.name = data.name;
                part_model.code = data.code;
                part_model.part_type_id = data.part_type_id;
                part_model.model_no = data.model_name;
                part_model.model_name = data.model_no;
                part_model.mfg_name = data.mfg_name;
                part_model.remark = data.remark;
                part_model.sales_contact_name = data.sales_contact_name;
                part_model.sales_contact_no = data.sales_contact_no;
                part_model.support_contact_name = data.support_contact_name;
                part_model.support_contact_no = data.support_contact_no;
                part_model.dt_modified = data.dt_modified;
                part_model.modified_by = data.modified_by;
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
        public int AddPartModel(part_model data)
        {
            try
            {
                _context.part_model.Add(data);
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
        public IEnumerable<dynamic> GetPartTypeSelection()
        {
            try
            {
                return (from pt in _context.part_type
                        orderby pt.name
                        select new
                        {
                            id = pt.id,
                            part_type = pt.name
                        }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
