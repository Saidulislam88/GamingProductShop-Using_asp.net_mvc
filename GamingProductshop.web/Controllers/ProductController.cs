using GamingProductshop.web.ViewModels;
using GamingProductShop.Entities;
using GamingProductShop.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GamingProductshop.web.Controllers
{
    public class ProductController : Controller
    {
        //ProductsService productsService = new ProductsService();
        
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }
        //public ActionResult ProductTable(string search)
        //{

        //    var products = productsService.GetProducts();
        //    if (string.IsNullOrEmpty(search) == false)
        //    {
        //        products = products.Where(p => p.Name !=null && p.Name.ToLower().Contains(search.ToLower())).ToList();
        //    }

        //    return PartialView(products);
        //}
        public ActionResult ProductTable(string search, int? pageNo)
        {
            var pageSize = ConfigurationsService.Instance.PageSize();
            ProductSearchViewModel model = new ProductSearchViewModel();

            model.SearchTerm = search;

            pageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;
            

            var totalRecords = ProductsService.Instance.GetProductsCount(search);
            model.Products = ProductsService.Instance.GetProducts(search, pageNo.Value, pageSize);

            model.Pager = new Pager(totalRecords, pageNo, pageSize);

            return PartialView(model);
        }


        //[HttpGet]
        //public ActionResult Create()
        //{
        //    CategoriesService categoryService = new CategoriesService();
        //    var Categories = categoryService.GetCategories();
        //    return PartialView(Categories);
        //}

        [HttpGet]
        public ActionResult Create()
        {
            NewProductViewModel model = new NewProductViewModel();


            model.AvailableCategories = CategoriesService.Instance.GetAllCategories();

            return PartialView(model);
        }


        //[HttpPost]
        //public ActionResult Create(NewCategoryViewModel model)
        //{
        //    CategoriesService categoryService = new CategoriesService();

        //    var newProduct = new Product();
        //    newProduct.Name = model.Name;
        //    newProduct.Description = model.Description;
        //    newProduct.Price = model.Price;
        //    newProduct.Category = categoryService.GetCategory(model.CategoryID);

        //   productsService.SaveProduct(newProduct);
        //    return RedirectToAction("ProductTable");
        //}

        [HttpPost]
        public ActionResult Create(NewProductViewModel model)
        {

            var newProduct = new Product();
            newProduct.Name = model.Name;
            newProduct.Description = model.Description;
            newProduct.Price = model.Price;
            newProduct.Category = CategoriesService.Instance.GetCategory(model.CategoryID);
            newProduct.ImageURL = model.ImageURL;

            ProductsService.Instance.SaveProduct(newProduct);

            return RedirectToAction("ProductTable");
        }

        //[HttpGet]
        //public ActionResult Edit(int ID)
        //{
        //    var product = productsService.GetProduct(ID);
        //    return PartialView(product);
        //}

        [HttpGet]
        public ActionResult Edit(int ID)
        {
            EditProductViewModel model = new EditProductViewModel();

            var product = ProductsService.Instance.GetProduct(ID);

            model.ID = product.ID;
            model.Name = product.Name;
            model.Description = product.Description;
            model.Price = product.Price;
            model.CategoryID = product.Category != null ? product.Category.ID : 0;
            model.ImageURL = product.ImageURL;

            model.AvailableCategories = CategoriesService.Instance.GetAllCategories();

            return PartialView(model);
        }

        //[HttpPost]
        //public ActionResult Edit(Product product)
        //{

        //    productsService.UpdateProduct(product);
        //    return RedirectToAction("ProductTable");
        //}
        [HttpPost]
        public ActionResult Edit(EditProductViewModel model)
        {
            var existingProduct = ProductsService.Instance.GetProduct(model.ID);
            existingProduct.Name = model.Name;
            existingProduct.Description = model.Description;
            existingProduct.Price = model.Price;
            existingProduct.Category = null;
            existingProduct.CategoryID = model.CategoryID;


            //dont update imageURL if its empty
            if (!string.IsNullOrEmpty(model.ImageURL))
            {
                existingProduct.ImageURL = model.ImageURL;
            }

            existingProduct.ImageURL = model.ImageURL;

            ProductsService.Instance.UpdateProduct(existingProduct);

            return RedirectToAction("ProductTable");
        }

        //[HttpPost]
        //public ActionResult Delete(int ID)
        //{

        //    productsService.DeleteProduct(ID);
        //    return RedirectToAction("ProductTable");
        //}
        [HttpPost]
        public ActionResult Delete(int ID)
        {
            ProductsService.Instance.DeleteProduct(ID);

            return RedirectToAction("ProductTable");
        }

        [HttpGet]
        public ActionResult Details(int ID)
        {
            ProductViewModel model = new ProductViewModel();

            model.Product = ProductsService.Instance.GetProduct(ID);

            if (model.Product == null) return HttpNotFound();

            return View(model);
        }
    }
}