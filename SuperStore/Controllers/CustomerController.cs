using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SuperStore.Common;
using SuperStore.Service;
using SuperStore.ViewModel;
using System.IO;

namespace SuperStore.Controllers
{
    [SessionExpire]
    [Authorize]
    public class CustomerController : Controller
    {
        // GET: Customer
        CustomerService _service = new CustomerService();
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
        public ActionResult Create(CustomerViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string fileName = "";
                    HttpPostedFileBase file = null;
                    if (Request.Files.Count > 0)
                    {
                        file = Request.Files[0];

                        if (file != null && file.ContentLength > 0)
                        {
                            fileName = Path.GetFileName(file.FileName);
                            model.Image = fileName;
                        }
                    }

                    model.CreatedBy = Convert.ToInt32(Session[SessionVariables.UserId]);
                    model.CreatedDate = DateTime.Now;
                    model.Active = true;
                    if (_service.Create(model))
                    {
                        if (file != null)
                        {
                            string path = Path.Combine(Server.MapPath("~/Images/"), fileName);
                            file.SaveAs(path);
                        }
                        return Json(true, JsonRequestBehavior.AllowGet);
                    }
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
        public ActionResult Update(CustomerViewModel model)
        {
            try
            {
                model.UpdatedBy = Convert.ToInt32(Session[SessionVariables.UserId]);
                model.UpdatedDate = DateTime.Now;
                return View(_service.Update(model));
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
                CustomerViewModel model = new CustomerViewModel();
                model.Active = false;
                model.CustomerId = id;
                model.UpdatedBy = Convert.ToInt32(Session[SessionVariables.UserId]);
                return View(_service.Deactive(model));
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return null;
            }
        }

        public ActionResult CustomerPurchase()
        {
            try
            {
                return View(_service.GetSaleByUsers(Convert.ToInt32(Session[SessionVariables.UserId])));
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return null;
            }
        }
    }
}