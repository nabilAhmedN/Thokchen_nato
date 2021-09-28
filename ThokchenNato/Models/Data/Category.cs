using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ThokchenNato.Models.Data
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string PhotoURL { get; set; }
    }
}