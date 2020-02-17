using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SuperStore.ViewModel
{
    public class InterfaceViewModel
    {
        public int InterfaceId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool? Active { get; set; }
        public bool? Archived { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}