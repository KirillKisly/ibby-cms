using AutoMapper;
using ibby_cms.Common.Abstract;
using ibby_cms.Common.Abstract.Interfaces;
using ibby_cms.Common.Models;
using ibby_cms.Entities.DAL;
using ibby_cms.Entities.Entitites;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ibby_cms.Common {
    public class MenuManager : IMenuManager {
        public void Delete(int id) {
            using (EntitiesContext context = new EntitiesContext()) {
                var item = context.MenuEssences.Find(id);

                if (item != null) {
                    context.MenuEssences.Remove(item);

                    context.SaveChanges();
                }
            }
        }

        public void EditMenu(MenuModel menuModel) {
            if (menuModel == null) {
                throw new ValidationException("Меню отсутствует", "");
            }

            if (HasUniqueCode(menuModel.Code, menuModel.Id)) {
                var menu = new MenuEssence {
                    Id = menuModel.Id,
                    Code = menuModel.Code,
                    TitleMenu = menuModel.TitleMenu
                };

                using (EntitiesContext context = new EntitiesContext()) {
                    context.Entry(menu).State = EntityState.Modified;

                    context.SaveChanges();
                }
            }
            else {
                throw new ValidationException("Меню с таким кодом уже существует.", "");
            }


        }

        public MenuModel Get(int id) {
            using (EntitiesContext context = new EntitiesContext()) {
                //var item = context.MenuEssences.Include(x => x.MenuItems).FirstOrDefaultAsync(a => a.Id.Equals(id));
                var item = context.MenuEssences.Find(id);

                if (item == null) {
                    throw new ValidationException("Меню не найдено", "");
                }

                var menuModel = new MenuModel {
                    Id = item.Id,
                    Code = item.Code,
                    TitleMenu = item.TitleMenu
                };



                if (item.MenuItems != null) {
                    var listMenuItem = new List<MenuItemModel>();
                    foreach (var menuItems in item.MenuItems) {

                        var menu = new MenuItemModel {
                            Id = menuItems.Id,
                            MenuID = menuItems.MenuID.Value,
                            PageID = menuItems.PageID,
                            TitleMenuItem = menuItems.TitleMenuItem,
                            Url = menuItems.Url
                        };
                        listMenuItem.Add(menu);
                    }
                    menuModel.MenuItemsModel = listMenuItem;
                }



                return menuModel;
            }
        }

        public IEnumerable<MenuModel> GetAll() {

            using (EntitiesContext context = new EntitiesContext()) {
                var allMenus = new List<MenuModel> { };

                foreach (var item in context.MenuEssences.Include(o => o.MenuItems)) {
                    allMenus.Add(Get(item.Id));
                }

                return allMenus;
            }


            //using (EntitiesContext context = new EntitiesContext()) {
            //    var mapper = new MapperConfiguration(cfg => cfg.CreateMap<MenuEssence, MenuModel>()).CreateMapper();
            //    var menus = mapper.Map<IEnumerable<MenuEssence>, List<MenuModel>>(context.MenuEssences.Include(o => o.MenuItems));



            //    return menus;
            //}
        }

        public void SaveMenu(MenuModel menuModel) {
            if (menuModel == null) {
                throw new ValidationException("Меню отсутствует", "");
            }

            if (HasUniqueCode(menuModel.Code, menuModel.Id)) {
                var menu = new MenuEssence {
                    Code = menuModel.Code,
                    TitleMenu = menuModel.TitleMenu
                };

                using (EntitiesContext context = new EntitiesContext()) {
                    context.MenuEssences.Add(menu);

                    context.SaveChanges();
                }

            }
            else {
                throw new ValidationException("Меню с таким кодом уже существует.", "");
            }
        }

        private bool HasUniqueCode(string code, int id) {
            IEnumerable<MenuModel> allMenu = GetAll();

            foreach (var item in allMenu) {
                if (item.Code == code && item.Id != id) {
                    return false;
                }
            }

            return true;
        }

        public MenuModel RenderMenu(string menuCode) {
            using (EntitiesContext context = new EntitiesContext()) {
                var menu = context.MenuEssences.Where(a => a.Code == menuCode).FirstOrDefault(); //.Find(menuCode);

                if (menu == null) {
                    throw new ValidationException("Меню не найдено", "");
                }

                var menuModel = new MenuModel {
                    Id = menu.Id,
                    Code = menu.Code,
                    TitleMenu = menu.TitleMenu
                };

                if (menu.MenuItems != null) {
                    var listMenuItem = new List<MenuItemModel>();
                    foreach (var menuItems in menu.MenuItems) {

                        var menuItem = new MenuItemModel {
                            Id = menuItems.Id,
                            MenuID = menuItems.MenuID.Value,
                            PageID = menuItems.PageID,
                            TitleMenuItem = menuItems.TitleMenuItem,
                            Url = menuItems.Url
                        };
                        listMenuItem.Add(menuItem);
                    }
                    menuModel.MenuItemsModel = listMenuItem;
                }

                return menuModel;
            }
        }
    }
}