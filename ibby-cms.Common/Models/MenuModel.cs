using ibby_cms.Entities.Entitites;
using System.Collections.Generic;

namespace ibby_cms.Common.Models {
    public class MenuModel {
        public int Id { get; set; }
        public string Code { get; set; }
        public string TitleMenu { get; set; }

        public virtual ICollection<MenuItemModel> MenuItemsModel { get; set; }
    }
}