using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SuperStore.Proc_Model;
using SuperStore.Common;
using System.Data.SqlClient;
using SuperStore.Models;
using SuperStore.ViewModel;

namespace SuperStore.Repository
{
    public class StockRepository
    {
        SuperStoreEntities _db = new SuperStoreEntities();
        public List<Get_StockDetail> GetStockDetail(string CategoryIds, string SubCategoryIds, string ProductIds)
        {
            try
            {
                var CategoryIdsParameter = new SqlParameter("@CategoryIds", CategoryIds);
                var SubCategoryIdsParameter = new SqlParameter("@SubcategoryIds", SubCategoryIds);
                var ProductIdsParameter = new SqlParameter("@ProductIds", ProductIds);
                var result = _db.Database.SqlQuery<Get_StockDetail>
                    ("Get_StockDetail @CategoryIds, @SubcategoryIds", CategoryIdsParameter, SubCategoryIdsParameter).ToList();
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