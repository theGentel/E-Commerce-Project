using E_Commerce.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_Commerce.Controllers
{
    public class IndexController : Controller
    {
        // GET: Index
        public ActionResult AddIndex()
        {
            E_CommerceDBEntities obj = new E_CommerceDBEntities();
            ViewBag.Categories = obj.tblCategory.ToList();
            ViewBag.Products = obj.tblProduct.ToList();
            return View();
        }

        public JsonResult AddCart(string ProductName , int UnitPrice , string Imagename , int UserRegisterID)
        {
            using (var context = new E_CommerceDBEntities())
            {
                tblCart obj = new tblCart()
                {
                    UserRegisterID = UserRegisterID,
                    ProductName = ProductName,
                    Image = Imagename,
                    UnitPrice = Convert.ToString(UnitPrice),
                    Quantity = Convert.ToString(1),
                };
                context.tblCart.Add(obj);
                context.SaveChanges();
            }
            return Json(1);
        }

        public JsonResult Addwishlist(string ProductName, int UnitPrice, string ImageName, int status)
        {
            using (var context = new E_CommerceDBEntities())
            {
                tblWishlist objj = new tblWishlist()
                {
                    ProductName = ProductName,
                    Image = ImageName,
                    UnitPrice = Convert.ToString(UnitPrice),
                    Status = Convert.ToString(status),
                };
                context.tblWishlist.Add(objj);
                context.SaveChanges();
            }
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