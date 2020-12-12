using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Entities;
using WebApi.Helpers;

namespace WebApi.Services
{
	public class EquipmentModelService : IEquipmentModelService
	{
		private DataContext _context;

		public EquipmentModelService(DataContext context)
		{
			_context = context;
		}
		public IEnumerable<dynamic> GetModelSelection()
		{
			try
			{
				return (from em in _context.equipment_model
						orderby em.name
						select new
						{
							id = em.id,
							model_name = string.Concat(em.name + " / ", em.model_name)
						}).ToList();
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public dynamic GetModelSelectionById(int id)
		{
			try
			{
				return (from em in _context.equipment_model
						where em.id == id
						orderby em.name
						select new
						{
							id = em.id,
							model_name = string.Concat(em.name + " / ", em.model_name)
						}).FirstOrDefault();
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		public IEnumerable<dynamic> GetEquipmentModelList()
		{
			try
			{
				return (from em in _context.equipment_model
						orderby em.name
						select new
						{
							id = em.id,
							equipmentName = em.name,
							processName = em.process_name,
							manufacturer = em.mfg_name,
							modelName = em.model_name
						}).ToList();
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public IEnumerable<dynamic> GetEquipmentModelProcessList()
		{
			try
			{
				return (from em in _context.equipment_model
						where em.process_name != (string)null && em.process_name != string.Empty
						orderby em.process_name
						select new
						{
							em.process_name
						}).Distinct().ToList();
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public dynamic GetEquipmentModelById(int id)
		{
			try
			{
				if (id == 0)
				{
					return new equipment_model();
				}
				return (from em in _context.equipment_model
						join e in _context.equipment_type on em.equipment_type_id equals e.id into eJoin
						from ej in eJoin.DefaultIfEmpty()
						where em.id == id
						select new
						{
							id = em.id,							
							equipmentName = em.name,
							equipmentTypeId = ej.id,
							equipmentType = ((ej != null) ? ej.name : ""),
							processName = em.process_name,
							modelName = em.model_name,
							modelNo = em.model_no,
							manufacturer = em.mfg_name,
							sales_contact_name = em.sales_contact_name,
							sales_contact_no = em.sales_contact_no,
							support_contact_name = em.support_contact_name,
							support_contact_no = em.support_contact_no,
							remarks = em.remark
						}).First();
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public int AddEquipmentModel(equipment_model data)
		{
			try
			{
				_context.equipment_model.Add(data);
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

		public int EditEquipmentModel(equipment_model data)
		{
			try
			{				
				equipment_model equipment_model = _context.equipment_model.Where((equipment_model em) => em.id == data.id).First();
				equipment_model.name = data.name;
				equipment_model.equipment_type_id = data.equipment_type_id;
				equipment_model.process_name = data.process_name;
				//equipment_model.model_name = data.model_name;
				equipment_model.model_no = data.model_no;
				equipment_model.mfg_name = data.mfg_name;
				equipment_model.sales_contact_name = data.sales_contact_name;
				equipment_model.sales_contact_no = data.sales_contact_no;
				equipment_model.support_contact_name = data.support_contact_name;
				equipment_model.support_contact_no = data.support_contact_no;
				equipment_model.remark = data.remark;
				equipment_model.dt_modified = data.dt_modified;
				equipment_model.modified_by = data.modified_by;
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
		public bool DeleteEquipmentModelField(int id)
		{
			try
			{
				part_model_field partToDelete = _context.part_model_field.Where((part_model_field ef) => ef.id == id).FirstOrDefault();
				_context.part_model_field.Remove(partToDelete);
				_context.SaveChanges();
				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}
		public IEnumerable<dynamic> GetTotalEquipmentModel()
		{
			try
			{				
				var source = (from e in _context.equipments
							  join em in _context.equipment_model on e.equipment_model_id equals em.id into emJoin
							  from emj in emJoin.DefaultIfEmpty()
							  select new
							  {
								  equipment_model_name = string.Concat(emj.name + "/", emj.model_name),
								  id = emj.id
							  } into x
							  orderby x.equipment_model_name
							  select x).ToList();
				return from p in source
					   group p by new
					   {
						   name = p.equipment_model_name,
						   Id = p.id
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
	}

}
