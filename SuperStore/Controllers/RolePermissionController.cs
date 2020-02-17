using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SuperStore.Common;
using SuperStore.ViewModel;
using SuperStore.Service;

namespace SuperStore.Controllers
{

    [SessionExpire]
    [Authorize]
    public class RolePermissionController : Controller
    {
        RolePermissionService _service = new RolePermissionService();
        // GET: RolePermission
        public ActionResult Index()
        {
            RolePermissionViewModel rolePermisssion = new RolePermissionViewModel();
            RoleService _role = new RoleService();
            List<RoleViewModel> roleList = new List<RoleViewModel>();
            roleList.Add(new RoleViewModel() { RoleId = 0, Name = "---Select Role---" });
            roleList.AddRange(_role.GetAllRole());
            rolePermisssion.Roles = roleList;
            return View(rolePermisssion);
        }

        public ActionResult GetRolePermission(int RoleId)
        {
            try
            {
                var result = _service.GetInterfaceFromRolePermissionByRoleId(RoleId);
                return View(result);
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return null;
            }
        }

        public ActionResult AddRolePermission(RolePermissionViewModel model)
        {
            try
            {
                model.Active = true;
                model.Archived = false;
                model.CreatedBy = Convert.ToInt32(Session[SessionVariables.UserId]);
                model.CreatedDate = DateTime.Now;
                return Json(_service.AddRolePermission(model), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return null;
            }
        }
    }
}