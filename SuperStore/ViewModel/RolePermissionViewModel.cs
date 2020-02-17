using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SuperStore.ViewModel
{
    public class RolePermissionViewModel
    {
        public int RolePermissionId { get; set; }

        public int RoleId { get; set; }

        public int? InterfaceId { get; set; }

        public bool? Active { get; set; }

        public bool? Archived { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public List<InterfaceViewModel> Interfaces { get; set; }

        public List<InterfaceViewModel> SelectedInterfaces { get; set; }

        public List<RoleViewModel> Roles { get; set; }

        public int?[] InterfaceArr { get; set; }
    }
}