using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SuperStore.Common;
using SuperStore.Models;
using SuperStore.ViewModel;
using System.Data.SqlClient;
using System.Threading.Tasks;
using SuperStore.Proc_ViewModel;

namespace SuperStore.Repository
{
    public class SaleRepository
    {

        SuperStoreEntities _db = new SuperStoreEntities();
        public bool CreateNewSale(SaleViewModel model)
        {
            int Added = 0;
            try
            {
                var sale = new tblSale()
                {
                    Active = model.Active,
                    CreatedBy = model.CreatedBy,
                    CreatedDate = model.CreatedDate,
                    Discount = model.Discount,
                    NetPrice = model.NetPrice,
                    PaymentType = model.PaymentType,
                    ReceiptNo = model.ReceiptNo,
                    Due = model.Due,
                    Paid = model.Paid,
                    CustomerId = model.customerId
                };
                _db.tblSales.Add(sale);
                _db.SaveChanges();

                foreach (var item in model.listSaleDetail)
                {
                    var saledetail = new tblSaleDetail()
                    {
                        Active = model.Active,
                        CreatedBy = model.CreatedBy,
                        CreatedDate = model.CreatedDate,
                        Price = item.Price,
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        SalesId = sale.SaleId,
                        UnitId = item.UnitId
                    };
                    _db.tblSaleDetails.Add(saledetail);
                    Added = _db.SaveChanges();

                    var saleData = _db.tblStocks.SingleOrDefault(x => x.ProductId == item.ProductId);
                    if (saleData != null)
                    {
                        saleData.Quantity -= item.Quantity;
                        saleData.UpdatedBy = item.ModifyBy;
                        saleData.UpdatedDate = item.ModifyDate;
                        _db.SaveChanges();
                    }
                }

                return Added > 0 ? true : false;
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return false;
            }
        }

        public List<SaleViewModel> GetSale()
        {
            try
            {
                var result = (from sale in _db.tblSales
                              join user in _db.tblUsers on sale.CreatedBy equals user.UserId
                              join customer in _db.tblCustomers on sale.CustomerId equals customer.CustomerId
                              where sale.Active == true
                              select new SaleViewModel()
                              {
                                  ReceiptNo = sale.ReceiptNo,
                                  NetPrice = sale.NetPrice,
                                  Discount = sale.Discount,
                                  PaymentType = sale.PaymentType,
                                  Paid = sale.Paid,
                                  Due = sale.Due,
                                  SoldBy = user.Username,
                                  CreatedDate = sale.CreatedDate,
                                  SaleId = sale.SaleId,
                                  customerName = customer.Name
                              }).ToList();
                return result;
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return null;
            }
        }

        public List<SaleDetailViewModel> GetSaleDetailBySaleId(int saleId)
        {
            try
            {
                var queryResult = (from SaleDetail in _db.tblSaleDetails
                                   join usr in _db.tblUsers on SaleDetail.CreatedBy equals usr.UserId
                                   join product in _db.tblProducts on SaleDetail.ProductId equals product.ProductId
                                   where SaleDetail.SalesId == saleId && SaleDetail.Active == true
                                   select new SaleDetailViewModel()
                                   {
                                       Image = product.Image,
                                       ProductName = product.Name,
                                       Price = SaleDetail.Price,
                                       Quantity = SaleDetail.Quantity,
                                       CreatedUser = usr.Username
                                   }).ToList();

                return queryResult;
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return null;
            }
        }


        public List<Get_SaleDetailByProductIdAndDateViewModel> GetSaleDetail(string productId, DateTime fromDate, DateTime toDate)
        {
            try
            {
                var productParameter = new SqlParameter("@ProductId", productId);
                var fromDateParameter = new SqlParameter("@FromDate", fromDate);
                var toDateParameter = new SqlParameter("@ToDate", toDate);
                var result = _db.Database.SqlQuery<Get_SaleDetailByProductIdAndDateViewModel>
                    ("Get_SaleDetailByProductIdAndDate @ProductId, @FromDate, @ToDate",
                    productParameter, fromDateParameter, toDateParameter).ToList();
                return result;
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return null;
            }
        }

        public List<Get_ProductSaleByProductIdViewModel> GetProductSaleByProductId(int productId)
        {
            try
            {
                var productParameter = new SqlParameter("@ProductId", productId);
                var result = _db.Database.SqlQuery<Get_ProductSaleByProductIdViewModel>
                    ("Get_ProductSaleByProductId @ProductId",
                    productParameter).ToList();
                return result;
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return null;
            }
        }

    }
}