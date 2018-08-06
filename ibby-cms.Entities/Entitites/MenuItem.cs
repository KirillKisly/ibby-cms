using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ibby_cms.Entities.Entitites {
    public class MenuItem {
        public int Id { get; set; }
        public int MenuId { get; set; }
        public string Url { get; set; }
        public int? PageId { get; set; }
        public string Title { get; set; }

        public virtual Menu Menu { get; set; }
        public virtual PageContentEssence Page { get; set; }
    }
}