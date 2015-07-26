using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;

namespace BlogViajes.EntityModels
{
    public class CategoryModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsFather { get; set; }
        public CategoryModel Father { get; set; }
        public string Type { get; set; }
    }

    public enum CategoryType
    {
        Tag = 1,
        Destination = 2,
        Section = 3

    }


}
