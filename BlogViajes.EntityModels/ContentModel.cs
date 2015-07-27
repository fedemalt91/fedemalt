using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;

namespace BlogViajes.EntityModels
{
    public class ContentModel
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string Title { get; set; }
        public List<CommentModel> Comments { get; set; }
        public List<CategoryModel> Categories { get; set; }
        public UserModel User { get; set; }
        public DateTime DatePosted { get; set; }
        public Boolean IsActive { get; set; }
        public Boolean IsFeatured { get; set; }
        public Boolean IsImportant { get; set; }
        public int Resume { get; set; }

        
        
    }

  
}
