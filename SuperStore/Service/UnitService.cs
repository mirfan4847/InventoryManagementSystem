using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SuperStore.ViewModel;
using SuperStore.Repository;
using SuperStore.Common;

namespace SuperStore.Service
{
    public class UnitService
    {
        UnitRepository _repository = new UnitRepository();

        public bool Create(UnitViewModel model)
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

        public UnitViewModel Edit(int id)
        {
            try
            {
                return
                    _repository.Edit(id);
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return null;
            }
        }

        public bool Update(UnitViewModel model)
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

        public bool Deactive(UnitViewModel model)
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

        public List<UnitViewModel> GetAll()
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
    }
}