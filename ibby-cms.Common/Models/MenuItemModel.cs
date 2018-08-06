using ibby_cms.Entities.Entitites;

namespace ibby_cms.Common.Models {
    public class MenuItemModel {
        public int Id { get; set; }
        public int MenuId { get; set; }
        public string Url { get; set; }
        public int? PageId { get; set; }
        public string Title { get; set; }

        public virtual MenuModel MenuModel { get; set; }
        public virtual PageContentModel PageModel { get; set; }
    }
}