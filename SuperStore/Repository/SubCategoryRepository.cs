using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using SuperStore.ViewModel;
using SuperStore.Models;
using SuperStore.Common;

namespace SuperStore.Repository
{
    public class SubCategoryRepository
    {
        SuperStoreEntities _db = new SuperStoreEntities();
        public bool Add(SubCategoryViewModel model)
        {
            try
            {
                var cate = new tblSubCategory()
                {
                    CategoryId = model.CategoryId,
                    SubCategoryName = model.SubCategoryName,
                    Active = model.Active,
                    CreateBy = model.CreateBy,
                    CreatedDate = model.CreatedDate,
                    Description = model.Description
                };

                _db.tblSubCategories.Add(cate);
                int Added = _db.SaveChanges();
                return Added > 0 ? true : false;
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return false;
            }
        }

        public List<SubCategoryViewModel> GetAll()
        {
            try
            {
                var listSub = (from sub in _db.tblSubCategories
                               join cate in _db.tblCategories on sub.CategoryId equals cate.CategoryId
                               join user in _db.tblUsers on sub.CreateBy equals user.UserId
                               where sub.Active == true
                               select new SubCategoryViewModel()
                               {
                                   SubCategoryId = sub.SubCategoryId,
                                   CategoryName = cate.CategoryName,
                                   SubCategoryName = sub.SubCategoryName,
                                   Active = sub.Active,
                                   CreateBy = sub.CreateBy,
                                   CreatedDate = sub.CreatedDate,
                                   ModifyBy = sub.ModifyBy,
                                   ModifyDate = sub.ModifyDate,
                                   CreatedUser = user.Username
                               }).OrderBy(x => x.SubCategoryName).ToList();
                return listSub;
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return null;
            }
        }

        public SubCategoryViewModel Edit(int id)
        {
            try
            {
                return (from sub in _db.tblSubCategories
                        join cate in _db.tblCategories on sub.CategoryId equals cate.CategoryId
                        where sub.SubCategoryId == id && sub.Active == true
                        select new SubCategoryViewModel()
                        {
                            CategoryName = cate.CategoryName,
                            SubCategoryName = sub.SubCategoryName,
                            Active = sub.Active,
                            CreateBy = sub.CreateBy,
                            CreatedDate = sub.CreatedDate,
                            ModifyBy = sub.ModifyBy,
                            ModifyDate = sub.ModifyDate,
                            CategoryId = cate.CategoryId,
                            SubCategoryId = sub.SubCategoryId,
                            Description = sub.Description
                        }).SingleOrDefault();
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return null;
            }
        }

        public bool Update(SubCategoryViewModel model)
        {
            try
            {
                var subcat = _db.tblSubCategories.Where(x => x.SubCategoryId == model.SubCategoryId).SingleOrDefault();
                subcat.SubCategoryName = model.SubCategoryName;
                subcat.CategoryId = model.CategoryId;
                subcat.ModifyBy = model.ModifyBy;
                subcat.ModifyDate = model.ModifyDate;
                subcat.Description = model.Description;
                int added = _db.SaveChanges();
                return added > 0 ? true : false;
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return false;
            }
        }

        public bool Deactive(SubCategoryViewModel model)
        {
            try
            {
                var subcat = _db.tblSubCategories.Where(x => x.SubCategoryId == model.SubCategoryId).SingleOrDefault();
                subcat.Active = model.Active;
                subcat.ModifyBy = model.ModifyBy;
                subcat.ModifyDate = model.ModifyDate;
                int update = _db.SaveChanges();
                return update > 0 ? true : false;
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return false;
            }
        }

        public static List<SubCategoryViewModel> ddlSubCategory(int categoryId)
        {
            try
            {
                SuperStoreEntities _db = new SuperStoreEntities();
                return (from sub in _db.tblSubCategories
                        join cate in _db.tblCategories on sub.CategoryId equals cate.CategoryId
                        where sub.CategoryId == categoryId && sub.Active == true
                        select new SubCategoryViewModel()
                        {
                            SubCategoryName = sub.SubCategoryName,
                            SubCategoryId = sub.SubCategoryId
                        }).ToList();
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return null;
            }
        }
    }
}