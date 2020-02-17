using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel;

namespace SuperStore.ViewModel
{
    public class ProductViewModel
    {
        public int ProductId { get; set; }
        public int PurchaseProductId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }

        [DisplayName("Category")]
        public int? CategoryId { get; set; }

        [DisplayName("Sub Category")]
        public int? SubCategoryId { get; set; }

        [DisplayName("Cost Price")]
        public decimal? CostPrice { get; set; }

        [DisplayName("Retail Price")]
        public decimal? RetailPrice { get; set; }

        [DisplayName("Tax")]
        public int? TaxId { get; set; }
        public int? Unit { get; set; }
        public HttpPostedFileBase file { get; set; }

        public string Image { get; set; }
        public bool? Active { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifyBy { get; set; }
        public DateTime? ModifyDate { get; set; }

        public string CategoryName { get; set; }

        public string SubCategoryName { get; set; }

        public List<CategoryViewModel> listCategory { get; set; }

        public List<SubCategoryViewModel> listSubCategory { get; set; }

        public List<TaxViewModel> listTax { get; set; }

        public string Tax { get; set; }

        public decimal SalePrice { get; set; }

        public List<SupplierViewModel> listSupplier { get; set; }

        public int supplierId { get; set; }

        [DisplayName("Supplier")]
        public string SuppierName { get; set; }          

        public List<ProductViewModel> listProduct { get; set; }

    }
}