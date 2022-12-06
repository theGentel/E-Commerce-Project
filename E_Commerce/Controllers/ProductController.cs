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
    public class ProductController : Controller
    {
        // GET: Product
        [Route("AddProduct")]
        public ActionResult AddProduct()
        {
            E_CommerceDBEntities obj = new E_CommerceDBEntities();
            ViewBag.Category = obj.tblCategory.ToList();
            ViewBag.SubCategory = obj.tblSubCategory.ToList();
            ViewBag.Color = obj.tblColor.ToList();
            return View();
        }
        [HttpPost]

        public JsonResult Save(ProductModel obj)
        {
            E_CommerceDBEntities objj = new E_CommerceDBEntities();
            if (obj.ProductID > 0)
            {
                if (obj.ImageFile != null)
                {
                    var data = objj.tblProduct.Where(a => a.ProdutID == obj.ProductID).FirstOrDefault();
                    data.CategoryID = obj.CategoryID;
                    data.ProdutID = obj.ProductID;
                    data.ProductName = obj.ProductName;
                    data.ColorID = obj.ColorID;
                    data.Quantity = obj.Quantity;
                    data.Price = obj.Price;
                    data.Status = obj.Status;
                    data.Image = UploadImage(obj.ImageFile);
                    objj.SaveChanges();
                }
                else
                {
                    var data = objj.tblProduct.Where(a => a.ProdutID == obj.ProductID).FirstOrDefault();
                    data.CategoryID = obj.CategoryID;
                    data.ProdutID = obj.ProductID;
                    data.ProductName = obj.ProductName;
                    data.ColorID = obj.ColorID;
                    data.Quantity = obj.Quantity;
                    data.Price = obj.Price;
                    data.Status = obj.Status;
                    data.Image = obj.Image;
                    objj.SaveChanges();
                }
            }
            else
            {
                var table = new SqlParameter("@MyTable", "tblProduct");
                var coloumn = new SqlParameter("@ColoumnName", "ProductName");
                var compare = new SqlParameter("@ValueToCompare", obj.ProductName);
                var result = objj.Database.SqlQuery<ColorModel>("Sp_IsExist @MyTable,@ColoumnName,@ValueToCompare", table, coloumn, compare).ToList();
                // var find = result.Count();
                if (result.Count() > 0)
                {
                    return Json(2);
                }
                else
                {
                    using (var context = new E_CommerceDBEntities())
                    {
                        tblProduct C = new tblProduct()
                        {
                            CategoryID = obj.CategoryID,
                            SubCategoryID = obj.SubCategoryID,
                            ProductName = obj.ProductName,
                            ColorID = obj.ColorID,
                            Quantity = obj.Quantity,
                            Status = obj.Status,
                            Price = obj.Price,
                            Image = UploadImage(obj.ImageFile),
                        };
                        context.tblProduct.Add(C);
                        context.SaveChanges();
                    }
                }
            }
            return Json(1);
        }


        //public ActionResult listProduct()
        //{
        //    E_CommerceDBEntities obj = new E_CommerceDBEntities();
        //    var data = obj.tblProduct.ToList();
        //    return PartialView("_PartialViewProduct", data);
        //}
        public ActionResult listProduct()
        {
            E_CommerceDBEntities dbcontext = new E_CommerceDBEntities();
            var data = (from P in dbcontext.tblProduct
                        join C in dbcontext.tblCategory on P.CategoryID equals C.CategoryID
                        join SC in dbcontext.tblSubCategory on P.SubCategoryID equals SC.SubCategoryID
                        join CO in dbcontext.tblColor on P.ColorID equals CO.ColorID
                        join S in dbcontext.tblStatus on P.Status equals S.StatusID
                        select new ProductModel
                        {
                            ProductID=P.ProdutID,
                            CategoryName=C.CategoryName,
                            SubCategoryName=SC.SubCategoryName,
                            ProductName=P.ProductName,
                            ColorName=CO.ColorName,
                            Quantity=P.Quantity,
                            Price=P.Price,
                            StatusName=S.StatusName,
                            Image=P.Image,

                        }).ToList();
            return PartialView("_PartialViewProduct", data);
        }

        [HttpGet]

        public ActionResult Edit(int ProductID)
        {
            E_CommerceDBEntities obj = new E_CommerceDBEntities();
            var data = obj.tblProduct.Where(a => a.ProdutID == ProductID).FirstOrDefault();
            return Json(data, JsonRequestBehavior.AllowGet);
        }


        public ActionResult DeleteProduct(int id, string ImageName)
        {
            E_CommerceDBEntities obj = new E_CommerceDBEntities();
            string Filename = Path.GetFileName(ImageName);
            var verify = Path.Combine(Server.MapPath("~/Content/assets/ProductImage"), Filename);
            FileInfo file = new FileInfo(verify);
            if (file.Exists)
            {
                file.Delete();
            }
            var Data = obj.tblProduct.Where(a => a.ProdutID == id).First();
            obj.tblProduct.Remove(Data);
            obj.SaveChanges();
            return Json(1);
        }


        [HttpGet]
        public ActionResult SubCategories(int CategoryId)
        {
            E_CommerceDBEntities obj = new E_CommerceDBEntities();
            var data = obj.tblSubCategory.Where(a => a.CategoryID == CategoryId).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public string UploadImage(HttpPostedFileBase file)
        {
            if (file != null)
            {
                string filename = Path.GetFileName(file.FileName);
                string folderpath = Path.Combine("~/Content/assets/ProductImage", filename);
                file.SaveAs(Server.MapPath(folderpath));
                return "/Content/assets/ProductImage/" + filename;

            }
            else
            {
                return null;
            }
        }

    }
}