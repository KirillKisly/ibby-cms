using AutoMapper;
using ibby_cms.Common.Abstract;
using ibby_cms.Common.Abstract.Interfaces;
using ibby_cms.Common.Models;
using ibby_cms.Entities.DAL;
using ibby_cms.Entities.Entitites;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ibby_cms.Common {
    public class MenuItemManager : IMenuItemManager {
        public void Delete(int id) {
            using (EntitiesContext context = new EntitiesContext()) {
                var item = context.MenuItemEssences.Find(id);

                if (item != null) {
                    context.MenuItemEssences.Remove(item);

                    context.SaveChanges();
                }
            }
        }

        public void EditMenu(MenuItemModel menuItemModel) {
            if (menuItemModel == null) {
                throw new ValidationException("Элемент меню отсутствует", "");
            }

            var menuItem = new MenuItemEssence {
                Id = menuItemModel.Id,
                MenuID = menuItemModel.MenuID,
                Url = menuItemModel.Url,
                PageID = menuItemModel.PageID,
                TitleMenuItem = menuItemModel.TitleMenuItem
            };

            using (EntitiesContext context = new EntitiesContext()) {
                context.Entry(menuItem).State = EntityState.Modified;

                context.SaveChanges();
            }
        }

        public MenuItemModel Get(int id) {
            using (EntitiesContext context = new EntitiesContext()) {
                var item = context.MenuItemEssences.Include(x => x.Menu).Include(x => x.Page).FirstOrDefault(a => a.Id.Equals(id));
                //var item = context.MenuItems.Include(x => x.Menu).Include(x => x.Page).Include(x => x.Page.PageSeo).FirstOrDefault(a => a.Id.Equals(id));

                if (item == null) {
                    throw new ValidationException("Элемент меню не найден", "");
                }

                var menuItemModel = new MenuItemModel {
                    Id = item.Id,
                    MenuID = item.MenuID.Value,
                    Url = item.Url,
                    PageID = item.PageID,
                    TitleMenuItem = item.TitleMenuItem,
                    MenuModel = new MenuModel {
                        Id = item.Menu.Id,
                        Code = item.Menu.Code,
                        TitleMenu = item.Menu.TitleMenu
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
            using (EntitiesContext context = new EntitiesContext()) {

                var allMenuItems = new List<MenuItemModel> { };

                foreach (var item in context.MenuItemEssences) {
                    allMenuItems.Add(Get(item.Id));
                }

                return allMenuItems;



                // ВЕРНУТЬСЯ СЮДА!!!!!!!!!!!!!!!
                //var mapper = new MapperConfiguration(cfg => cfg.CreateMap<MenuItemEssence, MenuItemModel>()).CreateMapper();
                //return mapper.Map<IEnumerable<MenuItemEssence>, List<MenuItemModel>>(context.MenuItemEssences.Include(o => o.Menu).Include(o => o.Page));
            }
        }

        public void SaveMenu(MenuItemModel menuItemModel) {
            if (menuItemModel == null) {
                throw new ValidationException("Элемент меню отсутствует", "");
            }

            string url = !String.IsNullOrEmpty(menuItemModel.Url) ? menuItemModel.Url : menuItemModel.PageModel.Url;

            var menuItem = new MenuItemEssence() {
                Url = url,
                TitleMenuItem = menuItemModel.TitleMenuItem,
                Menu = new MenuEssence {
                    Code = menuItemModel.MenuModel.Code,
                    TitleMenu = menuItemModel.MenuModel.TitleMenu
                },
                PageID = menuItemModel.PageID
            };
        }
    }
}