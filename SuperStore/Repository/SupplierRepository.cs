using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using SuperStore.Models;
using SuperStore.ViewModel;
using SuperStore.Common;

namespace SuperStore.Repository
{
    public class SupplierRepository
    {
        SuperStoreEntities _db = new SuperStoreEntities();
        public SupplierViewModel AddSupplier(SupplierViewModel model)
        {
            try
            {
                var supplier = new tblSupplier()
                {
                    Active = model.Active,
                    Address = model.Address,
                    CreatedBy = model.CreatedBy,
                    CreatedDate = model.CreatedDate,
                    Email = model.Email,
                    ModifyBy = model.ModifyBy,
                    ModifiedDate = model.ModifiedDate,
                    Name = model.Name,
                    PhoneNo = model.PhoneNo
                };
                _db.tblSuppliers.Add(supplier);
                int add = _db.SaveChanges();
                model.Status = add > 0 ? true : false;
                return model;
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return null;
            }
        }

        public List<SupplierViewModel> GetAll()
        {
            try
            {
                var supplier = (from sup in _db.tblSuppliers
                                where sup.Active == true
                                select new SupplierViewModel()
                                {
                                    Name = sup.Name,
                                    Email = sup.Email,
                                    PhoneNo = sup.PhoneNo,
                                    SupplierId = sup.SupplierId,
                                    Address = sup.Address
                                }).OrderBy(x => x.Name).ToList();
                return supplier;
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
                var supplier = (from sup in _db.tblSuppliers
                                where sup.Active == true && sup.SupplierId == id
                                select new SupplierViewModel()
                                {
                                    Name = sup.Name,
                                    Email = sup.Email,
                                    PhoneNo = sup.PhoneNo,
                                    SupplierId = sup.SupplierId,
                                    Address = sup.Address,
                                    CreatedBy = sup.CreatedBy,
                                    CreatedDate = sup.CreatedDate
                                }).FirstOrDefault();
                return supplier;
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
                int updatd = 0;
                var sup = _db.tblSuppliers.Where(x => x.SupplierId == model.SupplierId).FirstOrDefault();
                if (sup != null)
                {
                    sup.Name = model.Name;
                    sup.Address = model.Address;
                    sup.Email = model.Email;
                    sup.PhoneNo = model.PhoneNo;

                    updatd = _db.SaveChanges();
                }
                model.Status = updatd > 0 ? true : false;
                return model;
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return null;
            }
        }

        public static List<SupplierViewModel> GetSupplier()
        {
            try
            {
                SuperStoreEntities _db = new SuperStoreEntities();
                var supplier = (from sup in _db.tblSuppliers
                                where sup.Active == true
                                select new SupplierViewModel()
                                {
                                    Name = sup.Name,
                                    Email = sup.Email,
                                    PhoneNo = sup.PhoneNo,
                                    SupplierId = sup.SupplierId
                                }).OrderBy(x => x.Name).ToList();
                return supplier;
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
                int update = 0;
                var supplier = _db.tblSuppliers.SingleOrDefault(x => x.SupplierId == model.SupplierId);
                if (supplier != null)
                {
                    supplier.Active = model.Active;
                    supplier.ModifyBy = model.ModifyBy;
                    supplier.ModifiedDate = model.ModifiedDate;
                    update = _db.SaveChanges();
                }
                model.Status = update > 0 ? true : false;
                return model;
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return null;
            }
        }
    }
}