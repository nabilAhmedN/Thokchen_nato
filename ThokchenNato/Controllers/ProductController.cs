using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThokchenNato.DatabaseContext;
using ThokchenNato.Models.Data;
using ThokchenNato.Models.ViewModels;

namespace ThokchenNato.Controllers
{
    public class ProductController : Controller
    {
        ThokchenNatoDbContext db = new ThokchenNatoDbContext();
        ProductVM productVM = new ProductVM();
        Product product = new Product();
        CategoryVM categoryVM = new CategoryVM();
        Category category = new Category();

        // GET: Product
        [HttpGet]
        public ActionResult Index(string tst="")
        {
            return View();
        }


        [Authorize]
        [HttpGet]
        public ActionResult AddProduct()
        {
            productVM.CategoryList = db.categories.ToList().
               Select(c => new SelectListItem
               {
                   Value = c.Id.ToString(),
                   Text = c.CategoryName
               }).ToList();

            return View(productVM);
        }


        [Authorize]
        [HttpPost]
        public ActionResult AddProduct(ProductVM productVM)
        {
            if (productVM.ImageUpload != null)
            {
                string fileName = Path.GetFileNameWithoutExtension(productVM.ImageUpload.FileName);
                string extension = Path.GetExtension(productVM.ImageUpload.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                productVM.PhotoURL = "~/App_Files/Images/" + fileName;
                productVM.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/App_Files/Images/"), fileName));
            }

            ////unique username checking
            //if (db.products.Any(x => x.Name.Equals(productVM.Name)))
            //{
            //    ModelState.AddModelError("", "Product " + productVM.Name + " is taken.");
            //    productVM.Name = "";
            //    productVM.CategoryList = db.categories.ToList().
            //                     Select(c => new SelectListItem
            //                     {
            //                         Value = c.Id.ToString(),
            //                         Text = c.CategoryName
            //                     }).ToList();
            //    return View("AddProduct", productVM);
            //}


            Product product = new Product()
            {
                Name = productVM.Name,
                Price = productVM.Price,
                Amount_Unit = productVM.Amount_Unit,
                PhotoURL = productVM.PhotoURL,
                CategoryId = productVM.CategoryId

            };

            if (ModelState.IsValid)
            {
                db.products.Add(product);
                db.SaveChanges();

            }

            TempData["SM"] = "Product has been added.";
            return RedirectToAction("ViewAll");
        }

        [Authorize]
        [HttpGet]
        public ActionResult ViewAll()
        {
            productVM.Products = db.products.ToList();
            return View(productVM);
        }


        [Authorize]
        public ActionResult Delete(int? id)
        {

            if (id != 0)
            {
                product = db.products.Where(x => x.Id == id).FirstOrDefault();

                db.products.Remove(product);
                db.SaveChanges();

            }

            return RedirectToAction("ViewAll");
        }



        [Authorize]
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id != 0)
            {
                product = db.products.Where(x => x.Id == id).FirstOrDefault();
            }

            product.CategoryList = db.categories.ToList().
                        Select(c => new SelectListItem
                        {
                            Value = c.Id.ToString(),
                            Text = c.CategoryName
                        }).ToList();

            //productVM.Products = product.Products;
            //productVM = new ProductVM(product);

            return View(product);
        }


        [Authorize]
        [HttpPost]
        public ActionResult Edit(Product product)
        {
            if (product.ImageUpload != null)
            {
                string fileName = Path.GetFileNameWithoutExtension(product.ImageUpload.FileName);
                string extension = Path.GetExtension(product.ImageUpload.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                product.PhotoURL = "~/App_Files/Images/" + fileName;
                product.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/App_Files/Images/"), fileName));
            }


            //unique username checking
            //if (db.categories.Any(x => x.CategoryName.Equals(categoryVM.CategoryName)))
            //{
            //    ModelState.AddModelError("", "Category " + categoryVM.CategoryName + " is taken.");
            //    categoryVM.CategoryName = "";
            //    return View("Edit", categoryVM);
            //}


            //using (BinaryReader br = new BinaryReader(categoryVM.ImageUpload.InputStream))
            //{

            //    categoryVM.PhotoBinary = br.ReadBytes(categoryVM.ImageUpload.ContentLength);
            //}

            //Category category = new Category()
            //{
            //    Id = categoryVM.Id,
            //    CategoryName = categoryVM.CategoryName,
            //    PhotoURL = categoryVM.PhotoURL


            //};

            db.Entry(product).State = EntityState.Modified;
            db.SaveChanges();



            TempData["SM"] = "Product has been Updated.";
            return RedirectToAction("ViewAll");
        }


        [Authorize]
        [HttpGet]
        public ActionResult ViewCategory()
        {
            categoryVM.Categories = db.categories.ToList();
            return View(categoryVM);
        }


        [Authorize]
        [HttpGet]
        public ActionResult ViewProducts(int? id)
        {
            productVM.Products = db.products.Where(x=>x.CategoryId==id).ToList();
            return View(productVM);
        }
    }
}