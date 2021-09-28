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
    public class CategoryController : Controller
    {
        ThokchenNatoDbContext db = new ThokchenNatoDbContext();
        CategoryVM categoryVM = new CategoryVM();
        Category category = new Category();
        // GET: Category
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        [HttpGet]
        public ActionResult AddCategory()
        {
            return View(categoryVM);
        }


        [Authorize]
        [HttpPost]
        public ActionResult AddCategory(CategoryVM categoryVM)
        {
            //if (!ModelState.IsValid)
            //{
            //    return View("AddCategory", categoryVM);
            //}

            if (categoryVM.ImageUpload != null)
            {
                string fileName = Path.GetFileNameWithoutExtension(categoryVM.ImageUpload.FileName);
                string extension = Path.GetExtension(categoryVM.ImageUpload.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                categoryVM.PhotoURL = "~/App_Files/Images/" + fileName;
                categoryVM.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/App_Files/Images/"), fileName));
            }

            //unique username checking
            if (db.categories.Any(x => x.CategoryName.Equals(categoryVM.CategoryName)))
            {
                ModelState.AddModelError("", "Category " + categoryVM.CategoryName + " is taken.");
                categoryVM.CategoryName = "";
                return View("AddCategory", categoryVM);
            }


            Category category = new Category()
            {
                CategoryName = categoryVM.CategoryName,
                PhotoURL=categoryVM.PhotoURL


            };

            db.categories.Add(category);
            db.SaveChanges();

            TempData["SM"] = "Category has been added.";
            return RedirectToAction("ViewAll");
        }


        [Authorize]
        public ActionResult ViewAll()
        {
            categoryVM.Categories = db.categories.ToList();
            return View(categoryVM);
        }



        [Authorize]
        [HttpGet]
        public ActionResult Edit(int? id)
        {

            if (id != 0)
            {
                category = db.categories.Where(x => x.Id == id).FirstOrDefault();
            }

            categoryVM = new CategoryVM(category);

            return View(categoryVM);
        }


        [Authorize]
        [HttpPost]
        public ActionResult Edit(CategoryVM categoryVM)
        {
            if (categoryVM.ImageUpload != null)
            {
                string fileName = Path.GetFileNameWithoutExtension(categoryVM.ImageUpload.FileName);
                string extension = Path.GetExtension(categoryVM.ImageUpload.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                categoryVM.PhotoURL = "~/App_Files/Images/" + fileName;
                categoryVM.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/App_Files/Images/"), fileName));
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

            Category category = new Category()
            {
                Id=categoryVM.Id,
                CategoryName = categoryVM.CategoryName,
                PhotoURL=categoryVM.PhotoURL
                

            };

                db.Entry(category).State = EntityState.Modified;
                db.SaveChanges();
            


            TempData["SM"] = "Category has been Updated.";
            return RedirectToAction("ViewAll");
        }



        [Authorize]
        public ActionResult Delete(int? id)
        {

            if (id != 0)
            {
                category = db.categories.Where(x => x.Id == id).FirstOrDefault();

                db.categories.Remove(category);
                db.SaveChanges();

            }

            return RedirectToAction("ViewAll");
        }
    }
}