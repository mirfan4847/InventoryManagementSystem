using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using SuperStore.Repository;
using SuperStore.ViewModel;
using SuperStore.Common;

namespace SuperStore.Service
{
    public class SupplierService
    {
        SupplierRepository _Repository = new SupplierRepository();

        public List<SupplierViewModel> GetAll()
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

        public SupplierViewModel AddSupplier(SupplierViewModel model)
        {
            try
            {
                return
                    _Repository.AddSupplier(model);
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return null;
            }
        }

        public SupplierViewModel Edit(int id)
        {
            try
            {
                return
                    _Repository.Edit(id);
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return null;
            }
        }

        public SupplierViewModel Update(SupplierViewModel model)
        {
            try
            {
                return
                    _Repository.Update(model);
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return null;
            }
        }


        public SupplierViewModel Deactive(SupplierViewModel model)
        {
            try
            {
                return _Repository.Deactive(model);
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return null;
            }
        }
    }
}