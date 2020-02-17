using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SuperStore.Common;
using SuperStore.ViewModel;
using SuperStore.Models;
using AutoMapper;

namespace SuperStore.Repository
{
    public class UnitRepository
    {
        SuperStoreEntities _db = new SuperStoreEntities();

        public static List<UnitViewModel> ddlUnit()
        {
            try
            {
                SuperStoreEntities _db = new SuperStoreEntities();

                return Mapper.Map<List<tblUnit>, List<UnitViewModel>>
                    (_db.tblUnits.Where(x => x.Active == true).ToList());
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return null;
            }

        }


        public bool Create(UnitViewModel model)
        {
            try
            {
                var unit = new tblUnit()
                {
                    Active = model.Active,
                    CreatedBy = model.CreatedBy,
                    CreatedDate = model.CreatedDate,
                    Description = model.Description,
                    Name = model.Name
                };
                _db.tblUnits.Add(unit);
                int Added = _db.SaveChanges();
                return Added > 0 ? true : false;
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return false;
            }
        }

        public UnitViewModel Edit(int id)
        {
            try
            {
                var unit = (from un in _db.tblUnits
                            where un.UnitId == id && un.Active == true
                            select new UnitViewModel()
                            {
                                Description = un.Description,
                                Name = un.Name,
                                UnitId = un.UnitId
                            }).SingleOrDefault();

                return unit;
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return null;
            }
        }


        public bool Update(UnitViewModel model)
        {
            try
            {
                int update = 0;
                var unit = _db.tblUnits.Where(x => x.UnitId == model.UnitId && x.Active == true).SingleOrDefault();
                if (unit != null)
                {
                    unit.Description = model.Description;
                    unit.ModifyBy = model.ModifyBy;
                    unit.ModifyDate = model.ModifyDate;
                    unit.Name = model.Name;
                    update = _db.SaveChanges();
                }
                return update > 0 ? true : false;
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return false;
            }
        }


        public bool Deactive(UnitViewModel model)
        {
            try
            {
                int deactive = 0;
                var unit = _db.tblUnits.Where(x => x.UnitId == model.UnitId && x.Active == true).SingleOrDefault();
                if (unit != null)
                {
                    unit.Active = model.Active;
                    unit.ModifyBy = model.ModifyBy;
                    unit.ModifyDate = model.ModifyDate;
                    deactive = _db.SaveChanges();
                }
                return deactive > 0 ? true : false;
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return false;
            }
        }

        public List<UnitViewModel> GetAll()
        {
            try
            {
                return
                    Mapper.Map<List<tblUnit>, List<UnitViewModel>>
                    (_db.tblUnits.Where(x => x.Active == true).ToList());
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return null;
            }
        }
    }
}