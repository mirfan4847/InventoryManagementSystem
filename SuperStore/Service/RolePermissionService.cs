using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SuperStore.ViewModel;
using SuperStore.Repository;

namespace SuperStore.Service
{
    public class RolePermissionService
    {
        RolePermissionRepository _repository = new RolePermissionRepository();
        public RolePermissionViewModel GetInterfaceFromRolePermissionByRoleId(int roleId)
        {
            return
                _repository.GetInterfaceFromRolePermissionByRoleId(roleId);
        }

        public bool AddRolePermission(RolePermissionViewModel model)
        {
            return
                _repository.AddRolePermission(model);
        }
    }
}