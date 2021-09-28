using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThokchenNato.Models.Data;

namespace ThokchenNato.Models.ViewModels
{
    public class ProductVM
    {
        public ProductVM()
        {
            PhotoURL = "~/App_Files/Images/default.jpg";
        }
        public ProductVM(Product product)
        {
            Id = product.Id;
            Name = product.Name;
            Price = product.Price;
            Amount_Unit = product.Amount_Unit;
            CategoryId = product.CategoryId;
            PhotoURL = product.PhotoURL;
            Category = product.Category;
        }

        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Amount_Unit { get; set; }
        [Required]
        [Display(Name ="Category")]
        public int CategoryId { get; set; }
        [Required]
        public string PhotoURL { get; set; }
        public IEnumerable<SelectListItem> CategoryList { get; set; }
        public virtual Category Category { get; set; }

        public HttpPostedFileBase ImageUpload { get; set; }
        public List<Product> Products { get; set; }
    }
}