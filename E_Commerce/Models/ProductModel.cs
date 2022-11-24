using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_Commerce.Models
{
    public class ProductModel
    {
        public int ProductID { get; set; }
        public int CategoryID { get; set; }
        public int SubCategoryID { get; set; }
        public string ProductName { get; set; }
        public int ColorID { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public int Status { get; set; }
        public string Image { get; set; }
        public HttpPostedFileBase ImageFile { get; set; }
    }
}