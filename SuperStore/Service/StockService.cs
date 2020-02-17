using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SuperStore.Common;
using SuperStore.Proc_Model;
using SuperStore.Proc_ViewModel;
using SuperStore.Repository;
using AutoMapper;

namespace SuperStore.Service
{
    public class StockService
    {
        StockRepository _repository = new StockRepository();
        public List<Get_StockDetailViewModel> GetStockDetail(string CategoryIds, string SubCategoryIds, string ProductIds)
        {
            try
            {
                return
                    Mapper.Map<List<Get_StockDetail>, List<Get_StockDetailViewModel>>
                    (_repository.GetStockDetail(CategoryIds, SubCategoryIds, ProductIds));
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return null;
            }
        }
    }
}