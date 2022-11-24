using E_Commerce.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_Commerce.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
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
            if(obj.ProductID > 0)
            {
                if(obj.ImageFile != null)
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
            return Json(1);
        }


        public ActionResult listProduct()
        {
            E_CommerceDBEntities obj = new E_CommerceDBEntities();
            var data = obj.tblProduct.ToList();
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
            string filename = Path.GetFileName(file.FileName);
            string folderpath = Path.Combine("~/Content/assets/ProductImage" , filename);
            file.SaveAs(Server.MapPath(folderpath));
            return "/Content/assets/ProductImage/" + filename;
        }

    }
}