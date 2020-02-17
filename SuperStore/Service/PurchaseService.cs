using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using SuperStore.Repository;
using SuperStore.ViewModel;
using SuperStore.Common;

namespace SuperStore.Service
{
    public class PurchaseService
    {
        PurchaseRepository _Repository = new PurchaseRepository();
        public bool PurchaseProduct(PurchaseViewModel model)
        {
            try
            {
                return
                    _Repository.PurchaseProduct(model);
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
                return
                    _Repository.GetAll();
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
                return
                    _Repository.Edit(Id);
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
                return
                    _Repository.Update(model);
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return false;
            }
        }

        public List<PurchaseDetailViewModel> GetProductPurchasedList(int purchaseDetailId)
        {
            try
            {
                return
                    _Repository.GetProductPurchasedList(purchaseDetailId);
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return null;
            }
        }
    }
}