using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SuperStore.ViewModel
{
    public class SupplierViewModel : BaseViewModel
    {
        public int SupplierId { get; set; }
        public string Name { get; set; }
        public string PhoneNo { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public bool? Active { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifyBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}