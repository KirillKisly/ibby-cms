using System.Collections.Generic;

namespace ibby_cms.Common.Models {
    public class HtmlContentModel {
        public int Id { get; set; }
        public string HtmlContent { get; set; }
        public string UniqueCode { get; set; }

        public virtual ICollection<PageContentModel> PageContentModel { get; set; }
    }
}