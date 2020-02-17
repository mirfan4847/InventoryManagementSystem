using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SuperStore.Proc_ViewModel
{
    public class Get_AllUsersViewModel
    {
        public int UserId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Username { get; set; }
        public string Image { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string Phone { get; set; }
        public int? Country { get; set; }
        public int? State { get; set; }
        public int? City { get; set; }

        public string DisplayName
        {
            get { return Firstname + " " + Lastname; }
        }

        public string Address
        {
            get { return Country + " " + State + " " + City; }
        }
    }
}