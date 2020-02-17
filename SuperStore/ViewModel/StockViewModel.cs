using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SuperStore.ViewModel
{
    public class StockViewModel
    {
        public List<CategoryViewModel> listCategory { get; set; }

        public int? CategoryId { get; set; }
        public int? SubCategoryId { get; set; }
        public int? ProductId { get; set; }

        public string CategoryName { get; set; }

        public string SubCategoryName { get; set; }

        public string ProductName { get; set; }

        public int Quantity { get; set; }
    }
}