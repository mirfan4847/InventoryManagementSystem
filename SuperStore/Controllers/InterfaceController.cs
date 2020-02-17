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

    public class InterfaceController : Controller
    {
        InterfaceService _Service = new InterfaceService();
        // GET: Interface
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetAll()
        {
            try
            {
                return View(_Service.GetAll());
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return null;
            }
        }

        [HttpGet]
        public ActionResult Add()
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
        public ActionResult Add(InterfaceViewModel model)
        {
            try
            {
                bool result = false;
                if (model.InterfaceId == 0)
                {
                    model.Active = true;
                    model.CreatedBy = Convert.ToInt32(Session[SessionVariables.UserId]);
                    model.CreatedDate = DateTime.Now;
                    result = _Service.AddInterface(model);
                }
                else
                {
                    model.UpdatedBy = Convert.ToInt32(Session[SessionVariables.UserId]);
                    model.UpdatedDate = DateTime.Now;
                    result = _Service.Update(model);
                }
                if (result)
                {
                    return RedirectToAction("Index");
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
                return View(_Service.Edit(id));
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return null;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(InterfaceViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    model.UpdatedBy = Convert.ToInt32(Session[SessionVariables.UserId]);
                    model.UpdatedDate = DateTime.Now;
                    if (_Service.Update(model))
                    {
                        return View("Index");
                    }
                    //return Json(, JsonRequestBehavior.AllowGet);

                }
                //return Json(false, JsonRequestBehavior.AllowGet);
                return View();
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return null;
            }
        }
        public ActionResult Deactive(int id)
        {
            try
            {
                InterfaceViewModel model = new InterfaceViewModel();
                model.Active = false;
                model.UpdatedBy = Convert.ToInt32(Session[SessionVariables.UserId]);
                model.UpdatedDate = DateTime.Now;
                model.InterfaceId = id;
                return Json(_Service.Deactive(model), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return null;
            }
        }
    }
}