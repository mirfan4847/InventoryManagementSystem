using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using SuperStore.Repository;
using SuperStore.ViewModel;
using SuperStore.Common;

namespace SuperStore.Service
{
    public class SubCategoryService
    {
        SubCategoryRepository _Repository = new SubCategoryRepository();

        public List<SubCategoryViewModel> GetAll()
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

        public bool Add(SubCategoryViewModel model)
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

        public SubCategoryViewModel Edit(int id)
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

        public bool Update(SubCategoryViewModel model)
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

        public bool Deactive(SubCategoryViewModel model)
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