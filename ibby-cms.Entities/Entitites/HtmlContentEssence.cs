using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ibby_cms.Entities.Entitites {
    public class HtmlContentEssence {
        [Key]
        public int Id { get; set; }
        public string HtmlContent { get; set; }
        public string UniqueCode { get; set; }

        public virtual ICollection<PageContentEssence> PageContent { get; set; }
    }
}