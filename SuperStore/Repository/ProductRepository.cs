using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SuperStore.ViewModel;
using SuperStore.Models;
using SuperStore.Common;
using AutoMapper;

namespace SuperStore.Repository
{
    public class ProductRepository
    {
        SuperStoreEntities _db = new SuperStoreEntities();
        public bool AddProduct(ProductViewModel model)
        {
            try
            {
                var product = new tblProduct()
                {
                    Active = model.Active,
                    CategoryId = model.CategoryId,
                    Code = model.Code,
                    CreatedBy = model.CreatedBy,
                    CreatedDate = model.CreatedDate,
                    Description = model.Description,
                    Image = model.Image,
                    ModifyBy = model.ModifyBy,
                    ModifyDate = model.ModifyDate,
                    Name = model.Name,
                    SubCategoryId = model.SubCategoryId
                };
                _db.tblProducts.Add(product);
                int Added = _db.SaveChanges();
                return Added > 0 ? true : false;
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
                var query = (from product in _db.tblProducts
                             join category in _db.tblCategories on product.CategoryId equals category.CategoryId
                             join sub in _db.tblSubCategories on product.SubCategoryId equals sub.SubCategoryId
                             where product.Active == true
                             select new ProductViewModel()
                             {
                                 Code = product.Code,
                                 CreatedBy = product.CreatedBy,
                                 CreatedDate = product.CreatedDate,
                                 Description = product.Description,
                                 Image = product.Image,
                                 Name = product.Name,
                                 ProductId = product.ProductId,
                                 //Unit = productPrice.Unit,
                                 CategoryName = category.CategoryName,
                                 SubCategoryName = sub.SubCategoryName
                             }).ToList();
                return query;
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
                ProductViewModel model = new ProductViewModel();
                var result = _db.tblProducts.Where(x => x.ProductId == id).Single();

                if (result != null)
                {
                    model.CategoryId = result.CategoryId;
                    model.Code = result.Code;
                    model.Description = result.Description;
                    model.Image = result.Image;
                    model.Name = result.Name;
                    model.ProductId = result.ProductId;
                    model.SubCategoryId = result.SubCategoryId;
                    return model;
                }
                return null;
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
                var result = _db.tblProducts.Where(x => x.ProductId == model.ProductId).Single();
                if (result != null)
                {
                    result.CategoryId = model.CategoryId;
                    result.Code = model.Code;
                    result.Description = model.Description;
                    result.Image = model.Image;
                    result.ModifyBy = model.ModifyBy;
                    result.ModifyDate = model.ModifyDate;
                    result.Name = model.Name;
                    result.SubCategoryId = model.SubCategoryId;
                    int updated = _db.SaveChanges();
                    return updated > 0 ? true : false;
                }
                return false;
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return false;
            }
        }

        public static List<ProductViewModel> ddlProductBySubcategory(int subcategoryId)
        {
            try
            {
                SuperStoreEntities _db = new SuperStoreEntities();
                var list = (from p in _db.tblProducts
                            where p.SubCategoryId == subcategoryId && p.Active == true
                            select new ProductViewModel()
                            {
                                ProductId = p.ProductId,
                                Name = p.Name
                            }).ToList();
                return list;
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return null;
            }
        }

        public bool ProductPurchase(ProductViewModel model)
        {
            try
            {
                var productDetail = new tblPurchaseProductDetail()
                {
                    CostPrice = model.CostPrice,
                    ProductId = model.ProductId,
                    RetailPrice = model.RetailPrice,
                    TaxId = model.TaxId,
                    //Quantity = model
                };

                _db.tblPurchaseProductDetails.Add(productDetail);
                int Added = _db.SaveChanges();
                return Added > 0 ? true : false;
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
                int update = 0;
                var result = _db.tblProducts.SingleOrDefault(x => x.ProductId == model.ProductId);
                if (result != null)
                {
                    result.ModifyBy = model.ModifyBy;
                    result.ModifyDate = model.ModifyDate;
                    result.Active = model.Active;
                    update = _db.SaveChanges();
                }
                return update > 0 ? true : false;
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return false;
            }
        }
    }
}