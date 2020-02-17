using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using SuperStore.Common;
using SuperStore.Repository;
using SuperStore.ViewModel;

namespace SuperStore.Service
{
    public class RoleService
    {
        RoleRepository _Repository = new RoleRepository();

        public bool AddRole(RoleViewModel model)
        {
            try
            {
                return
                    _Repository.AddRole(model);
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return false;
            }
        }

        public List<RoleViewModel> GetAllRole()
        {
            try
            {
                return
                    _Repository.GetAllRole();
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return null;
            }
        }

        public RoleViewModel EditRole(int id)
        {
            try
            {
                return
                    _Repository.Edit(id);
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return null;
            }
        }

        public bool UpdateRole(RoleViewModel model)
        {
            try
            {
                return
                    _Repository.UpdateRole(model);
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
                return
                    _Repository.Deactive(model);
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return false;
            }
        }
    }
}