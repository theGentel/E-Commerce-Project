using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_Commerce.Controllers
{
    [Authorize]
    public class ShopDefaultController : Controller
    {
        // GET: ShopDefault
        [Route("AddShopDefault")]
        public ActionResult AddShopDefault()
        {
            return View();
        }
    }
}