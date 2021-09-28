using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using ThokchenNato.Models.Data;

namespace ThokchenNato.Models.ViewModels
{
    public class CategoryVM
    {
        public CategoryVM()
        {

            PhotoURL = "~/App_Files/Images/default.jpg";

        }
        public CategoryVM(Category category)
        {
            Id = category.Id;
            CategoryName = category.CategoryName;
            PhotoURL = category.PhotoURL;
        }

        public int Id { get; set; }
        [Required(ErrorMessage = "Category Name is Required")]
        [Display(Name = "Category Name")]
        public string CategoryName { get; set; }

        [Required(ErrorMessage = "Image is Required")]
        [Display(Name = "Image")]
        public string PhotoURL { get; set; }


        [NotMapped]       
        public HttpPostedFileBase ImageUpload { get; set; }
        public List<Category> Categories { get; set; }
    }
}