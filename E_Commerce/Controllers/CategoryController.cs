using E_Commerce.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_Commerce.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {

        [Authorize(Roles = "SuperAdmin,Admin")]
        [Route("AddCategory")]
        public ActionResult AddCategory()
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
                //var table = new SqlParameter("@MyTable", "tblCategory");
                //var coloumn = new SqlParameter("@ColoumnName", "CategoryName");
                //var compare = new SqlParameter("@ValueToCompare", obj.CategoryName);
                //var result = objj.Database.SqlQuery<CategoryModel>("Sp_IsExist @MyTable,@ColoumnName,@ValueToCompare", table, coloumn,compare).ToList();
                //// var find = result.Count();

                bool IsAvailable = objj.tblCategory.Any(x => x.CategoryName == obj.CategoryName);

                
                if(IsAvailable)
                {
                    return Json(2);
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
               
            }
            return Json(1);
        }

        //public ActionResult listCategory()
        //{
        //    E_CommerceDBEntities obj = new E_CommerceDBEntities();
        //    var data = obj.tblCategory.ToList();
        //    return PartialView("_PartialViewCategory", data);
        //}
        public ActionResult listCategory()
        {
            E_CommerceDBEntities dbcontext = new E_CommerceDBEntities();
            var data = (from C in dbcontext.tblCategory
                        join S in dbcontext.tblStatus on C.Status equals S.StatusID
                        select new CategoryModel
                        {
                            CategoryID = C.CategoryID,
                            CategoryName = C.CategoryName,
                            StatusName = S.StatusName,
                            Image = C.Image,
                        }).ToList();

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

            //var table = new SqlParameter("@MyTable", "tblSubCategory");
            //var coloumn = new SqlParameter("@ColoumnName", "CategoryID");
            //var compare = new SqlParameter("@ValueToCompare", id);
            //var result = obj.Database.SqlQuery<CategoryModel>("Sp_IsExist @MyTable,@ColoumnName,@ValueToCompare", table, coloumn, compare).ToList();


            bool IsAvailable = obj.tblSubCategory.Any(x => x.CategoryID == id);

            if (!IsAvailable)
            {
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
            else
            {
                return Json(2);
            } 
        }


        public string UploadImage(HttpPostedFileBase file)
        {
            if (file !=null)
            {
                string filename = Path.GetFileName(file.FileName);
                string folderpath = Path.Combine("~/Content/assets/CategoriesImage", filename);
                file.SaveAs(Server.MapPath(folderpath));
                return "/Content/assets/CategoriesImage/" + filename;
            }
            else
            {
                return null;
            }
        }
    }
}