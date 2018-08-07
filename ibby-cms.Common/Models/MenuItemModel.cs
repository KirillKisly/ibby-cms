using ibby_cms.Entities.Entitites;

namespace ibby_cms.Common.Models {
    public class MenuItemModel {
        public int Id { get; set; }
        public int MenuID { get; set; }
        public string Url { get; set; }
        public int? PageID { get; set; }
        public string TitleMenuItem { get; set; }

        public virtual MenuModel MenuModel { get; set; }
        public virtual PageContentModel PageModel { get; set; }
    }
}