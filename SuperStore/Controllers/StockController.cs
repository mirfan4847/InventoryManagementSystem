using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using SuperStore.Common;
using SuperStore.Service;
using SuperStore.ViewModel;
using SuperStore.Repository;

namespace SuperStore.Controllers
{
    [SessionExpire]
    [Authorize]
    public class StockController : Controller
    {
        // GET: Stock
        StockService _service = new StockService();
        public ActionResult Index()
        {
            StockViewModel model = new StockViewModel();
            model.listCategory = CategoryRepository.ddlCategory();
            return View(model);
        }

        public ActionResult GetSubcategoryByCategoryId(int categoryId)
        {
            try
            {
                return Json(SubCategoryRepository.ddlSubCategory(categoryId), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return null;
            }
        }

        public ActionResult GetProductsBySubCategoryId(int SubcategoryId)
        {
            try
            {
                return Json(ProductRepository.ddlProductBySubcategory(SubcategoryId), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return null;
            }
        }


        public ActionResult ShowStock(string[] CategoryIds, string[] SubcategoryIds, string ProductsIds)
        {
            try
            {
                string category = string.Join(",", CategoryIds);
                string subCategory = string.Join(",", SubcategoryIds);
                var result = _service.GetStockDetail(category, subCategory, ProductsIds);
                return PartialView(result);
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return null;
            }
        }

    }
}