using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_Commerce.Controllers
{
    [Authorize]
    public class SingleProductController : Controller
    {
        // GET: SingleProduct
        [Route("AddSingleProduct")]
        public ActionResult AddSingleProduct()
        {
            return View();
        }
    }
}