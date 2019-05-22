using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AjaxProject.Models
{
    public class Products
    {
        public int ProductId { get; set; }
        public int TypeId { get; set; }
        public string ProductName { get; set; }
        public float Price { get; set; }
        public string Descripiton { get; set; }

    }
}