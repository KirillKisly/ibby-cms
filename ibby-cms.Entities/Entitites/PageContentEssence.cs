using System.ComponentModel.DataAnnotations;

namespace ibby_cms.Entities.Entitites {
    public class PageContentEssence {
        [Key]
        public int Id { get; set; }
        public string Header { get; set; }
        public int? HtmlContentID { get; set; }
        //новая сущность контент
        //public string Content { get; set; } 
        public string Url { get; set; }
        public bool IsPublished { get; set; }
        public int? SeoID { get; set; }

        public virtual PageSeoEssence PageSeo { get; set; }
        public virtual HtmlContentEssence HtmlContent { get; set; }
    }
}