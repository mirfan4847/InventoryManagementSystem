using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace SuperStore.Proc_ViewModel
{
    public class Get_StockDetailViewModel
    {
        public string Category { get; set; }

        [DisplayName("Product")]
        public string ProductName { get; set; }

        public string SubCategory { get; set; }

        public int Quantity { get; set; }
    }
}