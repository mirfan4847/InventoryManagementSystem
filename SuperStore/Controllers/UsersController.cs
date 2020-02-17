using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using SuperStore.Repository;
using SuperStore.ViewModel;
using System.Web.Security;
using SuperStore.Common;
using System.Collections;
using SuperStore.Service;
using System.Threading.Tasks;
using SuperStore.Models;
using CrystalDecisions.CrystalReports.Engine;
using System.IO;

namespace SuperStore.Controllers
{

    public class UsersController : Controller
    {
        UserService _userService = new UserService();

        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
        [Authorize]
        public ActionResult GetAllUser()
        {
            try
            {
                return View(_userService.GetAllUsers());
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return null;
            }
        }


        public ActionResult ExportReports()
        {
            SuperStoreEntities _db = new SuperStoreEntities();
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Reports"), "Test.rpt"));
            rd.SetDataSource(_db.tests.ToList());
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            try
            {
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                return File(stream, "application/pdf", "Test.pdf");
            }
            catch (Exception)
            {

                throw;
            }

        }


        [Authorize]
        public ActionResult AddUser(int id = 0, bool infoEdit = false)
        {
            try
            {
                UsersViewModel model = new UsersViewModel();
                RoleRepository role = new RoleRepository();
                ViewBag.listRole = role.GetAllRole();
                if (id != 0)
                {
                    model = _userService.EditUser(id);
                }
                else
                {
                    model.UserId = id;
                }
                model.IsProfileEdit = infoEdit;
                return View(model);
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return null;
            }
        }

        [HttpPost]
        [Authorize]
        public ActionResult AddUser(UsersViewModel model)
        {
            try
            {

                if (model.pic != null)
                {
                    //string pic = System.IO.Path.GetFileName(model.pic.FileName);
                    string extenion = System.IO.Path.GetExtension(model.pic.FileName);

                    if (!(extenion == ".svg" || extenion == ".html" || extenion == ".gif"
                        || extenion == ".png" || extenion == ".zip" || extenion == ".rar"
                        || extenion == ".jpg" || extenion == ".jpeg" || extenion == ".doc"
                        || extenion == ".docx" || extenion == ".pdf" || extenion == ".txt"
                        || extenion == ".xls" || extenion == ".xlsx" || extenion == ".ppt"
                        || extenion == ".pptx" || extenion == ".psd" || extenion == ".mp4"
                        || extenion == ".mp3" || extenion == ".flv" || extenion == ".xml"))
                    {
                        return View();
                        // return Json("|.File Is Not Allowed", JsonRequestBehavior.AllowGet);
                    }

                    string picName = System.IO.Path.GetFileName(System.Guid.NewGuid().ToString() + extenion);
                    string path = System.IO.Path.Combine(
                                             Server.MapPath("~/images"), picName);
                    // file is uploaded
                    model.pic.SaveAs(path);


                    //// in-case if you want to store byte[000000000000000000000000000000000] ie. for DB
                    //using (MemoryStream ms = new MemoryStream())
                    //{
                    //    model.pic.InputStream.CopyTo(ms);
                    //    byte[] array = ms.GetBuffer();
                    //}

                    model.Image = picName;
                }
                if (model.UserId != 0)
                {
                    model.UpdatedDate = DateTime.Now;
                    _userService.UpdateUser(model);
                    return View();
                }
                else {
                    model.CreatedDate = DateTime.Now;
                    model.Active = true;
                    _userService.AddUser(model);
                    return View("Index");
                }

            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return null;
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login(string access = "", string rtnurl = "")
        {
            try
            {
                if (!string.IsNullOrEmpty(Convert.ToString(Session[SessionVariables.Username])))
                    return RedirectToAction("Index", "Home");
                else
                    return View();
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return null;
            }
        }

        [HttpPost]
        public ActionResult Login(UsersViewModel model)
        {
            try
            {
                FormsAuthentication.SignOut();
                FormsAuthentication.SetAuthCookie(model.Username, true);
                UsersViewModel userdata = _userService.UserLogin(model);
                if (userdata != null)
                {
                    Session[SessionVariables.Firstname] = userdata.Firstname;
                    Session[SessionVariables.Image] = userdata.Image;
                    Session[SessionVariables.Lastname] = userdata.Lastname;
                    Session[SessionVariables.Login] = true;
                    Session[SessionVariables.RoleId] = userdata.Firstname;
                    Session[SessionVariables.UserId] = userdata.UserId;
                    Session[SessionVariables.Username] = userdata.Username;
                    var interfaces = InterfaceRepository.GetInterfaceByRoleId(userdata.RoleId);
                    Hashtable htable = new Hashtable();
                    foreach (var item in interfaces)
                    {
                        htable.Add(item.InterfaceId, item.Name);
                    }
                    Session[SessionVariables.hTable] = htable;
                    model.Login = true;
                    model.TotalLogin = userdata.TotalLogin + 1;
                    model.UpdatedBy = userdata.UserId;
                    model.UpdatedDate = DateTime.Now;
                    _userService.UpdateUserLogin(model);

                    return RedirectToAction("Index", "Home");
                }
                else
                    return View();
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return null;
            }
        }

        [AllowAnonymous]
        public ActionResult SignOut()
        {
            try
            {
                FormsAuthentication.SignOut();
                UsersViewModel model = new UsersViewModel();
                model.Login = false;
                model.TotalLogin = null;
                _userService.UpdateUserLogin(model);
                Session[SessionVariables.Username] = null;
                return RedirectToAction("Login");
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return null;
            }
        }

        [AllowAnonymous]
        public ActionResult Registration()
        {
            try
            {
                RoleRepository role = new RoleRepository();
                ViewBag.listRole = role.GetAllRole();
                return View();
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return null;
            }
        }

        [HttpPost]
        public async Task<ActionResult> Registration(UsersViewModel model)
        {
            try
            {
                return Json(await _userService.Regsitration(model), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return null;
            }
        }

        [HttpPost]
        public async Task<ActionResult> Deactive(int id)
        {
            try
            {
                UsersViewModel model = new UsersViewModel();
                model.Active = false;
                model.UpdatedBy = Convert.ToInt32(Session[SessionVariables.UserId]);
                model.UpdatedDate = DateTime.Now;
                model.UserId = id;
                await _userService.Deactive(model);
                return View("Index");
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return null;
            }
        }

        public ActionResult UserProfile(int id)
        {
            try
            {
                return View(_userService.EditUser(id));
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return null;
            }
        }

        public ActionResult ChangePassword(int UserId)
        {
            try
            {
                UsersViewModel model = new UsersViewModel();
                model.UserId = UserId;
                return View(model);
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return null;
            }
        }

        [HttpPost]
        public ActionResult ChangePassword(UsersViewModel model)
        {
            try
            {
                model.UpdatedBy = Convert.ToInt32(Session[SessionVariables.UserId]);
                model.UpdatedDate = DateTime.Now;
                _userService.ChangePassword(model);
                return View();
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return null;
            }
        }

    }
}