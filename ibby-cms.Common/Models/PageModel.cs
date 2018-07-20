using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ibby_cms.Common.Models {
    public class PageModel {
        public int Id { get; set; }
        public int? SeoID { get; set; }
        public string Header { get; set; }
        public string Content { get; set; }
        public string Url { get; set; }
        public bool IsPublished { get; set; }
        public string Title { get; set; }
        public string KeyWords { get; set; }
        public string Descriptions { get; set; }
    }
}