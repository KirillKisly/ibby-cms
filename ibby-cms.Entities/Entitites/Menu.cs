using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ibby_cms.Entities.Entitites {
    public class Menu {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }

        public virtual ICollection<MenuItem> MenuItems { get; set; }
    }
}