using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using SuperStore.Common;
using SuperStore.Repository;
using SuperStore.ViewModel;

namespace SuperStore.Service
{
    public class ProductService
    {
        ProductRepository _Repository = new ProductRepository();
        public bool AddProduct(ProductViewModel model)
        {
            try
            {
                return _Repository.AddProduct(model);
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return false;
            }
        }

        public List<ProductViewModel> GetAllProduct()
        {
            try
            {
                return _Repository.GetAllProduct();
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return null;
            }
        }

        public ProductViewModel EditProduct(int id)
        {
            try
            {
                return _Repository.EditProduct(id);
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return null;
            }
        }

        public bool UpdateProduct(ProductViewModel model)
        {
            try
            {
                return _Repository.UpdateProduct(model);
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return false;
            }
        }

        public bool Deactive(ProductViewModel model)
        {
            try
            {
                return
                    _Repository.Deactive(model);
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return false;
            }
        }

        public bool ProductPurchase(ProductViewModel model)
        {
            try
            {
                return
                    _Repository.ProductPurchase(model);
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return false;
            }
        }

    }
}