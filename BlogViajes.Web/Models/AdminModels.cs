using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BlogViajes.EntityModels;
using System.ComponentModel.DataAnnotations;

namespace BlogViajes.Web.Models
{
    public class ContentModelLoad
    {
        [Required]
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string UserMail { get; set; }
        [Required]
        public string Title { get; set; }
        public List<Int32> CategoriesId { get; set; }
        public List<CategoryModel> AllCategories { get; set; }
        [Required]
        public DateTime DatePosted { get; set; }
        
        public Boolean IsActive { get; set; }
        public Boolean IsFeatured { get; set; }
        public Boolean IsImportant { get; set; }
        [Required]
        public int Resume { get; set; }

    }
}