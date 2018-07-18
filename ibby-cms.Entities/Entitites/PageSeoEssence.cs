using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ibby_cms.Entities.Entitites {
    public class PageSeoEssence {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string KeyWords { get; set; }
        public string Descriptions { get; set; }

        public virtual ICollection<PageContentEssence> PageContent { get; set; }
    }
}