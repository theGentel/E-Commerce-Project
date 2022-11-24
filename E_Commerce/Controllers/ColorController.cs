using E_Commerce.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_Commerce.Controllers
{
    public class ColorController : Controller
    {
        // GET: Color
        public ActionResult AddColor()
        {
            E_CommerceDBEntities obj = new E_CommerceDBEntities();
            ViewBag.Color = obj.tblColor.ToList();
            return View();
        }
        [HttpPost]
        public JsonResult Save(ColorModel obj)
        {
            E_CommerceDBEntities objj = new E_CommerceDBEntities();
            if(obj.ColorID > 0)
            {
                if(obj.ImageFile != null)
                {
                    var data = objj.tblColor.Where(a => a.ColorID == obj.ColorID).FirstOrDefault();
                    data.ColorName = obj.ColorName;
                    data.Status = obj.Status;
                    data.Image = UploadImage(obj.ImageFile);
                    objj.SaveChanges();
                }
                else
                {
                    var data = objj.tblColor.Where(a => a.ColorID == obj.ColorID).FirstOrDefault();
                    data.ColorName = obj.ColorName;
                    data.Status = obj.Status;
                    data.Image = obj.Image;
                    objj.SaveChanges();
                }
            }
            else
            {
                using (var context = new E_CommerceDBEntities())
                {
                    tblColor C = new tblColor()
                    {
                        ColorName = obj.ColorName,
                        Status = obj.Status,
                        Image = UploadImage(obj.ImageFile),
                    };
                    context.tblColor.Add(C);
                    context.SaveChanges();
                }
            }
            return Json(1);
        }


        public ActionResult listColor()
        {
            E_CommerceDBEntities obj = new E_CommerceDBEntities();
            var data = obj.tblColor.ToList();
            return PartialView("_PartialViewColor", data);
        }

        [HttpGet]
        public ActionResult Edit(int ColorID)
        {
            E_CommerceDBEntities obj = new E_CommerceDBEntities();
            var data = obj.tblColor.Where(a => a.ColorID == ColorID).FirstOrDefault();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteColor(int id, string ImageName)
        {
            E_CommerceDBEntities obj = new E_CommerceDBEntities();
            string Filename = Path.GetFileName(ImageName);
            var verify = Path.Combine(Server.MapPath("~/Content/assets/ColorImage"), Filename);
            FileInfo file = new FileInfo(verify);
            if (file.Exists)
            {
                file.Delete();
            }
            var Data = obj.tblColor.Where(a => a.ColorID == id).First();
            obj.tblColor.Remove(Data);
            obj.SaveChanges();
            return Json(1);
        }


        public string UploadImage(HttpPostedFileBase file)
        {
            string filename = Path.GetFileName(file.FileName);
            string folderPath = Path.Combine("~/Content/assets/ColorImage" , filename);
            file.SaveAs(Server.MapPath(folderPath));
            return "/Content/assets/ColorImage/" + filename;
        }
    }
}