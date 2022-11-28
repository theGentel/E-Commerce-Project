using E_Commerce.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_Commerce.Controllers
{
    public class CategoryController : Controller
    {
        public ActionResult AddCategory()
        {
            return View();
        }

        public ActionResult AdCategory()
        {
            return View();
        }
        [HttpPost]
        public JsonResult Save(CategoryModel obj)
        {
            E_CommerceDBEntities objj = new E_CommerceDBEntities();
            if (obj.CategoryID > 0)
            {
                if (obj.ImageFile != null)
                {
                    var data = objj.tblCategory.Where(a => a.CategoryID == obj.CategoryID).FirstOrDefault();
                    data.CategoryName = obj.CategoryName;
                    data.Status = obj.Status;
                    data.Image = UploadImage(obj.ImageFile);
                    objj.SaveChanges();
                }
                else
                {
                    var data = objj.tblCategory.Where(a => a.CategoryID == obj.CategoryID).FirstOrDefault();
                    data.CategoryName = obj.CategoryName;
                    data.Status = obj.Status;
                    data.Image = obj.Image;
                    objj.SaveChanges();
                }
            }
            else
            {
                using (var context = new E_CommerceDBEntities())
                {
                    tblCategory C = new tblCategory()
                    {
                        CategoryName = obj.CategoryName,
                        Image = UploadImage(obj.ImageFile),
                        Status = obj.Status,
                    };
                    context.tblCategory.Add(C);
                    context.SaveChanges();
                }
            }
            return Json(1);
        }


        public ActionResult listCategory()
        {
            E_CommerceDBEntities obj = new E_CommerceDBEntities();
            var data = obj.tblCategory.ToList();
            return PartialView("_PartialViewCategory", data);
        }


        [HttpGet]
        public ActionResult Edit(int CategoryId)
        {
            E_CommerceDBEntities obj = new E_CommerceDBEntities();
            var data = obj.tblCategory.Where(a => a.CategoryID == CategoryId).FirstOrDefault();
            return Json(data, JsonRequestBehavior.AllowGet);
        }


        public ActionResult DeleteCategory(int id, string ImageName)
        {
            E_CommerceDBEntities obj = new E_CommerceDBEntities();
            string Filename = Path.GetFileName(ImageName);
            var verify = Path.Combine(Server.MapPath("~/Content/assets/CategoriesImage"), Filename);
            FileInfo file = new FileInfo(verify);
            if (file.Exists)
            {
                file.Delete();
            }
            var Data = obj.tblCategory.Where(a => a.CategoryID == id).First();
            obj.tblCategory.Remove(Data);
            obj.SaveChanges();
            return Json(1);
        }


        public string UploadImage(HttpPostedFileBase file)
        {
            string filename = Path.GetFileName(file.FileName);
            string folderpath = Path.Combine("~/Content/assets/CategoriesImage", filename);
            file.SaveAs(Server.MapPath(folderpath));
            return "/Content/assets/CategoriesImage/" + filename;
        }
    }
}