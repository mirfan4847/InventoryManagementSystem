using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using SuperStore.ViewModel;
using SuperStore.Models;
using AutoMapper;
using SuperStore.Common;
using System.Transactions;

namespace SuperStore.Repository
{
    public class PurchaseRepository
    {
        SuperStoreEntities _db = new SuperStoreEntities();
        public bool PurchaseProduct(PurchaseViewModel model)
        {
            try
            {
                using (var scope = new TransactionScope())
                {

                    var purchase = new tblProductPurchase()
                    {
                        Active = model.Active,
                        CreatedBy = model.CreatedBy,
                        CreatedDate = model.CreatedDate,
                        Discount = Convert.ToInt32(model.Discount),
                        Due = model.Due,
                        Method = model.Method,
                        NetTotal = model.TotalPrice,
                        Paid = model.Paid,
                        ReceiptNo = model.ReceiptNo,
                        SupplierId = model.SupplierId.Value,
                    };

                    _db.tblProductPurchases.Add(purchase);
                    _db.SaveChanges();

                    List<tblPurchaseProductDetail> listdetail = new List<tblPurchaseProductDetail>();
                    for (int i = 0; i < model.listPurchaseDetail.Count; i++)
                    {
                        listdetail.Add(new tblPurchaseProductDetail()
                        {
                            Active = model.listPurchaseDetail[i].Active,
                            CostPrice = model.listPurchaseDetail[i].CostPrice,
                            CreatedBy = model.listPurchaseDetail[i].CreatedBy,
                            CreatedDate = model.listPurchaseDetail[i].CreatedDate,
                            ProductId = model.listPurchaseDetail[i].ProductId,
                            ProductPurchaseId = purchase.ProductPurchaseId,
                            Quantity = model.listPurchaseDetail[i].Quantity,
                            RetailPrice = model.listPurchaseDetail[i].RetailPrice

                        });
                    }

                    _db.tblPurchaseProductDetails.AddRange(listdetail);
                    _db.SaveChanges();

                    tblStock objstock = new tblStock();
                    foreach (var item in listdetail)
                    {
                        var stocktdetail = _db.tblStocks.FirstOrDefault(x => x.ProductId == item.ProductId);
                        if (stocktdetail == null)
                        {

                            objstock.Active = item.Active;
                            objstock.CreatedBy = item.CreatedBy;
                            objstock.CreatedDate = item.CreatedDate;
                            objstock.ProductId = item.ProductId;
                            objstock.Quantity = Convert.ToInt32(item.Quantity.Value);
                            objstock.QuantityAlert = model.QuantityAlert;
                            objstock.SalePrice = item.RetailPrice;
                            _db.tblStocks.Add(objstock);
                            _db.SaveChanges();
                        }
                        else
                        {
                            stocktdetail.Quantity += Convert.ToInt32(item.Quantity.Value);
                            stocktdetail.SalePrice = item.RetailPrice;
                            stocktdetail.UpdatedBy = item.UpdatedBy;
                            stocktdetail.UpdatedDate = item.UpdatedDate;
                            _db.SaveChanges();
                        }

                    }

                    scope.Complete();
                }
                return true;
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return false;
            }
        }

        public List<PurchaseViewModel> GetAll()
        {
            try
            {
                var query = (from purchased in _db.tblProductPurchases
                             join user in _db.tblUsers on purchased.CreatedBy equals user.UserId
                             select new PurchaseViewModel()
                             {
                                 Due = purchased.Due,
                                 Paid = purchased.Paid,
                                 Method = purchased.Method,
                                 TotalPrice = purchased.NetTotal,
                                 CreatedUser = user.Username,
                                 CreatedDate = purchased.CreatedDate,
                                 Discount = purchased.Discount,
                                 ProductPurchseId = purchased.ProductPurchaseId,
                                 ReceiptNo = purchased.ReceiptNo
                             }).ToList();
                return query;
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return null;
            }
        }

        public ProductViewModel Edit(int Id)
        {
            try
            {
                return (from product in _db.tblProducts
                        join purchased in _db.tblPurchaseProductDetails on product.ProductId equals purchased.ProductId
                        where product.ProductId == Id
                        select new ProductViewModel()
                        {
                            Name = product.Name,
                            Code = product.Code,
                            CostPrice = purchased.CostPrice,
                            Image = product.Image,
                            RetailPrice = purchased.RetailPrice,
                        }).SingleOrDefault();
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return null;
            }
        }

        public bool Update(ProductViewModel model)
        {
            try
            {
                var purchase = _db.tblPurchaseProductDetails.Where(x => x.PurchaseProductDetailId == model.PurchaseProductId).SingleOrDefault();
                if (purchase != null)
                {
                    purchase.CostPrice = model.CostPrice != null ? model.CostPrice : purchase.CostPrice;
                    purchase.RetailPrice = model.RetailPrice != null ? model.RetailPrice : purchase.RetailPrice;
                    purchase.TaxId = model.TaxId != 0 ? model.TaxId : purchase.TaxId;
                    //purchase.Unit = model.Unit != 0 ? model.Unit : purchase.Unit;
                    int Update = _db.SaveChanges();
                    return Update > 0 ? true : false;
                }
                return false;
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return false;
            }
        }

        public List<PurchaseDetailViewModel> GetProductPurchasedList(int purchaseId)
        {
            try
            {
                var query = (from purchased in _db.tblPurchaseProductDetails
                             join product in _db.tblProducts on purchased.ProductId equals product.ProductId
                             join user in _db.tblUsers on purchased.CreatedBy equals user.UserId
                             where purchased.ProductPurchaseId == purchaseId
                             select new PurchaseDetailViewModel()
                             {
                                 Productname = product.Name,
                                 CostPrice = purchased.CostPrice,
                                 Quantity = purchased.Quantity.Value,
                                 TaxId = purchased.TaxId,
                                 CreatedUser = user.Username,
                                 CreatedDate = purchased.CreatedDate,
                                 Impage = product.Image,
                                 RetailPrice = purchased.RetailPrice
                             }).ToList();
                return query;
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return null;
            }
        }
    }
}