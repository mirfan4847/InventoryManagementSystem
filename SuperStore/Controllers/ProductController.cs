using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using SuperStore.Common;
using SuperStore.Repository;
using SuperStore.ViewModel;
using System.IO;
using System.Collections;
using SuperStore.Service;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using SuperStore.Models;

namespace SuperStore.Controllers
{
    [SessionExpire]
    [Authorize]
    public class ProductController : Controller
    {
        ProductService _Service = new ProductService();
        CategoryRepository _Category = new CategoryRepository();
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AllProduct()
        {
            try
            {
                return View(_Service.GetAllProduct());
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return null;
            }
        }

        [HttpGet]
        public ActionResult AddProduct()
        {
            try
            {
                ProductViewModel model = new ProductViewModel();
                List<CategoryViewModel> listCategory = new List<CategoryViewModel>();
                listCategory.Add(new CategoryViewModel() { CategoryId = 0, CategoryName = "---Select Category---" });
                listCategory.AddRange(CategoryRepository.ddlCategory());
                model.listCategory = listCategory;
                model.listTax = TaxRepository.ddlTax();
                model.listSupplier = SupplierRepository.GetSupplier();
                return View(model);
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return null;
            }
        }

        [HttpPost]
        public JsonResult AddProduct(ProductViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string fileName = "";
                    HttpPostedFileBase file = null;
                    if (Request.Files.Count > 0)
                    {
                        file = Request.Files[0];

                        if (file != null && file.ContentLength > 0)
                        {
                            fileName = Path.GetFileName(file.FileName);
                            model.Image = fileName;
                        }
                    }

                    model.CreatedBy = Convert.ToInt32(Session[SessionVariables.UserId]);
                    model.CreatedDate = DateTime.Now;
                    model.Active = true;
                    if (_Service.AddProduct(model))
                    {
                        if (file != null)
                        {
                            string path = Path.Combine(Server.MapPath("~/Images/"), fileName);
                            file.SaveAs(path);
                        }
                        return Json(true, JsonRequestBehavior.AllowGet);
                    }

                }
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return null;
            }
        }

        public JsonResult GetSubCategory(int CategoryId)
        {
            try
            {
                return Json(SubCategoryRepository.ddlSubCategory(CategoryId), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return null;
            }
        }


        public ActionResult Edit(int id)
        {
            try
            {
                ProductViewModel model = new ProductViewModel();
                model = _Service.EditProduct(id);
                model.listCategory = CategoryRepository.ddlCategory();
                model.listSubCategory = SubCategoryRepository.ddlSubCategory(model.CategoryId.Value);
                model.listTax = TaxRepository.ddlTax();
                return View(model);
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return null;
            }
        }

        [HttpPost]
        public ActionResult Edit(ProductViewModel model)
        {
            try
            {
                model.ModifyBy = Convert.ToInt32(Session[SessionVariables.UserId]);
                model.ModifyDate = DateTime.Now;
                return Json(_Service.UpdateProduct(model), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return null;
            }
        }

        public ActionResult Deactive(int id)
        {
            try
            {
                ProductViewModel model = new ProductViewModel();
                model.ProductId = id;
                model.ModifyBy = Convert.ToInt32(Session[SessionVariables.UserId]);
                model.ModifyDate = DateTime.Now;
                model.Active = false;
                _Service.Deactive(model);
                return View("Index");
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return null;
            }
        }


        [HttpPost]
        public ActionResult CreateNewAttachment2(IEnumerable<HttpPostedFileBase> attchments)
        {
            try
            {
                var uploadStatusFile = new Hashtable();
                for (var i = 0; i < Request.Files.Count; i++)
                {
                    var file = Request.Files[i];

                    if (file.ContentLength > 0)
                    {

                        var fileSavePath = Path.Combine(Server.MapPath(System.Web.Configuration.WebConfigurationManager.AppSettings["UploadDocumentPath"]), file.FileName);
                        var relativePath = fileSavePath.Replace(Server.MapPath("~/"), System.Web.Configuration.WebConfigurationManager.AppSettings["UploadDocumentPath"]).Replace(@"\", "/");
                        var extension = Path.GetExtension(fileSavePath).ToLower();
                        var filename = Path.GetFileName(fileSavePath);

                        if (((file.ContentLength / 1024f) / 1024f) > 6)
                        {
                            uploadStatusFile[filename] = ".File Should Not More than 6MB";
                            //return Json("|.File Should Not More than 6MB", JsonRequestBehavior.AllowGet);
                        }

                        if (!(extension == ".svg" || extension == ".html" || extension == ".gif" || extension == ".png" || extension == ".zip" || extension == ".rar" || extension == ".jpg" || extension == ".jpeg" || extension == ".doc" || extension == ".docx" || extension == ".pdf" || extension == ".txt" || extension == ".xls" || extension == ".xlsx" || extension == ".ppt" || extension == ".pptx" || extension == ".psd" || extension == ".mp4" || extension == ".mp3" || extension == ".flv" || extension == ".xml"))
                        {
                            uploadStatusFile[filename] = ".File Is Not Allowed";
                            // return Json("|.File Is Not Allowed", JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            if (System.IO.File.Exists(fileSavePath))
                            {
                                var di = new DirectoryInfo(Server.MapPath(System.Web.Configuration.WebConfigurationManager.AppSettings["UploadDocumentPath"]));
                                var files = di.GetFiles(filename);

                                var onlyfilename = filename.Substring(0, filename.LastIndexOf('.'));
                                var count = Directory.EnumerateFiles(@Server.MapPath(System.Web.Configuration.WebConfigurationManager.AppSettings["UploadDocumentPath"]), onlyfilename + "*", SearchOption.AllDirectories).Count();

                                onlyfilename = onlyfilename + "_" + count + extension;
                                fileSavePath = Path.Combine(Server.MapPath(System.Web.Configuration.WebConfigurationManager.AppSettings["UploadDocumentPath"]), onlyfilename);
                                relativePath = fileSavePath.Replace(Server.MapPath("~/"), System.Web.Configuration.WebConfigurationManager.AppSettings["UploadDocumentPath"] + "/").Replace(@"\", "/");
                            }
                            file.SaveAs(fileSavePath);
                            if (Request.Url != null)
                            {
                                var weburl = Request.Url.OriginalString.Substring(0, Request.Url.OriginalString.IndexOf(Request.Url.PathAndQuery, StringComparison.Ordinal));
                                relativePath = weburl + relativePath.Substring(1, (relativePath.Length - 1));
                            }
                            uploadStatusFile[Path.GetFileNameWithoutExtension(fileSavePath)] = Path.GetFileName(fileSavePath);
                        }

                        //return Json(relativePath, JsonRequestBehavior.AllowGet);
                    }
                }

                return Json(uploadStatusFile, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return null;
            }
        }

        public ActionResult LoadProductBySubcategory(int SubcategoryId)
        {
            try
            {
                var product = ProductRepository.ddlProductBySubcategory(SubcategoryId);
                return Json(product, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ExceptionManager.WriteExceptionMessageToFile(ex.Message, ex);
                return null;
            }
        }


        public ActionResult UploadBulkProduct()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UploadMultipleFiles78()
        {
            //FileUploadService service = new FileUploadService();

            var postedFile = Request.Files[0];

            StreamReader sr = new StreamReader(postedFile.InputStream);
            StringBuilder sb = new StringBuilder();
            DataTable dt = new DataTable();
            DataRow dr;
            string s;
            int j = 0;

            while (!sr.EndOfStream)
            {
                while ((s = sr.ReadLine()) != null)
                {
                    //Ignore first row as it consists of headers
                    if (j > 0)
                    {
                        string[] str = s.Split(',');

                        dr = dt.NewRow();
                        dr["Postcode"] = str[0].ToString();
                        dr["Latitude"] = str[2].ToString();
                        dr["Longitude"] = str[3].ToString();
                        dr["County"] = str[7].ToString();
                        dr["District"] = str[8].ToString();
                        dr["Ward"] = str[9].ToString();
                        dr["CountryRegion"] = str[12].ToString();
                        dt.Rows.Add(dr);
                    }
                    j++;
                }
            }
            //service.SaveFilesDetails(dt);
            sr.Close();
            return View("Index");
        }


        [HttpPost]
        public ActionResult UploadMultipleFiles(HttpPostedFileBase postedFile)
        {
            SuperStoreEntities _db = new SuperStoreEntities();

            string filePath = string.Empty;
            if (postedFile != null)
            {
                string path = Server.MapPath("~/Uploads/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                filePath = path + Path.GetFileName(postedFile.FileName);
                string extension = Path.GetExtension(postedFile.FileName);
                postedFile.SaveAs(filePath);

                //Create a DataTable.
                DataTable dt = new DataTable();
                dt.Columns.AddRange(new DataColumn[3] { new DataColumn("Id", typeof(int)),
                                new DataColumn("Name", typeof(string)),
                                new DataColumn("Country",typeof(string)) });


                //Read the contents of CSV file.
                string csvData = System.IO.File.ReadAllText(filePath);

                //Execute a loop over the rows.
                foreach (string row in csvData.Split('\n'))
                {
                    if (!string.IsNullOrEmpty(row))
                    {
                        dt.Rows.Add();
                        int i = 0;

                        //Execute a loop over the columns.
                        foreach (string cell in row.Split(','))
                        {
                            dt.Rows[dt.Rows.Count - 1][i] = cell;
                            i++;
                        }
                    }
                }
            }
            return View();
        }

    }
}