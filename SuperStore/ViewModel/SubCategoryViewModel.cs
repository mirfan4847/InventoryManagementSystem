using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel;

namespace SuperStore.ViewModel
{
    public class SubCategoryViewModel
    {
        public int SubCategoryId { get; set; }

        [DisplayName("Category")]
        public int? CategoryId { get; set; }
        public string SubCategoryName { get; set; }
        public bool? Active { get; set; }
        public int? CreateBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifyBy { get; set; }
        public DateTime? ModifyDate { get; set; }

        public List<CategoryViewModel> listCategory { get; set; }

        public string CategoryName { get; set; }

        public string CreatedUser { get; set; }

        public string Description { get; set; }
    }
}