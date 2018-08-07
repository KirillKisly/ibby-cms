using System.ComponentModel.DataAnnotations;

namespace ibby_cms.Entities.Entitites {
    public class MenuItemEssence {
        [Key]
        public int Id { get; set; }
        public int? MenuID { get; set; }
        public string Url { get; set; }
        public int? PageID { get; set; }
        public string TitleMenuItem { get; set; }

        public virtual MenuEssence Menu { get; set; }
        public virtual PageContentEssence Page { get; set; }
    }
}