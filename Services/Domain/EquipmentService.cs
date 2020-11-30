using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApi.Entities;
using WebApi.Helpers;
using WebApi.Models.Equipment;

namespace WebApi.Services
{
    public class EquipmentService : IEquipmentService
    {
        private DataContext _context;

        public EquipmentService(DataContext context)
        {
            _context = context;
        }

        public IEnumerable<dynamic> GetEquipmentList()
        {
            try
            {
                return (from eq in _context.equipments
                        join eqm in _context.equipment_model on eq.equipment_model_id equals eqm.id into eqmJoin
                        from eqmj in eqmJoin.DefaultIfEmpty()
                        orderby eqmj.model_no, eqmj.model_name, eq.serial_no
                        select new
                        {
                            id = eq.id,
                            equipment_no = eq.equipment_no,
                            model_no = eqmj.model_no,
                            equipment_name_model_name = string.Concat(((eqmj != null) ? eqmj.name : "") + " / ", (eqmj != null) ? eqmj.model_name : ""),
                            serial_no = eq.serial_no,
                            location = eq.location,
                            //equipment_full_name = string.Concat(((eqmj != null) ? eqmj.name : "") + " / ", (eqmj != null) ? eqmj.model_name : ""),
                            //equipment_name_serial = string.Concat(((eqmj != null) ? eqmj.name : "") + " / ", eq.serial_no),
                            //equipment_model_id = eq.equipment_model_id
                        }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<dynamic> GetEquipmentLog(int equipmentId)
        {
            try
            {
                var query = (from wo in _context.work_order
                             join user in _context.Users on wo.assignee_user_id equals user.Id
                             join status in _context.wo_status on wo.wo_status_id equals status.id
                             join type in _context.wo_type on wo.wo_type_id equals type.id
                             where wo.equipment_id == equipmentId
                             orderby wo.dt_created descending
                             select wo).ToList();

                List<EquipmentTimeline> equipmentTimeline = new List<EquipmentTimeline> { };
                int vCount = 0;
                string vMonth="";
                foreach (var cx in query)
                {
                    EquipmentTimeline equipment = new EquipmentTimeline();
                    var statusQuery = (from status in _context.wo_status
                                       where status.id == cx.wo_status_id
                                       select status.name).FirstOrDefault();
                    var statusUser = (from user in _context.Users
                                      where user.Id == cx.assignee_user_id
                                      select user.FirstName).FirstOrDefault();
                    var statusType = (from type in _context.wo_type
                                      where type.id == cx.wo_type_id
                                      select type.name).FirstOrDefault();


                    equipment.content = @" <br>Assigned to: " + statusUser + @"<br>
                                               Date: " + cx.dt_created + @"<br>
                                               Status: " + statusQuery + @"<br>
                                            ";

                    string vDate = cx.dt_created.Value.Year + "-" + cx.dt_created.Value.Month + "-" + cx.dt_created.Value.Day;
                    equipment.date = vDate;
                    equipment.title = statusType + ": " + cx.wo_no;
                    if (vMonth == cx.dt_created.Value.ToString("MMMM"))
                    {
                        vCount = vCount + 1;
                        vMonth = cx.dt_created.Value.ToString("MMMM");
                    }
                    else
                    {
                        Title title = new Title();
                        title.title = cx.dt_created.Value.ToString("MMMM") + "," + cx.dt_created.Value.Year + "|" + vCount + " Entries";
                        equipment.titles = title;
                        vCount = 1;
                        vMonth = cx.dt_created.Value.ToString("MMMM");
                    }
                    
                    equipmentTimeline.Add(equipment);
                }
                return equipmentTimeline;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<dynamic> GetEquipmentViewList()
        {
            try
            {
                return (from eq in _context.equipments
                        from p in _context.policies
                        join eqm in _context.equipment_model on eq.equipment_model_id equals eqm.id into eqmJoin
                        from eqmj in eqmJoin.DefaultIfEmpty()
                        orderby eqmj.model_no, eqmj.model_name, eq.serial_no
                        select new
                        {
                            equipment_no = eq.equipment_no,
                            model_name = eqmj.model_name,
                            model_no = eqmj.model_no,
                            equipment_name_model_name = string.Concat(((eqmj != null) ? eqmj.name : "") + " / ", (eqmj != null) ? eqmj.model_name : ""),
                            serial_no = eq.serial_no,
                            dt_acquisition = eq.dt_acquisition,
                            life_span = p.life_span,
                            location = eq.location
                        }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<dynamic> GetEquipmentLocationList()
        {
            try
            {
                return (from em in _context.equipments
                        where em.location != (string)null && em.location != string.Empty
                        orderby em.location
                        select new
                        {
                            em.location
                        }).Distinct().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<dynamic> GetEquipmentRepairReplaceList()
        {
            try
            {
                return (from eq in _context.equipments
                        join eqm in _context.equipment_model on eq.equipment_model_id equals eqm.id into eqmJoin
                        from eqmj in eqmJoin.DefaultIfEmpty()
                        orderby eqmj.model_no, eqmj.model_name, eq.serial_no
                        select new
                        {
                            id = eq.id,
                            equipment_no = eq.equipment_no,
                            equipment_name_model_name = string.Concat(((eqmj != null) ? eqmj.name : "") + " / ", (eqmj != null) ? eqmj.model_name : ""),
                            FutureEqTCO = 20000 + eq.id,
                            FutureNewEqTCO = 20100 + eq.id
                        }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic GetEquipmentById(int id)
        {
            try
            {
                if (id == 0)
                {
                    return new equipment();
                }
                return (from eqm in _context.equipments
                            //join eq in _context.equipment_model on eqm.equipment_model_id equals eq.id into eqJoin
                            //from eqj in eqJoin.DefaultIfEmpty()
                            //join es in _context.equipment_status on eqm.equipment_status_id equals es.id into esJoin
                            //from esj in esJoin.DefaultIfEmpty()
                            //join p in _context.policies on eqm.equipment_model_id equals p.equipment_model_id into pJoin
                            //from pj in pJoin.DefaultIfEmpty()
                        where eqm.id == id
                        select new
                        {
                            id = eqm.id,
                            equipment_no = eqm.equipment_no,
                            equipment_model_id = eqm.equipment_model_id,
                            serial_no = eqm.serial_no,
                            mfg_year = eqm.mfg_year,
                            dt_acquisition = eqm.dt_acquisition,
                            //dt_warranty_exp = eqm.dt_warranty_exp,
                            dt_site_delivery = eqm.dt_site_delivery,
                            dt_installation = eqm.dt_installation,
                            dt_commissioning = eqm.dt_commissioning,
                            cert_no = eqm.cert_no,
                            //dt_cert = eqm.dt_cert,
                            //remark = eqm.remark,
                            //location = eqm.location,
                            //longitude = eqm.longitude,
                            //latitude = eqm.latitude,
                            //horsepower = eqm.horsepower,
                            //equipment_status_id = eqm.equipment_status_id,
                            sales_contact_name = eqm.sales_contact_name,
                            sales_contact_no = eqm.sales_contact_no
                            //support_contact_name = eqm.support_contact_name,
                            //support_contact_no = eqm.support_contact_no
                            //model_no = eqj.model_no,
                            //process_name = eqj.process_name,
                            //life_span = ((pj != null) ? pj.life_span : 0),
                            //equipment_full_name = string.Concat(((eqj != null) ? eqj.name : "") + " / ", (eqj != null) ? eqj.model_name : ""),
                            //equipment_status_name = ((esj != null) ? esj.name : "")
                        }).First();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic GetEquipmentRepairReplaceById(int id)
        {
            try
            {
                if (id == 0)
                {
                    return new equipment();
                }
                return (from eqm in _context.equipments
                        join eq in _context.equipment_model on eqm.equipment_model_id equals eq.id into eqJoin
                        from eqj in eqJoin.DefaultIfEmpty()
                        join es in _context.equipment_status on eqm.equipment_status_id equals es.id into esJoin
                        from esj in esJoin.DefaultIfEmpty()
                        join p in _context.policies on eqm.equipment_model_id equals p.equipment_model_id into pJoin
                        from pj in pJoin.DefaultIfEmpty()
                        join en in _context.estimated_nav on eqm.id equals en.equipment_id into enJoin
                        from enj in enJoin.DefaultIfEmpty()
                        join et in _context.equipment_type on eqj.equipment_type_id equals et.id into etJoin
                        from etj in etJoin.DefaultIfEmpty()
                        join cnv in _context.current_nav_equipment on eqm.id equals cnv.equipment_id into cnvJoin
                        from cnvj in cnvJoin.DefaultIfEmpty()
                        where eqm.id == id
                        select new
                        {
                            id = eqm.id,
                            equipment_no = eqm.equipment_no,
                            equipment_model_id = eqm.equipment_model_id,
                            process_name = eqj.process_name,
                            serial_no = eqm.serial_no,
                            life_span = ((pj != null) ? pj.life_span : 0),
                            purchase_value = ((enj != null) ? enj.purchase_value : 0m),
                            equipment_type = ((etj != null) ? etj.name : string.Empty),
                            current_nav = ((cnvj != null) ? cnvj.current_nav : 0m),
                            equipment_full_name = string.Concat(((eqj != null) ? eqj.name : "") + " / ", (eqj != null) ? eqj.model_name : ""),
                            equipment_name_model_name = string.Concat(((eqj != null) ? eqj.name : "") + " / ", (eqj != null) ? eqj.model_name : ""),
                            equipment_status_name = ((esj != null) ? esj.name : "")
                        }).First();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic GetEquipmentBySerialNo(string serialNo)
        {
            try
            {
                return (from eqm in _context.equipments
                            //join eq in _context.equipment_model on eqm.equipment_model_id equals eq.id into eqJoin
                            //from eqj in eqJoin.DefaultIfEmpty()
                            //join es in _context.equipment_status on eqm.equipment_status_id equals es.id into esJoin
                            //from esj in esJoin.DefaultIfEmpty()
                            //join p in _context.policies on eqm.equipment_model_id equals p.equipment_model_id into pJoin
                            //from pj in pJoin.DefaultIfEmpty()
                        where eqm.serial_no == serialNo
                        select new
                        {
                            id = eqm.id,
                            equipment_no = eqm.equipment_no,
                            equipment_model_id = eqm.equipment_model_id,
                            serial_no = eqm.serial_no,
                            mfg_year = eqm.mfg_year,
                            dt_acquisition = eqm.dt_acquisition,
                            //dt_warranty_exp = eqm.dt_warranty_exp,
                            dt_site_delivery = eqm.dt_site_delivery,
                            dt_installation = eqm.dt_installation,
                            dt_commissioning = eqm.dt_commissioning,
                            cert_no = eqm.cert_no,
                            //dt_cert = eqm.dt_cert,
                            //remark = eqm.remark,
                            //location = eqm.location,
                            //longitude = eqm.longitude,
                            //latitude = eqm.latitude,
                            //horsepower = eqm.horsepower,
                            //equipment_status_id = eqm.equipment_status_id,
                            sales_contact_name = eqm.sales_contact_name,
                            sales_contact_no = eqm.sales_contact_no
                            //support_contact_name = eqm.support_contact_name,
                            //support_contact_no = eqm.support_contact_no
                            //model_no = eqj.model_no,
                            //process_name = eqj.process_name,
                            //life_span = ((pj != null) ? pj.life_span : 0),
                            //equipment_full_name = string.Concat(((eqj != null) ? eqj.name : "") + " / ", (eqj != null) ? eqj.model_name : ""),
                            //equipment_status_name = ((esj != null) ? esj.name : "")
                        }).First();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic GetEditEquipmentByEquipmentNo(string equipmentNo)
        {
            try
            {
                return (from eqm in _context.equipments
                            //join eq in _context.equipment_model on eqm.equipment_model_id equals eq.id into eqJoin
                            //from eqj in eqJoin.DefaultIfEmpty()
                            //join es in _context.equipment_status on eqm.equipment_status_id equals es.id into esJoin
                            //from esj in esJoin.DefaultIfEmpty()
                            //join p in _context.policies on eqm.equipment_model_id equals p.equipment_model_id into pJoin
                            //from pj in pJoin.DefaultIfEmpty()
                        where eqm.equipment_no == equipmentNo
                        select new
                        {
                            id = eqm.id,
                            model_id = eqm.equipment_model_id,
                            status_id = eqm.equipment_status_id,
                            serial_no = eqm.serial_no,
                            manufacture_year = eqm.mfg_year,
                            acquisitionDate = eqm.dt_acquisition,
                            warrantyDate = eqm.dt_warranty_exp,
                            deliveryDate = eqm.dt_site_delivery,
                            installationDate = eqm.dt_installation,
                            commissioningDate = eqm.dt_commissioning,
                            sales_name = eqm.sales_contact_name,
                            sales_no = eqm.sales_contact_no,
                            support_name = eqm.support_contact_name,
                            support_no = eqm.support_contact_no
                        }).First();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic OpenEquipment(string equipment_no)
        {
            try
            {
                return (from eqm in _context.equipments
                            //join eq in _context.equipment_model on eqm.equipment_model_id equals eq.id into eqJoin
                            //from eqj in eqJoin.DefaultIfEmpty()
                            //join es in _context.equipment_status on eqm.equipment_status_id equals es.id into esJoin
                            //from esj in esJoin.DefaultIfEmpty()
                            //join p in _context.policies on eqm.equipment_model_id equals p.equipment_model_id into pJoin
                            //from pj in pJoin.DefaultIfEmpty()
                        where eqm.equipment_no == equipment_no
                        select new
                        {
                            id = eqm.id,
                            equipment_no = eqm.equipment_no,
                            equipment_model_id = eqm.equipment_model_id,
                            serial_no = eqm.serial_no,
                            mfg_year = eqm.mfg_year,
                            dt_acquisition = eqm.dt_acquisition,
                            //dt_warranty_exp = eqm.dt_warranty_exp,
                            dt_site_delivery = eqm.dt_site_delivery,
                            dt_installation = eqm.dt_installation,
                            dt_commissioning = eqm.dt_commissioning,
                            cert_no = eqm.cert_no,
                            //dt_cert = eqm.dt_cert,
                            //remark = eqm.remark,
                            //location = eqm.location,
                            //longitude = eqm.longitude,
                            //latitude = eqm.latitude,
                            //horsepower = eqm.horsepower,
                            //equipment_status_id = eqm.equipment_status_id,
                            sales_contact_name = eqm.sales_contact_name,
                            sales_contact_no = eqm.sales_contact_no
                            //support_contact_name = eqm.support_contact_name,
                            //support_contact_no = eqm.support_contact_no
                            //model_no = eqj.model_no,
                            //process_name = eqj.process_name,
                            //life_span = ((pj != null) ? pj.life_span : 0),
                            //equipment_full_name = string.Concat(((eqj != null) ? eqj.name : "") + " / ", (eqj != null) ? eqj.model_name : ""),
                            //equipment_status_name = ((esj != null) ? esj.name : "")
                        }).First();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public equipment GetEquipmentByNo(equipment data)
        {
            try
            {
                IQueryable<equipment> source = _context.equipments.Where((equipment e) => e.equipment_no == data.equipment_no);
                return source.First();
            }
            catch (Exception)
            {
                return new equipment();
            }
        }

        public IEnumerable<dynamic> GetEquipmentListById(int id)
        {
            try
            {
                return (from em in _context.equipment_model
                        join eq in _context.equipments on em.id equals eq.equipment_model_id
                        where em.id == id
                        orderby em.model_no, em.model_name, eq.serial_no
                        select new
                        {
                            equipment_full_name = string.Concat(string.Concat(string.Concat(em.name + " / ", em.model_name), " / "), eq.serial_no)
                        }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int AddEquipment(equipment data)
        {
            try
            {
                data.equipment_no = string.Empty;
                _context.equipments.Add(data);
                int num = _context.SaveChanges();
                equipment equipment = _context.equipments.Where((equipment t) => t.id == data.id).First();
                equipment.equipment_no = "EQ" + data.id.ToString().PadLeft(6, '0');
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

        public int EditEquipment(equipment data)
        {
            try
            {

                equipment equipment = _context.equipments.Where(x => x.id == data.id).First();
                equipment.equipment_model_id = data.equipment_model_id;
                equipment.serial_no = data.serial_no;
                equipment.mfg_year = data.mfg_year;
                equipment.dt_acquisition = data.dt_acquisition;
                equipment.dt_warranty_exp = data.dt_warranty_exp;
                equipment.dt_commissioning = data.dt_commissioning;
                equipment.dt_installation = data.dt_installation;
                equipment.dt_site_delivery = data.dt_site_delivery;
                equipment.cert_no = data.cert_no;
                equipment.dt_cert = data.dt_cert;
                equipment.remark = data.remark;
                equipment.location = data.location;
                equipment.longitude = data.longitude;
                equipment.latitude = data.latitude;
                equipment.horsepower = data.horsepower;
                equipment.equipment_status_id = data.equipment_status_id;
                equipment.sales_contact_name = data.sales_contact_name;
                equipment.sales_contact_no = data.sales_contact_no;
                equipment.support_contact_name = data.support_contact_name;
                equipment.support_contact_no = data.support_contact_no;
                equipment.dt_modified = data.dt_modified;
                equipment.modified_by = data.modified_by;
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

        public IEnumerable<dynamic> GetHomeTotalEqLocationChart()
        {
            try
            {
                var source = (from w in _context.equipments
                              where w.is_deleted == 0
                              select new
                              {
                                  w.location
                              }).ToList();
                return from p in source
                       group p by new
                       {
                           name = p.location
                       } into g
                       select new
                       {
                           name = g.Key,
                           count = g.Count()
                       };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<dynamic> GetHomeTotalEqProcessChart()
        {
            try
            {
                var source = (from w in _context.equipment_model
                              where w.is_deleted == 0
                              select new
                              {
                                  w.process_name
                              }).ToList();
                return from p in source
                       group p by new
                       {
                           name = p.process_name
                       } into g
                       select new
                       {
                           name = g.Key,
                           count = g.Count()
                       };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IEnumerable<dynamic> GetEquipmentSelection()
        {
            try
            {
                return (from em in _context.equipment_model
                        join eq in _context.equipments on em.id equals eq.equipment_model_id
                        orderby em.model_name, eq.serial_no
                        select new
                        {
                            id = eq.id,
                            equipmentName = string.Concat(string.Concat(string.Concat(em.name + " / ", em.model_name), " / "), eq.serial_no)
                        }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
