using System.Collections.Generic;

namespace ibby_cms.Common.Models{
    public class PageContentModel{
        public int Id { get; set; }
        public string Header { get; set; }
        public string Url { get; set; }
        public bool IsPublished { get; set; }
        public int? HtmlContentID { get; set; }
        public int? SeoID { get; set; }

        public virtual PageSeoModel PageSeoModel { get; set; }
        public virtual HtmlContentModel HtmlContentModel { get; set; }
    }
}