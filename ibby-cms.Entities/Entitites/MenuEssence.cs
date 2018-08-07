using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ibby_cms.Entities.Entitites {
    public class MenuEssence {
        [Key]
        public int Id { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }

        public virtual ICollection<MenuItemEssence> MenuItems { get; set; }
    }
}