using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using SuperStore.Common;
using SuperStore.Service;
using SuperStore.ViewModel;

namespace SuperStore.Controllers
{
    [SessionExpire]
    [Authorize]
    public class UnitController : Controller
    {
        UnitService _service = new UnitService();

        // GET: Unit
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult GetAll()
        {
            try
            {
                return View(_service.GetAll());
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return null;
            }
        }

        public ActionResult Create()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return null;
            }
        }

        [HttpPost]
        public ActionResult Create(UnitViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    model.Active = true;
                    model.CreatedBy = Convert.ToInt32(Session[SessionVariables.UserId]);
                    model.CreatedDate = DateTime.Now;
                    return Json(_service.Create(model), JsonRequestBehavior.AllowGet);
                }
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return null;
            }
        }

        public ActionResult Edit(int id)
        {
            try
            {
                return View(_service.Edit(id));
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return null;
            }
        }

        [HttpPost]
        public ActionResult Update(UnitViewModel model)
        {
            try
            {
                model.ModifyBy = Convert.ToInt32(Session[SessionVariables.UserId]);
                model.ModifyDate = DateTime.Now;
                return Json(_service.Update(model), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return null;
            }
        }

        [HttpPost]
        public ActionResult Deactive(int id)
        {
            try
            {
                UnitViewModel model = new UnitViewModel();
                model.Active = false;
                model.ModifyBy = Convert.ToInt32(Session[SessionVariables.UserId]);
                model.ModifyDate = DateTime.Now;
                model.UnitId = id;
                return Json(_service.Deactive(model), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return null;
            }
        }
    }
}