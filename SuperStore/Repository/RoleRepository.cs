using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using SuperStore.ViewModel;
using SuperStore.Models;
using SuperStore.Common;
using AutoMapper;

namespace SuperStore.Repository
{
    public class RoleRepository
    {
        SuperStoreEntities _db = new SuperStoreEntities();
        public List<RoleViewModel> GetAllRole()
        {
            SuperStoreEntities _db = new SuperStoreEntities();
            try
            {
                var role = (from ro in _db.tblRoles
                            where ro.Active == true
                            select new RoleViewModel()
                            {
                                RoleId = ro.RoleId,
                                Name = ro.Name,
                                Description = ro.Description,
                                CreatedBy = ro.CreatedBy,
                                CreatedDate = ro.CreatedDate
                            }).ToList();
                return role;
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return null;
            }
        }

        public bool AddRole(RoleViewModel model)
        {
            try
            {
                var role = new tblRole()
                {
                    Name = model.Name,
                    Description = model.Description,
                    Active = true,
                    CreatedBy = model.CreatedBy,
                    CreatedDate = DateTime.Now,
                    ModifyBy = model.ModifyBy,
                    ModifiedDate = DateTime.Now
                };
                _db.tblRoles.Add(role);
                int added = _db.SaveChanges();
                return added > 0 ? true : false;
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return false;
            }
        }

        public RoleViewModel Edit(int id)
        {
            try
            {
                return Mapper.Map<tblRole, RoleViewModel>
                      (_db.tblRoles.Where(x => x.RoleId == id && x.Active == true).FirstOrDefault());
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return null;
                throw;
            }
        }

        public bool UpdateRole(RoleViewModel model)
        {
            try
            {
                var role = _db.tblRoles.Where(x => x.RoleId == model.RoleId).FirstOrDefault();
                if (role != null)
                {
                    role.Name = model.Name;
                    role.Description = model.Description;
                    role.ModifyBy = model.ModifyBy;
                    role.ModifiedDate = DateTime.Now;
                    int update = _db.SaveChanges();
                    return update > 0 ? true : false;
                }
                return false;
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return false;
            }
        }

        public bool Deactive(RoleViewModel model)
        {
            try
            {
                var role = _db.tblRoles.Where(x => x.RoleId == model.RoleId).FirstOrDefault();
                if (role != null)
                {
                    role.Active = model.Active;
                    role.ModifyBy = model.ModifyBy;
                    role.ModifiedDate = DateTime.Now;
                    int update = _db.SaveChanges();
                    return update > 0 ? true : false;
                }
                return false;
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return false;
            }
        }
    }
}