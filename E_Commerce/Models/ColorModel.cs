using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_Commerce.Models
{
    public class ColorModel
    {
        public int ColorID { get; set; }
        public string ColorName { get; set; }
        public int Status { get; set; }
        public string StatusName { get; set; }
        public string Image { get; set; }
        public HttpPostedFileBase ImageFile { get; set; }
    }
}