using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

    }
}
