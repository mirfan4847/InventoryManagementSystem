using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SuperStore.Repository;
using SuperStore.ViewModel;
using SuperStore.Common;

namespace SuperStore.Service
{
    public class CategoryService
    {
        CategoryRepository _Repository = new CategoryRepository();

        public List<CategoryViewModel> AllCategory()
        {
            try
            {
                return
                    _Repository.AllCategory();
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return null;
            }
        }

        public bool AddCategory(CategoryViewModel model)
        {
            try
            {
                return
                    _Repository.Add(model);
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return false;
            }
        }

        public CategoryViewModel Edit(int CategoryId)
        {
            try
            {
                return
                    _Repository.Edit(CategoryId);
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
                return
                    _Repository.Update(model);
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
                return
                    _Repository.Deactive(model);
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return false;
            }
        }
    }
}