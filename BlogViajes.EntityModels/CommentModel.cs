using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;

namespace BlogViajes.EntityModels
{
    public class CommentModel
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public DateTime DatePosted { get; set; }
        public ContentModel ContentWherePosted { get; set; }
        public Boolean IsActive { get; set; }
        public List<CommentModel> ChildComments { get; set; }
        public UserModel PostingUser { get; set; }
        public bool IsFatherNode { get; set; }

    }

}
