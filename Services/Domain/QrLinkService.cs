using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Entities;
using WebApi.Helpers;
using WebApi.Services;

namespace WebApi.Services
{
    public class QrLinkService : IQrLinkService
    {
        private DataContext _context;

        public QrLinkService(DataContext context)
        {
            _context = context;
        }

        public dynamic GetPartBySerialNo(string serialNo)
        {
            try
            {
                return (from qrcode_link in _context.qrcode_link
                        where qrcode_link.qr_id == serialNo
                        select new
                        {
                            id = qrcode_link.id,
                            qr_id = qrcode_link.qr_id,
                            qr_type = qrcode_link.qr_type
                        }).First();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
