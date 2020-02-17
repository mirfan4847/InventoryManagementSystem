using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using SuperStore.ViewModel;
using SuperStore.Models;
using SuperStore.Common;
using AutoMapper;
using System.Transactions;
using SuperStore.Proc_Model;
using SuperStore.Proc_ViewModel;
using System.Web.Mvc;
using System.Threading.Tasks;

namespace SuperStore.Repository
{
    public class UsersRepository
    {
        SuperStoreEntities _db = new SuperStoreEntities();
        public bool AddUser(UsersViewModel model)
        {
            try
            {
                int added = 0;
                bool UserExis = _db.tblUsers.Any(x => x.Username.Equals(model.Username, StringComparison.OrdinalIgnoreCase));
                if (!UserExis)
                {
                    using (var scope = new TransactionScope())
                    {
                        var user = new tblUser()
                        {
                            Firstname = model.Firstname,
                            Lastname = model.Lastname,
                            Username = model.Username,
                            Password = model.Password,
                            Active = model.Active,
                            CreatedDate = model.CreatedDate,
                            RoleId = model.RoleId,
                            Archived = model.Archived,
                            CreatedBy = model.CreatedBy,
                            Image = model.Image,
                            Salt = model.Salt,
                            UpdatedBy = model.UpdatedBy,
                            UpdatedDate = model.UpdatedDate
                        };
                        _db.tblUsers.Add(user);

                        var detail = new tblUserDetail()
                        {
                            Address = model.Address,
                            City = model.City,
                            CNIC = model.CNIC,
                            Country = model.Country,
                            CreatedBy = model.CreatedBy,
                            CreatedDate = model.CreatedDate,
                            Email = model.Email,
                            Gender = model.Gender,
                            Mobile = model.Mobile,
                            Phone = model.Phone,
                            State = model.State,
                            UpdatedBy = model.UpdatedBy,
                            UpdatedDate = model.UpdatedDate
                        };

                        _db.tblUserDetails.Add(detail);
                        added = _db.SaveChanges();
                        scope.Complete();
                    }
                }
                return added > 0 ? true : false;
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return false;
            }
        }

        public List<Get_AllUsersViewModel> GetAllUsers()
        {
            try
            {
                var query = Mapper.Map<List<Get_AllUsers>, List<Get_AllUsersViewModel>>
                        (_db.Database.SqlQuery<Get_AllUsers>("Get_AllUsers").ToList());
                return query;
            }
            catch (Exception ex)
            {

                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return null;
            }
        }

        public UsersViewModel EditUser(int UserId)
        {
            try
            {
                var query =
                    (from u in _db.tblUsers
                     join ud in _db.tblUserDetails on u.UserId equals ud.UserDetailId
                     join r in _db.tblRoles on u.RoleId equals r.RoleId into ru
                     from rv in ru.DefaultIfEmpty()
                     where u.UserId == UserId
                     select new UsersViewModel()
                     {
                         Address = ud.Address,                        
                         Email = ud.Email,
                         Firstname = u.Firstname,
                         Image = u.Image,
                         Lastname = u.Lastname,
                         Phone = ud.Phone,
                         Username = u.Username,
                         UserId = u.UserId,
                         RoleName = rv == null ? "External User" : rv.Name,
                         RoleId = u.RoleId
                     }).SingleOrDefault();
                return query;
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return null;
            }
        }

        public bool UpdateUser(UsersViewModel model)
        {
            try
            {
                var user = _db.tblUsers.Where(x => x.UserId == model.UserId).SingleOrDefault();
                if (user != null)
                {
                    user.Firstname = string.IsNullOrEmpty(model.Firstname) ? model.Firstname : user.Firstname;
                    user.Image = string.IsNullOrEmpty(model.Image) ? model.Image : user.Image;
                    user.Lastname = string.IsNullOrEmpty(model.Lastname) ? model.Lastname : user.Lastname;
                    user.RoleId = model.RoleId != 0 ? model.RoleId : user.RoleId;
                    user.Username = string.IsNullOrEmpty(model.Username) ? model.Username : user.Username;
                    _db.SaveChanges();
                    var userdetail = _db.tblUserDetails.Where(x => x.UserDetailId == model.UserId).SingleOrDefault();
                    if (userdetail != null)
                    {
                        userdetail.CNIC = string.IsNullOrEmpty(model.CNIC) ? model.CNIC : userdetail.CNIC;
                        //userdetail.City = model.City != 0 ? model.City : userdetail.City;
                        userdetail.Address = string.IsNullOrEmpty(model.Address) ? model.Address : userdetail.Address;
                        //userdetail.Country = string.IsNullOrEmpty(model.Country) ? model.Country : userdetail.Country;
                        userdetail.Email = string.IsNullOrEmpty(model.Email) ? model.Email : userdetail.Email;
                        userdetail.Gender = string.IsNullOrEmpty(model.Gender) ? model.Gender : userdetail.Gender;
                        userdetail.Mobile = string.IsNullOrEmpty(model.Mobile) ? model.Mobile : userdetail.Mobile;
                        userdetail.Phone = string.IsNullOrEmpty(model.Phone) ? model.Phone : userdetail.Phone;
                        // userdetail.State = string.IsNullOrEmpty(model.State) ? model.State : userdetail.State;
                        _db.SaveChanges();
                    }
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return false;
            }
        }


        public bool UserExist(UsersViewModel model)
        {
            try
            {
                return _db.tblUsers.Any(x => x.Username.Equals(model.Username, StringComparison.OrdinalIgnoreCase) && x.Password == model.Password);
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return false;
            }
        }

        public bool ChangePassword(UsersViewModel model)
        {
            try
            {
                int update = 0;
                var userData = _db.tblUsers.Where(x => x.UserId == model.UserId).SingleOrDefault();
                if (userData != null)
                {
                    userData.Password = model.Password;
                    userData.UpdatedDate = model.UpdatedDate;
                    userData.UpdatedBy = model.UpdatedBy;
                    update = _db.SaveChanges();
                }
                return update > 0 ? true : false;
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return false;
            }
        }

        public UsersViewModel UserLogin(UsersViewModel model)
        {
            try
            {
                return Mapper.Map<tblUser, UsersViewModel>
                    (_db.tblUsers.Where(x => x.Username == model.Username && x.Password == model.Password).FirstOrDefault());
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return null;
            }
        }

        public bool UpdateUserLogin(UsersViewModel model)
        {
            try
            {
                var user = _db.tblUsers.Where(x => x.UserId == model.UserId).FirstOrDefault();
                if (user != null)
                {
                    user.Login = model.Login;
                    if (model.TotalLogin != null)
                    {
                        user.UpdatedBy = model.TotalLogin;
                        user.UpdatedBy = model.UpdatedBy;
                        user.UpdatedDate = model.UpdatedDate;
                    }
                    return
                        _db.SaveChanges() > 1 ? true : false;
                }
                return false;
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return false;
            }
        }


        public async Task<bool> Regsitration(UsersViewModel model)
        {
            try
            {
                var user = new tblUser()
                {
                    Active = true,
                    Archived = false,
                    CreatedBy = model.CreatedBy,
                    CreatedDate = model.CreatedDate,
                    Firstname = model.Firstname,
                    Lastname = model.Lastname,
                    Password = model.Password,
                    UpdatedBy = model.UpdatedBy,
                    UpdatedDate = model.UpdatedDate,
                    Username = model.Username
                };
                _db.tblUsers.Add(user);

                var userdetail = new tblUserDetail()
                {
                    Active = true,
                    Address = model.Address,
                    Archived = false,
                    City = model.City,
                    CNIC = model.CNIC,
                    Country = model.Country,
                    CreatedBy = model.CreatedBy,
                    CreatedDate = model.CreatedDate,
                    Email = model.Email,
                    EmailCode = model.EmailCode,
                    EmialConfirmed = model.EmialConfirmed,
                    Gender = model.Gender,
                    Mobile = model.Mobile,
                    Phone = model.Phone,
                    State = model.State,
                    UpdatedBy = model.UpdatedBy,
                    UpdatedDate = model.UpdatedDate
                };
                _db.tblUserDetails.Add(userdetail);

                int Added = await _db.SaveChangesAsync();
                return Added > 0 ? true : false;
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return false;
            }
        }

        public async Task<bool> Deactive(UsersViewModel model)
        {
            try
            {
                int update = 0;
                var user = _db.tblUsers.SingleOrDefault(x => x.UserId == model.UserId);
                if (user != null)
                {
                    user.Active = model.Active;
                    user.UpdatedBy = model.UpdatedBy;
                    user.UpdatedDate = model.UpdatedDate;
                    update = await _db.SaveChangesAsync();

                }
                return update > 0 ? true : false;
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return false;
            }
        }
    }
}