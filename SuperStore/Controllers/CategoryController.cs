using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using SuperStore.ViewModel;
using SuperStore.Common;
using SuperStore.Service;
using SuperStore.Common;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.IO;
using SuperStore.Models;

namespace SuperStore.Controllers
{
    [SessionExpire]
    [Authorize]
    public class CategoryController : Controller
    {
        CategoryService _Service = new CategoryService();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetAll()
        {
            try
            {
                return View(_Service.AllCategory());
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return null;
            }
        }

        [HttpGet]
        public ActionResult AddCategory()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddCategory(CategoryViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    model.CreatedBy = Convert.ToInt32(Session[SessionVariables.UserId]);
                    model.CreatedDate = DateTime.Now;
                    model.Active = true;

                    return Json(_Service.AddCategory(model), JsonRequestBehavior.AllowGet);
                }
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return null;
            }
        }

        [HttpGet]
        public ActionResult Edit(string id)
        {
            try
            {
                int categoryid = Convert.ToInt32(EncryptDecrypt.DecryptId(id));
                return View(_Service.Edit(categoryid));
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return null;
            }
        }

        //[ActionName("Edit")]
        [HttpPost]
        public ActionResult Update(CategoryViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    model.ModifyBy = Convert.ToInt32(Session[SessionVariables.UserId]);
                    model.ModifyDate = DateTime.Now;
                    return Json(_Service.Update(model), JsonRequestBehavior.AllowGet);
                    //return RedirectToAction("Index");
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
                if (ModelState.IsValid)
                {
                    CategoryViewModel model = new CategoryViewModel();
                    model.CategoryId = Convert.ToInt32(EncryptDecrypt.DecryptId(id));
                    model.Active = false;
                    model.ModifyBy = Convert.ToInt32(Session[SessionVariables.UserId]);
                    model.ModifyDate = DateTime.Now;
                    if (_Service.Deactive(model))
                        return RedirectToAction("Index");
                    else
                        return View();
                    //return Json(_Repository.Deactive(model), JsonRequestBehavior.AllowGet);
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

        public ActionResult ExportReports()
        {
            SuperStoreEntities _db = new SuperStoreEntities();
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Reports"), "AllCategory.rpt"));
            rd.SetDataSource(_db.tblCategories.ToList());
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            try
            {
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                return File(stream, "application/pdf", "AllCategory.rpt");
            }
            catch (Exception)
            {

                throw;
            }

        }

    }
}