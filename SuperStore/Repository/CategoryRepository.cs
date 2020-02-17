using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using SuperStore.ViewModel;
using SuperStore.Models;
using AutoMapper;
using System.Web.Mvc;
using SuperStore.Common;
using AutoMapper;


namespace SuperStore.Repository
{
    public class CategoryRepository
    {

        SuperStoreEntities _db = new SuperStoreEntities();
        public bool Add(CategoryViewModel model)
        {
            try
            {
                var category = new tblCategory()
                {
                    CategoryName = model.CategoryName,
                    Active = true,
                    CreatedBy = model.CreatedBy,
                    CreatedDate = model.CreatedDate,
                    Description = model.Description
                };
                _db.tblCategories.Add(category);
                int added = _db.SaveChanges();
                return added > 0 ? true : false;
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return false;
            }
        }

        public List<CategoryViewModel> AllCategory()
        {
            try
            {
                var result = Mapper.Map<List<CategoryViewModel>>
                //(_db.tblCategories.Where(x => x.Active == true).ToList());
                (from category in _db.tblCategories
                 join user in _db.tblUsers on category.CreatedBy equals user.UserId
                 where category.Active == true
                 select new CategoryViewModel()
                 {
                     CreatedName = user.Username,
                     CategoryId = category.CategoryId,
                     CategoryName = category.CategoryName,
                     CreatedDate = category.CreatedDate
                 }).ToList();
                return result;
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return null;
            }
        }

        public CategoryViewModel Edit(int categoryId)
        {
            try
            {
                var result = Mapper.Map<CategoryViewModel>(_db.tblCategories.Where(x => x.CategoryId == categoryId).SingleOrDefault());
                return result;
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return null;
            }
        }

        public string Update(CategoryViewModel model)
        {
            try
            {
                var category = _db.tblCategories.Where(x => x.CategoryId == model.CategoryId && x.Active == true).SingleOrDefault();
                if (!(category == null))
                {
                    category.CategoryName = model.CategoryName;
                    category.ModifyBy = model.ModifyBy;
                    category.ModifyDate = model.ModifyDate;
                    int update = _db.SaveChanges();
                    return update > 0 ? "Updated" : "Not updated";
                }
                return "Not updated";
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return null;
            }
        }

        public bool Deactive(CategoryViewModel model)
        {
            try
            {
                var category = _db.tblCategories.Where(x => x.CategoryId == model.CategoryId).Single();
                if (!(category == null))
                {
                    category.Active = model.Active;
                    category.ModifyBy = model.ModifyBy;
                    category.ModifyDate = model.ModifyDate;
                    int deactive = _db.SaveChanges();
                    return deactive > 0 ? true : false;
                };
                return false;
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return false;
            }
        }

        public static List<CategoryViewModel> ddlCategory()
        {
            try
            {
                SuperStoreEntities _db = new SuperStoreEntities();

                var result = Mapper.Map<List<tblCategory>, List<CategoryViewModel>>
                    (_db.tblCategories.Where(x => x.Active == true).OrderBy(x => x.CategoryName).ToList());
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