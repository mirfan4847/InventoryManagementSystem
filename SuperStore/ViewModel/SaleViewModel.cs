using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace SuperStore.ViewModel
{
    public class SaleViewModel
    {
        public int SaleId { get; set; }
        public string PaymentType { get; set; }
        public decimal? Discount { get; set; }
        public decimal? NetPrice { get; set; }
        public bool? Active { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public string ReceiptNo { get; set; }
        public decimal? Due { get; set; }
        public decimal? Paid { get; set; }
        public string SoldBy { get; set; }

        public string PaymentMethod { get; set; }

        // Sale Detail  

        public long SubTotal { get; set; }

        public List<ProductViewModel> listProduct { get; set; }

        public List<UnitViewModel> listUnit { get; set; }

        public int unitId { get; set; }

        public string UnitName { get; set; }

        public List<SaleDetailViewModel> ArrSaleDetail { get; set; }

        public List<SaleDetailViewModel> listSaleDetail { get; set; }

        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public decimal? Quantity { get; set; }
        public decimal? Price { get; set; }

        public string Image { get; set; }

        public List<CustomerViewModel> listCustomer { get; set; }

        public int customerId { get; set; }

        public string customerName { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public string SoldUser { get; set; }

    }

    public class SaleDetailViewModel
    {
        public int SaleDetailId { get; set; }
        public int ProductId { get; set; }
        public decimal? Price { get; set; }
        public int? Quantity { get; set; }
        public string Image { get; set; }
        [DisplayName("Product Name")]
        public string ProductName { get; set; }
        public int? ModifyBy { get; set; }
        public DateTime? ModifyDate { get; set; }
        public int? UnitId { get; set; }
        public int? TaxId { get; set; }
        public bool? Active { get; set; }
        public string CreatedUser { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}