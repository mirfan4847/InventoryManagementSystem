using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SuperStore.Models;
using SuperStore.ViewModel;
using AutoMapper;
using SuperStore.Common;

namespace SuperStore.Common
{
    public static class SessionVariables
    {
        public const string UserId = "UserId";

        public const string Username = "Username";

        public const string Firstname = "Firstname";

        public const string Lastname = "Lastname";

        public const string Image = "Image";

        public const string RoleId = "RoleId";

        public const string Login = "Login";
        public const string hTable = "hTable";
    }

    public static class SessionAppSessting
    {
        public static string GetAppSettingValue(string Name)
        {
            try
            {
                SuperStoreEntities _db = new SuperStoreEntities();
                return _db.tblAppSettings.Where(x => x.Name.Equals(Name, StringComparison.OrdinalIgnoreCase)).FirstOrDefault().Value;
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return null;
            }
        }
    }
}