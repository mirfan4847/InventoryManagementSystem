using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SuperStore.Proc_ViewModel
{
    public class Get_ProductSaleByProductIdViewModel
    {
        public string ProductName { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public string CustomerName { get; set; }

        public string Image { get; set; }

        public string ReceiptNo { get; set; }
    }
}