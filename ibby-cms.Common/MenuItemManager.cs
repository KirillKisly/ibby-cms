﻿using AutoMapper;
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

            var url = FriendlyUrls.GetFriendlyUrl(!string.IsNullOrEmpty(menuItemModel.Url) ? menuItemModel.Url : menuItemModel.MenuModel.TitleMenu);
            var menuItem = new MenuItemEssence {
                Id = menuItemModel.Id,
                MenuID = menuItemModel.MenuID,
                Url = url,
                PageID = menuItemModel.PageID,
                TitleMenuItem = menuItemModel.TitleMenuItem,
                Weight = menuItemModel.Weight
            };

            using (EntitiesContext context = new EntitiesContext()) {
                context.Entry(menuItem).State = EntityState.Modified;

                context.SaveChanges();
            }
        }

        public MenuItemModel Get(int id) {
            using (EntitiesContext context = new EntitiesContext()) {
                var item = context.MenuItemEssences.Find(id);
                //var item = context.MenuItemEssences.Include(x => x.Menu).Include(x => x.Page).FirstOrDefault(a => a.Id.Equals(id));

                if (item == null) {
                    throw new ValidationException("Элемент меню не найден", "");
                }

                var menuItemModel = new MenuItemModel {
                    Id = item.Id,
                    MenuID = item.MenuID.Value,
                    Url = item.Url,
                    PageID = item.PageID,
                    TitleMenuItem = item.TitleMenuItem,
                    Weight = item.Weight,
                    MenuModel = new MenuModel {
                        Id = item.Menu.Id,
                        Code = item.Menu.Code,
                        TitleMenu = item.Menu.TitleMenu
                    }
                    //PageModel = new PageContentModel {
                    //    Id = item.Page.Id,
                    //    Header = item.Page.Header,
                    //    HtmlContentID = item.Page.HtmlContentID,
                    //    Url = item.Page.Url,
                    //    IsPublished = item.Page.IsPublished,
                    //    SeoID = item.Page.SeoID,
                    //    HtmlContentModel = new HtmlContentModel {
                    //        Id = item.Page.HtmlContent.Id,
                    //        HtmlContent = item.Page.HtmlContent.HtmlContent,
                    //        UniqueCode = item.Page.HtmlContent.UniqueCode
                    //    },
                    //    PageSeoModel = new PageSeoModel {
                    //        Id = item.Page.PageSeo.Id,
                    //        Title = item.Page.PageSeo.Title,
                    //        KeyWords = item.Page.PageSeo.KeyWords,
                    //        Descriptions = item.Page.PageSeo.Descriptions
                    //    }
                    //}
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
            }
        }

        public void SaveMenu(MenuItemModel menuItemModel) {
            if (menuItemModel == null) {
                throw new ValidationException("Элемент меню отсутствует", "");
            }

            var menuItem = new MenuItemEssence() {
                Url = menuItemModel.Url,
                TitleMenuItem = menuItemModel.TitleMenuItem,
                MenuID = menuItemModel.MenuID,
                PageID = menuItemModel.PageID,
                Weight = menuItemModel.Weight
            };

            using (EntitiesContext context = new EntitiesContext()) {
                context.MenuItemEssences.Add(menuItem);

                context.SaveChanges();
            }
        }
    }
}