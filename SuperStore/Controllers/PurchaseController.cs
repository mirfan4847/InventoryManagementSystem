using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SuperStore.Common;
using SuperStore.Service;
using SuperStore.ViewModel;
using SuperStore.Repository;
using System.Web.Http;

namespace SuperStore.Controllers
{
    [SessionExpire]
    [System.Web.Http.Authorize]
    public class PurchaseController : Controller
    {
        PurchaseService _Service = new PurchaseService();
        ProductService _ProductService = new ProductService();
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

        public ActionResult PurchasedProduct()
        {
            try
            {
                PurchaseViewModel model = new PurchaseViewModel();
                model.listCategory = CategoryRepository.ddlCategory();
                model.listCategory.Add(new CategoryViewModel() { CategoryId = 0, CategoryName = "--- Select Category ---" });
                model.listSupplier = SupplierRepository.GetSupplier();
                model.listSupplier.Add(new SupplierViewModel() { SupplierId = 0, Name = "--- Select Supplier ---" });
                model.listUnit = UnitRepository.ddlUnit();
                model.listUnit.Add(new UnitViewModel() { UnitId = 0, Name = "--- Select Unit ---" });
                return View(model);
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return null;
            }
        }

        [System.Web.Http.HttpPost]
        public ActionResult PurchasedProduct_Post([FromBody] PurchaseViewModel model)
        {
            try
            {
                model.Active = true;
                model.CreatedBy = Convert.ToInt32(Session[SessionVariables.UserId]);
                model.CreatedDate = DateTime.Now;
                model.ModifyBy = Convert.ToInt32(Session[SessionVariables.UserId]);
                model.ModifiedDate = DateTime.Now;
                model.QuantityAlert = 5;
                model.ReceiptNo = System.Guid.NewGuid().ToString().Substring(0, 6);
                List<PurchaseDetailViewModel> listpurchaseDetail = new List<PurchaseDetailViewModel>();
                if (model.ArrPurchaseDetail.Count > 0)
                {
                    for (int i = 0; i < model.ArrPurchaseDetail.Count; i++)
                    {
                        listpurchaseDetail.Add(new PurchaseDetailViewModel()
                        {
                            Active = true,
                            CreatedBy = Convert.ToInt32(Session[SessionVariables.UserId]),
                            CreatedDate = DateTime.Now,
                            CostPrice = model.ArrPurchaseDetail[i].CostPrice,
                            ProductId = model.ArrPurchaseDetail[i].ProductId,
                            Quantity = model.ArrPurchaseDetail[i].Quantity,
                            RetailPrice = model.ArrPurchaseDetail[i].RetailPrice,
                            UnitId = model.ArrPurchaseDetail[i].UnitId
                        });
                    }
                }

                model.listPurchaseDetail = listpurchaseDetail;
                if (_Service.PurchaseProduct(model))
                {
                    Session["purchaseList"] = null;
                    return Json(true, JsonRequestBehavior.AllowGet);
                }
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return null;
            }
        }


        public ActionResult change(int Id)
        {
            try
            {
                ProductViewModel model = new ProductViewModel();
                model.listCategory = CategoryRepository.ddlCategory();
                return View();
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return null;
            }
        }

        public ActionResult change(ProductViewModel model)
        {
            try
            {
                model.ModifyBy = Convert.ToInt32(Session[SessionVariables.UserId]);
                model.ModifyDate = DateTime.Now;
                return Json(_Service.Update(model), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return null;
            }
        }

        public ActionResult PurchasedDetail(string Id)
        {
            try
            {
                int purchasedId = Convert.ToInt32(EncryptDecrypt.DecryptId(Id));
                return View(_Service.GetProductPurchasedList(purchasedId));
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return null;
            }
        }


        public ActionResult PurchasedPro()
        {
            try
            {
                PurchaseViewModel model = new PurchaseViewModel();
                List<ProductViewModel> product = new List<ProductViewModel>();
                List<UnitViewModel> unit = new List<UnitViewModel>();
                List<SupplierViewModel> supplier = new List<SupplierViewModel>();
                product.Add(new ProductViewModel() { ProductId = 0, Name = "---Select Product---" });
                product.AddRange(_ProductService.GetAllProduct());
                unit.Add(new UnitViewModel() { UnitId = 0, Name = "---Select Unit---" });
                unit.AddRange(UnitRepository.ddlUnit());
                model.listProduct = product;
                model.listUnit = unit;
                supplier.Add(new SupplierViewModel() { Name = "Walkin Supplier", SupplierId = 0 });
                supplier.AddRange(SupplierRepository.GetSupplier());
                model.listSupplier = supplier;
                return View(model);
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return null;
            }
        }

        [System.Web.Http.HttpPost]
        public ActionResult PurchasedPro_Post(PurchasedProducts model)
        {
            try
            {

                List<PurchasedProducts> PurchasedProductsList = new List<PurchasedProducts>();
                PurchasedProductsList.Add(new PurchasedProducts()
                {
                    CostPrice = model.CostPrice,
                    ProductId = model.ProductId,
                    ProductName = model.ProductName,
                    Quantity = model.Quantity,
                    RetailPrice = model.RetailPrice,
                    UnitId = model.UnitId,
                    UnitName = model.UnitName,
                    SubTotal = model.SubTotal
                });
                if (Session["purchaseList"] != null)
                {
                    var purchaseData = (List<PurchasedProducts>)Session["purchaseList"];
                    if (model.ProductId != 0)
                        purchaseData.AddRange(PurchasedProductsList);
                    Session["purchaseList"] = null;
                    Session["purchaseList"] = purchaseData;
                    return PartialView("PurchasedPro_Post", purchaseData);
                }
                else
                {
                    if (model.ProductId != 0)
                        Session["purchaseList"] = PurchasedProductsList;
                }
                return PartialView("PurchasedPro_Post", PurchasedProductsList);
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return null;
            }
        }
    }



}