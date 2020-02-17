using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SuperStore.ViewModel;
using SuperStore.Repository;
using SuperStore.Common;

namespace SuperStore.Service
{
    public class CustomerService
    {
        CustomerRepository _repository = new CustomerRepository();

        public bool Create(CustomerViewModel model)
        {
            try
            {
                return
                    _repository.Create(model);
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return false;
            }
        }


        public List<CustomerViewModel> GetAll()
        {
            try
            {
                return
                    _repository.GetAll();
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return null;
            }
        }


        public CustomerViewModel Edit(int customerId)
        {
            try
            {
                return
                    _repository.Edit(customerId);
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return null;
            }
        }


        public bool Update(CustomerViewModel model)
        {
            try
            {
                return
                    _repository.Update(model);
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return false;
            }
        }


        public bool Deactive(CustomerViewModel model)
        {
            try
            {
                return
                    _repository.Deactive(model);
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return false;
            }
        }

        public List<SaleViewModel> GetSaleByUsers(int CustomerId)
        {
            try
            {
                return
                    _repository.GetSaleByUsers(CustomerId);
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return null;
            }
        }
    }
}