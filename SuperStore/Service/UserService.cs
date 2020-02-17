using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using SuperStore.ViewModel;
using SuperStore.Repository;
using SuperStore.Common;
using SuperStore.Proc_ViewModel;
using System.Threading.Tasks;

namespace SuperStore.Service
{
    public class UserService
    {
        UsersRepository _userService = new UsersRepository();
        public List<Get_AllUsersViewModel> GetAllUsers()
        {
            return _userService.GetAllUsers();
        }

        public bool AddUser(UsersViewModel model)
        {
            return
                _userService.AddUser(model);
        }

        public UsersViewModel EditUser(int UserId)
        {
            return
                 _userService.EditUser(UserId);
        }
        public bool UpdateUser(UsersViewModel model)
        {
            return
                _userService.UpdateUser(model);
        }

        public async Task<bool> Deactive(UsersViewModel model)
        {
            return
              await _userService.Deactive(model);
        }

        public bool UserExist(UsersViewModel model)
        {
            return
                _userService.UserExist(model);
        }

        public bool ChangePassword(UsersViewModel model)
        {
            return
                _userService.ChangePassword(model);
        }

        public UsersViewModel UserLogin(UsersViewModel model)
        {
            return
                _userService.UserLogin(model);
        }

        public bool UpdateUserLogin(UsersViewModel model)
        {
            return
                _userService.UpdateUserLogin(model);
        }

        public async Task<bool> Regsitration(UsersViewModel model)
        {
            return
              await _userService.Regsitration(model);
        }
    }
}