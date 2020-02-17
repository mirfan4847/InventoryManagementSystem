using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using SuperStore.Common;
using SuperStore.Repository;
using SuperStore.ViewModel;
using AutoMapper;
using SuperStore.Models;

namespace SuperStore.Service
{
    public class InterfaceService
    {
        InterfaceRepository _Repository = new InterfaceRepository();
        public bool AddInterface(InterfaceViewModel model)
        {
            try
            {
                return _Repository.AddInterface(model);
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return false;
            }
        }

        public List<InterfaceViewModel> GetAll()
        {
            try
            {
                return _Repository.GetAll();
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return null;
            }
        }

        public InterfaceViewModel Edit(int id)
        {
            try
            {
                return
                  Mapper.Map<tblInterface, InterfaceViewModel>
                  (_Repository.Edit(id));
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return null;
            }
        }

        public bool Update(InterfaceViewModel model)
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

        public bool Deactive(InterfaceViewModel model)
        {
            try
            {
                return _Repository.Deactive(model);
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return false;
            }
        }
    }
}