using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using SuperStore.ViewModel;
using SuperStore.Repository;
using SuperStore.Common;
using SuperStore.Service;

namespace SuperStore.Controllers
{
    [SessionExpire]
    [Authorize]
    public class SupplierController : Controller
    {
        SupplierService _Service = new SupplierService();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AllSupplier()
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

        public ActionResult AddSupplier(string id = "")
        {
            try
            {
                SupplierViewModel model = new SupplierViewModel();
                if (string.IsNullOrEmpty(id))
                    return View(model);
                else
                {
                    int Supplierid = Convert.ToInt32(EncryptDecrypt.DecryptId(id));
                    return View(_Service.Edit(Supplierid));
                }

            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return null;
            }
        }

        [HttpPost]
        public ActionResult AddSupplier(SupplierViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (model.SupplierId == 0)
                    {
                        model.Active = true;
                        model.CreatedBy = Convert.ToInt32(Session[SessionVariables.UserId]);
                        model.CreatedDate = DateTime.Now;
                        return View(_Service.AddSupplier(model));
                    }
                    else
                    {
                        model.ModifyBy = Convert.ToInt32(Session[SessionVariables.UserId]);
                        model.ModifiedDate = DateTime.Now;
                        return View(_Service.Update(model));
                    }
                }
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
                SupplierViewModel model = new SupplierViewModel();
                model.Active = false;
                model.ModifyBy = Convert.ToInt32(Session[SessionVariables.UserId]);
                model.ModifiedDate = DateTime.Now;
                model.SupplierId = id;
                _Service.Deactive(model);
                return View("Index");
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return null;
            }
        }

    }
}