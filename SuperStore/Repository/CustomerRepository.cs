using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SuperStore.ViewModel;
using SuperStore.Common;
using SuperStore.Models;
using AutoMapper;

namespace SuperStore.Repository
{
    public class CustomerRepository
    {
        SuperStoreEntities _db = new SuperStoreEntities();
        public bool Create(CustomerViewModel model)
        {
            try
            {
                var customer = new tblCustomer()
                {
                    Active = model.Active,
                    Address = model.Address,
                    CreatedBy = model.CreatedBy,
                    CreatedDate = model.CreatedDate,
                    Email = model.Email,
                    Image = model.Image,
                    Name = model.Name,
                    PhoneNo = model.PhoneNo
                };
                _db.tblCustomers.Add(customer);
                int Added = _db.SaveChanges();
                return Added > 0 ? true : false;
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
                    Mapper.Map<List<tblCustomer>, List<CustomerViewModel>>
                    (_db.tblCustomers.ToList());
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
                    Mapper.Map<tblCustomer, CustomerViewModel>
                    (_db.tblCustomers.SingleOrDefault(x => x.CustomerId == customerId));
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
                var customerdata = _db.tblCustomers.SingleOrDefault(c => c.CustomerId == model.CustomerId);
                customerdata.Email = model.Email;
                customerdata.Image = model.Image;
                customerdata.Name = model.Name;
                customerdata.PhoneNo = model.PhoneNo;
                customerdata.UpdatedBy = model.UpdatedBy;
                customerdata.UpdatedDate = model.UpdatedDate;
                int updated = _db.SaveChanges();
                return updated > 0 ? true : false;
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
                var customerdata = _db.tblCustomers.SingleOrDefault(c => c.CustomerId == model.CustomerId);
                customerdata.Active = false;
                customerdata.UpdatedBy = model.UpdatedBy;
                customerdata.UpdatedDate = model.UpdatedDate;
                int updated = _db.SaveChanges();
                return updated > 0 ? true : false;
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return false;
            }
        }

        public static List<CustomerViewModel> ddlCustomer()
        {
            try
            {
                SuperStoreEntities _db = new SuperStoreEntities();
                return Mapper.Map<List<tblCustomer>, List<CustomerViewModel>>
                      (_db.tblCustomers.ToList());
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return null;
            }
        }

        public List<SaleViewModel> GetSaleByUsers(int CustomerId)
        {
            try
            {
                var result = (from c in _db.tblCustomers
                              join s in _db.tblSales on c.CustomerId equals s.CustomerId
                              join u in _db.tblUsers on s.CreatedBy equals u.UserId
                              where c.CustomerId == CustomerId
                              select new SaleViewModel()
                              {
                                  customerName = c.Name,
                                  customerId = c.CustomerId,
                                  Discount = s.Discount,
                                  Due = s.Due,
                                  NetPrice = s.NetPrice,
                                  Paid = s.Paid,
                                  PaymentType = s.PaymentType,
                                  SoldUser = u.Username,
                                  CreatedDate = s.CreatedDate
                              }).ToList();
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