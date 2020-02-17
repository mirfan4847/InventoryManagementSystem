using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using SuperStore.ViewModel;
using SuperStore.Common;
using SuperStore.Repository;

namespace SuperStore.Controllers
{
    [SessionExpire]
    [Authorize]
    public class RoleController : Controller
    {
        RoleRepository _Repository = new RoleRepository();
        // GET: Role
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetAll()
        {
            try
            {
                return View(_Repository.GetAllRole());
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return null;
            }
        }

        public ActionResult AddRole()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddRole(RoleViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    model.CreatedBy = Convert.ToInt32(Session[SessionVariables.UserId]);
                    model.Active = true;
                    model.CreatedDate = DateTime.Now;
                    if (_Repository.AddRole(model))
                        return View("Index");
                }
                return View();
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return null;
            }
        }

        public ActionResult Edit(string id)
        {
            try
            {
                int roleId = Convert.ToInt32(EncryptDecrypt.DecryptId(id));
                return View(_Repository.Edit(roleId));
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return null;
            }
        }

        [HttpPost]
        public ActionResult Edit(RoleViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    model.ModifyBy = Convert.ToInt32(Session[SessionVariables.UserId]);
                    model.ModifiedDate = DateTime.Now;
                    if (_Repository.UpdateRole(model))
                        return View("Index");
                    else
                        return View();
                }
                return View();
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return null;
            }
        }

        public ActionResult Deactive(string id)
        {
            try
            {
                RoleViewModel model = new RoleViewModel();
                model.RoleId = Convert.ToInt32(EncryptDecrypt.DecryptId(id));
                model.Active = false;
                model.ModifyBy = Convert.ToInt32(Session[SessionVariables.UserId]);
                model.ModifiedDate = DateTime.Now;
                if (_Repository.Deactive(model))
                    return View("Index");
                else
                    return View();
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return null;
            }
        }
    }
}