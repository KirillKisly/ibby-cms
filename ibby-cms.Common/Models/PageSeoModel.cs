using System.Collections.Generic;

namespace ibby_cms.Common.Models{
    public class PageSeoModel{
        public int Id { get; set; }
        public string Title { get; set; }
        public string KeyWords { get; set; }
        public string Descriptions { get; set; }

        public virtual ICollection<PageModel> PageModel { get; set; }
    }
}