using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_Commerce.Controllers
{
    [Authorize]
    public class WishlistController : Controller
    {
        // GET: wishlist
        [Route("Addwishlist")]
        public ActionResult Addwishlist()
        {
            return View();
        }
    }
}