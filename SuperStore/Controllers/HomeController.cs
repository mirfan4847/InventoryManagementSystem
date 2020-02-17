using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using SuperStore.Repository;
using SuperStore.ViewModel;
using SuperStore.Service;

namespace SuperStore.Controllers
{
    public class HomeController : Controller
    {
        SubCategoryService _subCategory = new SubCategoryService();
        ProductService _product = new ProductService();
        UserService _user = new UserService();
        SupplierService _supplier = new SupplierService();
        public ActionResult Index()
        {
            ViewBag.Category = CategoryRepository.ddlCategory().Count;
            ViewBag.SubCategory = _subCategory.GetAll().Count;
            ViewBag.Products = _product.GetAllProduct().Count;
            ViewBag.Users = _user.GetAllUsers().Count;
            ViewBag.Suppliers = _supplier.GetAll().Count;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


    }
}