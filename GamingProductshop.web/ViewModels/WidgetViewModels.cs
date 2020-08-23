﻿using GamingProductShop.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GamingProductshop.web.ViewModels
{
    public class ProductsWidgetViewModel
    {
        public List<Product> Products { get; set; }

        public bool IsLatestProducts { get; set; }
    }
}