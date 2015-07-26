using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BlogViajes.EntityModels;

namespace BlogViajes.Web.Models
{
    public class ArticulosListModel
    {
        public List<ContentModel> ContentList { get; set; }
        public List<ContentModel> FeatureList { get; set; }
        public List<ContentModel> RecentList { get; set; }
        public int ActualPage { get; set; }
        private int _amtPages;
        public int AmtPages { get { return (_amtPages == 0 ? ((ContentList.Count % 10) + 1) : _amtPages); } set { _amtPages = value; } }
        public List<CategoryModel> Categories { get; set; }


    }
}