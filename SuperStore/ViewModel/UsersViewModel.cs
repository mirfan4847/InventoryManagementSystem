using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SuperStore.ViewModel
{
    public class UsersViewModel
    {
        public int UserId { get; set; }

        [DisplayName("First Name")]
        public string Firstname { get; set; }

        [DisplayName("Last Name")]
        public string Lastname { get; set; }

        [DisplayName("User Name")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password required")]
        public string Password { get; set; }

        [DisplayName("Confirm Password")]
        public string ConfirmPassword { get; set; }
        public string Salt { get; set; }


        public string Image { get; set; }
        public int? RoleId { get; set; }
        public bool? Login { get; set; }

        public HttpPostedFileBase pic { get; set; }
        // User Detail

        public int UserDetailId { get; set; }
        public string CNIC { get; set; }

        [Required(ErrorMessage = "Username or email required")]
        public string Email { get; set; }
        public string Gender { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
        public int? Country { get; set; }
        public int? State { get; set; }
        public int? City { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public int? TotalLogin { get; set; }
        public DateTime? ChangePasswordDate { get; set; }
        public bool? EmialConfirmed { get; set; }
        public string EmailCode { get; set; }
        public bool? Active { get; set; }
        public bool? Archived { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public bool IsProfileEdit { get; set; }

        public string OldPassword { get; set; }

        public string RoleName { get; set; }
    }
}