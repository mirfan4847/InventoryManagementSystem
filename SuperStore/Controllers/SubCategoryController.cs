using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using SuperStore.ViewModel;
using SuperStore.Service;
using SuperStore.Common;
using SuperStore.Repository;

namespace SuperStore.Controllers
{
    [SessionExpire]
    [Authorize]
    public class SubCategoryController : Controller
    {
        SubCategoryService _Service = new SubCategoryService();
        // GET: SubCategory
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
        public ActionResult AddSubCategory()
        {
            try
            {
                SubCategoryViewModel model = new SubCategoryViewModel();
                model.listCategory = CategoryRepository.ddlCategory();
                return View(model);

            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return null;
            }
        }
        [HttpPost]
        public ActionResult AddSubCategory(SubCategoryViewModel model)
        {
            try
            {
                model.Active = true;
                model.CreateBy = Convert.ToInt32(Session[SessionVariables.UserId]);
                model.CreatedDate = DateTime.Now;
                model.ModifyBy = Convert.ToInt32(Session[SessionVariables.UserId]);
                model.ModifyDate = DateTime.Now;
                return Json(_Service.Add(model), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return null;
            }
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            try
            {
                SubCategoryViewModel model = new SubCategoryViewModel();
                model = _Service.Edit(id);
                model.listCategory = CategoryRepository.ddlCategory();
                return View(model);
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return null;
            }
        }
        [ActionName("Edit")]
        [HttpPost]
        public ActionResult Update(SubCategoryViewModel model)
        {
            try
            {
                model.ModifyBy = Convert.ToInt32(Session[SessionVariables.UserId]);
                model.ModifyDate = DateTime.Now;
                _Service.Update(model);
                //return View(_Repository.Update(model));
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return null;
            }
        }

        [HttpGet]
        public ActionResult Deactive(int id)
        {
            try
            {
                SubCategoryViewModel model = new SubCategoryViewModel();
                model.Active = false;
                model.ModifyBy = Convert.ToInt32(Session[SessionVariables.UserId]);
                model.ModifyDate = DateTime.Now;
                model.SubCategoryId = id;
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