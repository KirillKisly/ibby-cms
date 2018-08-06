using ibby_cms.Common.Models;
using System.Collections.Generic;

namespace ibby_cms.Common.Abstract.Interfaces {
    interface IMenuItemManager {
        IEnumerable<MenuItemModel> GetAll();
        MenuItemModel Get(int id);
        void Delete(int id);
        void SaveMenu(MenuItemModel menuItemModel);
        void EditMenu(MenuItemModel menuItemModel);
    }
}
