using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using SuperStore.Common;
using SuperStore.Repository;
using SuperStore.ViewModel;
using SuperStore.Proc_ViewModel;

namespace SuperStore.Service
{
    public class SaleService
    {
        SaleRepository _Repository = new SaleRepository();
        public bool CreateNewSale(SaleViewModel model)
        {
            try
            {
                return
                    _Repository.CreateNewSale(model);
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
                return
                    _Repository.GetSale();
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
                return
                    _Repository.GetSaleDetailBySaleId(saleId);
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return null;
            }
        }

        public List<Get_SaleDetailByProductIdAndDateViewModel> GetSaleDetail(string productId, DateTime fromDate, DateTime toDate)
        {
            return
                _Repository.GetSaleDetail(productId, fromDate, toDate);
        }

        public List<Get_ProductSaleByProductIdViewModel> GetProductSaleByProductId(int productId)
        {
            return
                _Repository.GetProductSaleByProductId(productId);
        }
    }
}