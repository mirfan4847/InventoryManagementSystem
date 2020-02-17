using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using SuperStore.Models;
using SuperStore.Common;
using SuperStore.ViewModel;
using AutoMapper;

namespace SuperStore.Repository
{
    public class InterfaceRepository
    {
        SuperStoreEntities _db = new SuperStoreEntities();
        public bool AddInterface(InterfaceViewModel model)
        {
            try
            {
                int Added = 0;
                var interfaces = new tblInterface()
                {
                    Description = model.Description,
                    Name = model.Name,
                    Active = model.Active,
                    Archived = model.Archived,
                    CreatedBy = model.CreatedBy,
                    CreatedDate = model.CreatedDate
                };
                _db.tblInterfaces.Add(interfaces);
                Added = _db.SaveChanges();
                return Added > 0 ? true : false;
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return false;
            }
        }

        public List<InterfaceViewModel> GetAll()
        {
            try
            {
                return Mapper.Map<List<tblInterface>, List<InterfaceViewModel>>
                      (_db.tblInterfaces.Where(x => x.Active == true).ToList());
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return null;
            }
        }

        public tblInterface Edit(int id)
        {
            try
            {
                return _db.tblInterfaces.Where(x => x.InterfaceId == id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return null;
            }
        }

        public bool Update(InterfaceViewModel model)
        {
            try
            {
                int update = 0;
                var interfaces = _db.tblInterfaces.Where(x => x.InterfaceId == model.InterfaceId).FirstOrDefault();
                if (interfaces != null)
                {
                    interfaces.Description = model.Description;
                    interfaces.Name = model.Name;
                    interfaces.UpdatedBy = model.UpdatedBy;
                    interfaces.UpdatedDate = model.UpdatedDate;
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

        public bool Deactive(InterfaceViewModel model)
        {
            try
            {
                int deactive = 0;
                var interfaces = _db.tblInterfaces.Where(x => x.InterfaceId == model.InterfaceId).FirstOrDefault();
                if (interfaces != null)
                {
                    interfaces.Active = model.Active;
                    interfaces.UpdatedBy = model.UpdatedBy;
                    interfaces.UpdatedDate = model.UpdatedDate;
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

        public static List<InterfaceViewModel> GetInterfaceByRoleId(int? roleId)
        {
            SuperStoreEntities _db = new SuperStoreEntities();
            return Mapper.Map<List<tblInterface>, List<InterfaceViewModel>>
                ((from interfaces in _db.tblInterfaces
                  join permission in _db.tblRolePermissions on interfaces.InterfaceId equals permission.InterfaceId
                  where permission.RoleId == roleId
                  select interfaces).ToList());
        }

    }
}