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
    public class MenuItemManager : IMenuItemManager {
        public void Delete(int id) {
            using (EntitiesContext context = new EntitiesContext()) {
                var item = context.MenuItems.Find(id);

                if (item != null) {
                    context.MenuItems.Remove(item);

                    context.SaveChanges();
                }
            }
        }

        public void EditMenu(MenuItemModel menuItemModel) {
            if (menuItemModel == null) {
                throw new ValidationException("Элемент меню отсутствует", "");
            }

            var menuItem = new MenuItem {
                Id = menuItemModel.Id,
                MenuId = menuItemModel.MenuId,
                Url = menuItemModel.Url,
                PageId = menuItemModel.PageId,
                Title = menuItemModel.Title
            };

            using (EntitiesContext context = new EntitiesContext()) {
                context.Entry(menuItem).State = EntityState.Modified;

                context.SaveChanges();
            }
        }

        public MenuItemModel Get(int id) {
            using (EntitiesContext context = new EntitiesContext()) {
                var item = context.MenuItems.Include(x => x.Menu).Include(x => x.Page).FirstOrDefault(a => a.Id.Equals(id));
                //var item = context.MenuItems.Include(x => x.Menu).Include(x => x.Page).Include(x => x.Page.PageSeo).FirstOrDefault(a => a.Id.Equals(id));

                if (item == null) {
                    throw new ValidationException("Элемент меню не найден","");
                }

                var menuItemModel = new MenuItemModel {
                    Id = item.Id,
                    MenuId = item.MenuId,
                    Url  = item.Url,
                    PageId = item.PageId,
                    Title = item.Title,
                    MenuModel = new MenuModel {
                        Id = item.Menu.Id,
                        Code = item.Menu.Code,
                        Title = item.Menu.Title
                    },
                    PageModel = new PageContentModel {
                        Id = item.Page.Id,
                        Header = item.Page.Header,
                        HtmlContentID = item.Page.HtmlContentID,
                        Url = item.Page.Url,
                        IsPublished = item.Page.IsPublished,
                        SeoID = item.Page.SeoID,
                        HtmlContentModel = new HtmlContentModel {
                            Id = item.Page.HtmlContent.Id,
                            HtmlContent = item.Page.HtmlContent.HtmlContent,
                            UniqueCode = item.Page.HtmlContent.UniqueCode
                        },
                        PageSeoModel = new PageSeoModel {
                           Id = item.Page.PageSeo.Id,
                           Title = item.Page.PageSeo.Title,
                           KeyWords = item.Page.PageSeo.KeyWords,
                           Descriptions = item.Page.PageSeo.Descriptions
                        }
                    }
                };

                return menuItemModel;
            }
        }

        public IEnumerable<MenuItemModel> GetAll() {
            throw new System.NotImplementedException();
        }

        public void SaveMenu(MenuItemModel menuItemModel) {
            throw new System.NotImplementedException();
        }
    }
}