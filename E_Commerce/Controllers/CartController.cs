using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_Commerce.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        public ActionResult AddCart()
        {
            return View();
        }
    }
}