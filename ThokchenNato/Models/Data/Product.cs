using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ThokchenNato.Models.Data
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price{ get; set; }
        public string Amount_Unit { get; set; }
        public int CategoryId { get; set; }
        public string PhotoURL { get; set; }

        
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

        [NotMapped]
        public List<Product> Products { get; set; }
        [NotMapped]
        public IEnumerable<SelectListItem> CategoryList { get; set; }
        [NotMapped]
        public HttpPostedFileBase ImageUpload { get; set; }

        public Product()
        {
            PhotoURL = "~/App_Files/Images/default.jpg";
        }
    }
}