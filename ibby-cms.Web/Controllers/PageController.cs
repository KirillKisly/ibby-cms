using ibby_cms.Common.Abstract.Interfaces;
using ibby_cms.Common.Models;
using ibby_cms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ibby_cms.Controllers {
    public class PageController : Controller {
        private readonly IPageContentEssenceManager _pageContentEssenceManager;
        private readonly IPageSeoEssenceManager _pageSeoEssenceManager;
        private readonly IHtmlContentEssenceManager _htmlContentEssenceManager;
        private readonly IMenuManager _menuManager;

        public PageController(IPageContentEssenceManager pageContentEssenseManager, IPageSeoEssenceManager pageSeoEssenceManager, IHtmlContentEssenceManager htmlContentEssenceManager, IMenuManager menuManager) {
            _pageContentEssenceManager = pageContentEssenseManager;
            _pageSeoEssenceManager = pageSeoEssenceManager;
            _htmlContentEssenceManager = htmlContentEssenceManager;
            _menuManager = menuManager;
        }

        
        public ActionResult Index(string permalink, string codeMenu) {
            var page = _pageContentEssenceManager.FindUrl(permalink);

            if (!page.IsPublished) {
                return new HttpStatusCodeResult(404);
            }

            var pageContent = new PageContentViewModel {
                Id = page.Id,
                Header = page.Header,
                Url = page.Url,
                SeoID = page.SeoID,
                Title = page.PageSeoModel.Title,
                Descriptions = page.PageSeoModel.Descriptions,
                KeyWords = page.PageSeoModel.KeyWords,
                HtmlContentID = page.HtmlContentID,
                HtmlContent = page.HtmlContentModel.HtmlContent,
                UniqueCode = page.HtmlContentModel.UniqueCode
            };

            // Has menu
            if (!String.IsNullOrEmpty(codeMenu)) {
                var menu = _menuManager.RenderMenu(codeMenu);
                var menuViewModel = new MenuViewModel {
                    Id = menu.Id,
                    TitleMenu = menu.TitleMenu,
                    Code = menu.Code
                };

                var listMenuItem = new List<MenuItemViewModel>();
                foreach (var menuItems in menu.MenuItemsModel) {

                    var menuItem = new MenuItemViewModel {
                        Id = menuItems.Id,
                        MenuID = menuItems.MenuID,
                        PageID = menuItems.PageID,
                        TitleMenuItem = menuItems.TitleMenuItem,
                        Url = menuItems.Url
                    };
                    listMenuItem.Add(menuItem);
                }
                menuViewModel.MenuItems = listMenuItem;


                ViewBag.Menu = menuViewModel;
            }

            return View(pageContent);
        }
    }
}