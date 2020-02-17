using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SuperStore.Common;
using SuperStore.Repository;
using SuperStore.Service;
using SuperStore.ViewModel;
using System.Web.Http;
using System.Globalization;

namespace SuperStore.Controllers
{

    [SessionExpire]
    [System.Web.Http.Authorize]
    public class SaleController : Controller
    {
        SaleService _Service = new SaleService();
        ProductService _ProductService = new ProductService();
        CustomerService _customerService = new CustomerService();
        // GET: Sale
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetAll()
        {
            try
            {
                return View(_Service.GetSale());
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return null;
            }
        }

        public ActionResult GetSaleDetailBySaleId(int id)
        {
            try
            {
                return View(_Service.GetSaleDetailBySaleId(id));
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return null;
            }
        }


        public ActionResult CreateNewSale()
        {
            try
            {
                SaleViewModel model = new SaleViewModel();
                List<ProductViewModel> product = new List<ProductViewModel>();
                List<UnitViewModel> unit = new List<UnitViewModel>();
                List<CustomerViewModel> customer = new List<CustomerViewModel>();
                product.Add(new ProductViewModel() { ProductId = 0, Name = "---Select Product---" });
                product.AddRange(_ProductService.GetAllProduct());
                unit.Add(new UnitViewModel() { UnitId = 0, Name = "---Select Unit---" });
                unit.AddRange(UnitRepository.ddlUnit());
                model.listProduct = product;
                model.listUnit = unit;
                model.listCustomer = CustomerRepository.ddlCustomer();
                return View(model);
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return null;
            }
        }
        [System.Web.Http.HttpPost]
        public ActionResult CreateNewSale_Post(SaleViewModel model)
        {
            try
            {
                List<SaleViewModel> saleProductsList = new List<SaleViewModel>();
                saleProductsList.Add(new SaleViewModel()
                {
                    ProductId = model.ProductId,
                    ProductName = model.ProductName,
                    Quantity = model.Quantity,
                    Price = model.Price,
                    unitId = model.unitId,
                    UnitName = model.UnitName,
                    SubTotal = model.SubTotal
                });
                if (Session["saleList"] != null)
                {
                    var saleData = (List<SaleViewModel>)Session["saleList"];
                    if (model.ProductId != 0)
                        saleData.AddRange(saleProductsList);
                    Session["saleList"] = null;
                    Session["saleList"] = saleData;
                    return PartialView("CreateNewSale_Post", saleData);
                }
                else
                {
                    if (model.ProductId != 0)
                        Session["saleList"] = saleProductsList;
                }
                return PartialView("CreateNewSale_Post", saleProductsList);
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return null;
            }
        }


        [System.Web.Http.HttpPost]
        public ActionResult SaleItem_Post([FromBody] SaleViewModel model)
        {
            try
            {
                model.Active = true;
                model.CreatedBy = Convert.ToInt32(Session[SessionVariables.UserId]);
                model.CreatedDate = DateTime.Now;
                model.CreatedBy = Convert.ToInt32(Session[SessionVariables.UserId]);
                model.ReceiptNo = System.Guid.NewGuid().ToString().Substring(0, 6);
                List<SaleDetailViewModel> listpurchaseDetail = new List<SaleDetailViewModel>();
                if (model.ArrSaleDetail.Count > 0)
                {
                    for (int i = 0; i < model.ArrSaleDetail.Count; i++)
                    {
                        listpurchaseDetail.Add(new SaleDetailViewModel()
                        {
                            Active = true,
                            CreatedBy = Convert.ToInt32(Session[SessionVariables.UserId]),
                            CreatedDate = DateTime.Now,
                            ProductId = model.ArrSaleDetail[i].ProductId,
                            Quantity = model.ArrSaleDetail[i].Quantity,
                            UnitId = model.ArrSaleDetail[i].UnitId,
                            Price = model.ArrSaleDetail[i].Price
                        });
                    }
                }

                model.listSaleDetail = listpurchaseDetail;
                if (_Service.CreateNewSale(model))
                {
                    Session["saleList"] = null;
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

        public ActionResult SaleDetail()
        {
            try
            {
                SaleViewModel model = new SaleViewModel();
                model.listProduct = _ProductService.GetAllProduct();
                model.listCustomer = _customerService.GetAll();
                return View(model);
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return null;
            }
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult SaleDetail(int[] ProductIds, DateTime fromDate, DateTime toDate)
        {
            try
            {
                //var result = _Service.GetSaleDetail(String.Join(",", ProductIds), DateTime.ParseExact(fromDate, "dd/M/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd"),
                //       DateTime.ParseExact(toDate, "dd/M/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd"));
                var result = _Service.GetSaleDetail(String.Join(",", ProductIds), fromDate, toDate);
                return PartialView("_SaleDetailwithTable", result);
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return null;
            }
        }


        public ActionResult GetProductSaleDetailByProductId(int id)
        {
            try
            {
                return View(_Service.GetProductSaleByProductId(id));
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return null;
            }
        }

        public ActionResult POS()
        {
            try
            {
                POSViewModel pos = new POSViewModel();
                pos.Categorylist = CategoryRepository.ddlCategory();
                return View(pos);
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return null;
            }
        }

    }
}