using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using SuperStore.ViewModel;
using SuperStore.Models;
using SuperStore.Common;

namespace SuperStore.Repository
{
    public class TaxRepository
    {

        public static List<TaxViewModel> ddlTax()
        {
            SuperStoreEntities _db = new SuperStoreEntities();
            try
            {
                var query = (from tax in _db.tblTaxes
                             where tax.Active == true
                             select new TaxViewModel()
                             {
                                 TaxId = tax.TaxId,
                                 TaxName = tax.TaxName
                             }).ToList();
                return query;
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return null;
            }
        }
    }
}