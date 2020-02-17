using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace SuperStore.ViewModel
{
    public class PurchaseViewModel
    {
        public int ProductPurchseId { get; set; }

        [DisplayName("Supplier")]
        public int? SupplierId { get; set; }
        [DisplayName("Supplier")]
        public string SupplierName { get; set; }
        public decimal? TotalPrice { get; set; }
        public decimal? Discount { get; set; }
        public int? PaymentMethod { get; set; }
        public string Method { get; set; }
        public decimal? Paid { get; set; }
        public decimal? Due { get; set; }
        public bool? Active { get; set; }

        public string CreatedUser { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifyBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public List<PurchaseDetailViewModel> listPurchaseDetail { get; set; }
        public List<PurchaseDetailViewModel> ArrPurchaseDetail { get; set; }
        public List<ProductViewModel> listProduct { get; set; }

        public List<SupplierViewModel> listSupplier { get; set; }

        public List<CategoryViewModel> listCategory { get; set; }

        [DisplayName("Category")]
        public int CategoryId { get; set; }

        [DisplayName("Subcategory")]
        public int SubCategoryId { get; set; }

        public List<UnitViewModel> listUnit { get; set; }

        public decimal RetailPrice { get; set; }

        public int Quantity { get; set; }

        public int QuantityAlert { get; set; }

        public string ReceiptNo { get; set; }
    }

    public class PurchaseDetailViewModel
    {
        public int ProductPurchseDetailId { get; set; }
        public int ProductId { get; set; }

        [DisplayName("Product")]
        public string Productname { get; set; }
        public decimal? CostPrice { get; set; }

        public decimal? RetailPrice { get; set; }

        public int? UnitId { get; set; }
        public decimal? Quantity { get; set; }
        public int? TaxId { get; set; }
        public bool? Active { get; set; }

        [DisplayName("Created By")]
        public string CreatedUser { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifyBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public string Impage { get; set; }

        public long Price { get; set; }
    }

    public class PurchasedProducts
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int UnitId { get; set; }

        [DisplayName("Unit")]
        public string UnitName { get; set; }
        public decimal CostPrice { get; set; }
        public decimal RetailPrice { get; set; }
        public decimal Quantity { get; set; }

        public int Tax { get; set; }

        public decimal SubTotal { get; set; }
    }
}