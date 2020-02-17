using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SuperStore.ViewModel;
using SuperStore.Models;
using AutoMapper;
using SuperStore.Common;


namespace SuperStore.Repository
{
    public class RolePermissionRepository
    {
        SuperStoreEntities _db = new SuperStoreEntities();
        public RolePermissionViewModel GetInterfaceFromRolePermissionByRoleId(int roleId)
        {
            try
            {
                RolePermissionViewModel model = new RolePermissionViewModel();

                var selectedInterfaces =
                     (from permission in _db.tblRolePermissions
                      join ineterfaces in _db.tblInterfaces on permission.InterfaceId equals ineterfaces.InterfaceId
                      where permission.RoleId == roleId
                      select new InterfaceViewModel()
                      {
                          InterfaceId = ineterfaces.InterfaceId,
                          Name = ineterfaces.Name
                      }).ToList();

                var allInterfaces = _db.tblInterfaces.ToList();
                var result = allInterfaces.Where(x => !selectedInterfaces.Any(y => y.InterfaceId == x.InterfaceId)).ToList();
                var remainingInterfaces = Mapper.Map<List<tblInterface>, List<InterfaceViewModel>>(result);
                model.SelectedInterfaces = selectedInterfaces;
                model.Interfaces = remainingInterfaces;
                return model;
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return null;
            }
        }

        public bool AddRolePermission(RolePermissionViewModel list)
        {
            try
            {
                int added = 0;
                List<tblRolePermission> intrfacelist = _db.tblRolePermissions.Where(x => x.RoleId == list.RoleId).ToList();
                if (list.InterfaceArr != null)
                {
                    var remainingInterface = list.InterfaceArr.Where(x => !intrfacelist.Select(y => y.InterfaceId).Contains(x)).ToList(); // Pass
                    var delRecord = intrfacelist.Where(x => !list.InterfaceArr.Any(y => y == x.InterfaceId)).ToList();
                    //var remainingInterface = intrfacelist.Where(x => !list.InterfaceArr.Contains(x.InterfaceId)).ToList();
                    List<tblRolePermission> rolePermission = new List<tblRolePermission>();
                    for (int i = 0; i < remainingInterface.Count(); i++)
                    {
                        rolePermission.Add(new tblRolePermission()
                        {
                            Active = list.Active,
                            Archived = list.Archived,
                            CreatedBy = list.CreatedBy,
                            CreatedDate = list.CreatedDate,
                            InterfaceId = remainingInterface[i],
                            RoleId = list.RoleId
                        });
                    }
                    _db.tblRolePermissions.AddRange(rolePermission);
                    if (delRecord.Count > 0)
                        _db.tblRolePermissions.RemoveRange(delRecord);
                }
                else
                    _db.tblRolePermissions.RemoveRange(intrfacelist);
                added = _db.SaveChanges();
                return added > 0 ? true : false;
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return false;
            }
        }
    }
}