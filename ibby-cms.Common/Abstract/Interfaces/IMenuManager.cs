using ibby_cms.Common.Models;
using System.Collections.Generic;

namespace ibby_cms.Common.Abstract.Interfaces {
    public interface IMenuManager {
        IEnumerable<MenuModel> GetAll();
        MenuModel Get(int id);
        void Delete(int id);
        void SaveMenu(MenuModel menuModel);
        void EditMenu(MenuModel menuModel);
    }
}
