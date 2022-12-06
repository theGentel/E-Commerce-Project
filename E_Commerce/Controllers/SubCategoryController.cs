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
    public class SubCategoryController : Controller
    {
        // GET: SubCategory
        [Route("AddSubCategory")]
        public ActionResult AddSubCategory()
        {
            E_CommerceDBEntities obj = new E_CommerceDBEntities();
            ViewBag.Category = obj.tblCategory.ToList();
            return View();
        }
        [HttpPost]

        public JsonResult Save(SubCategoryModel obj)
        {
            E_CommerceDBEntities objj = new E_CommerceDBEntities();
            if (obj.SubCategoryID > 0)
            {
                if (obj.ImageFile != null)
                {
                    var data = objj.tblSubCategory.Where(a => a.SubCategoryID == obj.SubCategoryID).FirstOrDefault();
                    data.SubCategoryName = obj.SubCategoryName;
                    data.Status = obj.Status;
                    data.Image = UploadImage(obj.ImageFile);
                    objj.SaveChanges();
                }
                else
                {
                    var data = objj.tblSubCategory.Where(a => a.SubCategoryID == obj.SubCategoryID).FirstOrDefault();
                    data.SubCategoryName = obj.SubCategoryName;
                    data.Status = obj.Status;
                    data.Image = obj.Image;
                    objj.SaveChanges();
                }
            }
            else
            {
                //var table = new SqlParameter("@MyTable", "tblSubCategory");
                //var coloumn = new SqlParameter("@ColoumnName", "SubCategoryName");
                //var compare = new SqlParameter("@ValueToCompare", obj.SubCategoryName);
                //var result = objj.Database.SqlQuery<SubCategoryModel>("Sp_IsExist @MyTable,@ColoumnName,@ValueToCompare", table, coloumn, compare).ToList();

                // var find = result.Count();

                bool IsAvailable = objj.tblCategory.Any(x => x.CategoryName == obj.CategoryName);

                if (IsAvailable)
                {
                    return Json(2);
                }
                else
                {
                    using (var context = new E_CommerceDBEntities())
                    {
                        tblSubCategory C = new tblSubCategory()
                        {
                            CategoryID = obj.CategoryID,
                            SubCategoryName = obj.SubCategoryName,
                            Status = obj.Status,
                            Image = UploadImage(obj.ImageFile),
                        };
                        context.tblSubCategory.Add(C);
                        context.SaveChanges();
                    }
                }
            }
            return Json(1);
        }

        //public ActionResult listSubCategory()
        //{
        //    E_CommerceDBEntities obj = new E_CommerceDBEntities();
        //    var data = obj.tblSubCategory.ToList();
        //    return PartialView("_PartialViewSubCategory", data);
        //}


        public ActionResult listSubCategory()
        {
            E_CommerceDBEntities dbContext = new E_CommerceDBEntities();
            var data = (from SC in dbContext.tblSubCategory
                        join C in dbContext.tblCategory on SC.CategoryID equals C.CategoryID
                        join S in dbContext.tblStatus on SC.Status equals S.StatusID
                        select new SubCategoryModel
                        {
                            CategoryName = C.CategoryName,
                            SubCategoryID = SC.SubCategoryID,
                            SubCategoryName = SC.SubCategoryName,
                            StatusName = S.StatusName,
                            Image = SC.Image,
                        }).ToList();

            return PartialView("_PartialViewSubCategory", data);
        }

        [HttpGet]

        public ActionResult Edit(int SubCategoryId)
        {
            E_CommerceDBEntities objj = new E_CommerceDBEntities();
            var data = objj.tblSubCategory.Where(a => a.SubCategoryID == SubCategoryId).FirstOrDefault();
            return Json(data, JsonRequestBehavior.AllowGet);
        }


        public ActionResult DeleteSubCategory(int id, string ImageName)
        {
            E_CommerceDBEntities obj = new E_CommerceDBEntities();

            //var table = new SqlParameter("@MyTable", "tblProduct");
            //var coloumn = new SqlParameter("@ColoumnName", "SubCategoryID");
            //var compare = new SqlParameter("@ValueToCompare", id);
            //var result = obj.Database.SqlQuery<SubCategoryModel>("Sp_IsExist @MyTable,@ColoumnName,@ValueToCompare", table, coloumn, compare).ToList();

            bool IsAvailabel = obj.tblProduct.Any(x => x.SubCategoryID == id);
            
            if (!IsAvailabel)
            {
                string Filename = Path.GetFileName(ImageName);
                var verify = Path.Combine(Server.MapPath("~/Content/assets/SubCategoriesImage"), Filename);
                FileInfo file = new FileInfo(verify);
                if (file.Exists)
                {
                    file.Delete();
                }
                var Data = obj.tblSubCategory.Where(a => a.SubCategoryID == id).First();
                obj.tblSubCategory.Remove(Data);
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
            if (file != null)
            {
                string filename = Path.GetFileName(file.FileName);
                string folderPath = Path.Combine("~/Content/assets/SubCategoriesImage", filename);
                file.SaveAs(Server.MapPath(folderPath));
                return "/Content/assets/SubCategoriesImage/" + filename;

            }
            else
            {
                return null;
            }
        }
    }
}