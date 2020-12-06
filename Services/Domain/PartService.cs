using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Entities;
using WebApi.Helpers;

namespace WebApi.Services
{
    public class PartService : IPartService
    {
        private DataContext _context;

        public PartService(DataContext context)
        {
            _context = context;
        }

        public dynamic GetPartBySerialNo(string serialNo)
        {
            try
            {
                return (from part in _context.parts
                        join part_model in _context.part_model on part.part_model_id equals part_model.id
                        where part.serial_no == serialNo
                        select new
                        {
                            id = part.id,
                            part_no = part.part_no,
                            serial_no = part.serial_no,
                            name = part_model.model_name,
                            mfg_year = part.mfg_year,
                            sales_contact_name = part.support_contact_name
                        }).First();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public IEnumerable<dynamic> GetPartList()
        {
            try
            {
                return (from part in _context.parts
                        join part_model in _context.part_model on part.part_model_id equals part_model.id
                        select new
                        {
                            id = part.id,
                            partModelNo = part_model.model_no,
                            partModelName = part_model.model_name,
                            serialNo = part.serial_no
                        }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic GetPartById(int id)
        {
            try
            {
                return (from part in _context.parts
                        join part_model in _context.part_model on part.part_model_id equals part_model.id
                        where part.id == id
                        select new
                        {
                            id = part.id,
                            model_id = part.part_model_id,
                            part_model = string.Concat(string.Concat(string.Concat(part_model.name + " / ", part_model.model_name), " / "), part_model.code),
                            serial_no = part.serial_no,
                            acquisition_date = part.dt_acquisition,
                            mfg_year = part.mfg_year,
                            warranty_date = part.dt_warranty_exp,
                            installation_date = part.dt_installation,
                            certificate_no = part.cert_no,
                            remarks = part.remark,
                            certificate_date = part.dt_cert,
                            sales_contact_name = part.sales_contact_name,
                            sales_contact_no = part.sales_contact_no,
                            support_contact_name = part.support_contact_name,
                            support_contact_no = part.support_contact_no
                        }).First();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int EditPart(part data)
        {
            try
            {

                part part = _context.parts.Where(x => x.id == data.id).First();
                part.dt_acquisition = data.dt_acquisition;
                part.dt_warranty_exp = data.dt_warranty_exp;
                part.dt_installation = data.dt_installation;
                part.dt_cert = data.dt_cert;
                part.mfg_year = data.mfg_year;
                part.cert_no = data.cert_no;
                part.serial_no = data.serial_no;
                part.remark = data.remark;
                part.sales_contact_name = data.sales_contact_name;
                part.sales_contact_no = data.sales_contact_no;
                part.support_contact_name = data.support_contact_name;
                part.support_contact_no = data.support_contact_no;
                part.dt_modified = DateTime.Now;
                part.modified_by = data.modified_by;
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

        public int AddPart(part data)
        {
            try
            {
                _context.parts.Add(data);
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
        public IEnumerable<dynamic> GetPartModelSelection()
        {
            try
            {
                return (from pm in _context.part_model
                        orderby pm.name
                        select new
                        {
                            id = pm.id,
                            part_model = string.Concat(string.Concat(string.Concat(pm.name + " / ", pm.model_name), " / "), pm.code)
                        }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
