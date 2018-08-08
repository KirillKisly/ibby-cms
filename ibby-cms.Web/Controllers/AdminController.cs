using ibby_cms.Models;
using System.Web.Mvc;
using ibby_cms.Common.Abstract.Interfaces;
using System.Collections.Generic;
using AutoMapper;
using ibby_cms.Common.Models;
using ibby_cms.Common;
using PagedList;
using System;
using System.Linq;

namespace ibby_cms.Controllers {
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller {
        const int PAGE_SIZE = 15;

        private readonly IPageContentEssenceManager _pageContentEssenceManager;
        private readonly IPageSeoEssenceManager _pageSeoEssenceManager;
        private readonly IHtmlContentEssenceManager _htmlContentEssenceManager;
        private readonly IMenuManager _menuManager;
        private readonly IMenuItemManager _menuItemManager;

        public AdminController(IPageContentEssenceManager pageContentEssenseManager, IPageSeoEssenceManager pageSeoEssenceManager, IHtmlContentEssenceManager htmlContentEssenceManager, IMenuManager menuManager, IMenuItemManager menuItemManager) {
            _pageContentEssenceManager = pageContentEssenseManager;
            _pageSeoEssenceManager = pageSeoEssenceManager;
            _htmlContentEssenceManager = htmlContentEssenceManager;
            _menuManager = menuManager;
            _menuItemManager = menuItemManager;
        }

        public ActionResult Index() {
            return View();
        }

        #region Page

        public ActionResult ManagementPage(int? page, string searchString, string currentFilter) {
            if (searchString != null) {
                page = 1;
            }
            else {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;

            IEnumerable<PageContentModel> pageContentModels = _pageContentEssenceManager.GetAll();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<PageContentModel, PageContentViewModel>()).CreateMapper();
            var pages = mapper.Map<IEnumerable<PageContentModel>, List<PageContentViewModel>>(pageContentModels);
            pages.Reverse();

            if (!String.IsNullOrEmpty(searchString)) {
                pages = pages.Where(p => p.Header.ToUpper().Contains(searchString.ToUpper()) || p.Url.ToUpper().Contains(searchString.ToUpper())).ToList();
            }

            int pageNumber = (page ?? 1);

            return View(pages.ToPagedList(pageNumber, PAGE_SIZE));
        }

        public ActionResult DetailsPage(int? id) {
            var page = _pageContentEssenceManager.Get(id.Value);
            var pageContent = new PageContentViewModel {
                Id = page.Id,
                Header = page.Header,
                Url = page.Url,
                IsPublished = page.IsPublished,
                SeoID = page.SeoID,
                HtmlContentID = page.HtmlContentID,
                Title = page.PageSeoModel.Title,
                Descriptions = page.PageSeoModel.Descriptions,
                KeyWords = page.PageSeoModel.KeyWords,
                HtmlContent = page.HtmlContentModel.HtmlContent,
                UniqueCode = page.HtmlContentModel.UniqueCode
            };

            return View(pageContent);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult DetailsPage(PageContentViewModel pageContent) {
            return View(pageContent);
        }

        public ActionResult CreatePage() {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult CreatePage(PageContentViewModel pageContent) {
            try {
                if (ModelState.IsValid) {
                    var pageSeoModel = new PageSeoModel {
                        Title = pageContent.Title,
                        KeyWords = pageContent.KeyWords,
                        Descriptions = pageContent.Descriptions
                    };

                    var htmlContentModel = new HtmlContentModel {
                        HtmlContent = pageContent.HtmlContent,
                        UniqueCode = System.Guid.NewGuid().ToString()
                    };

                    var pageContentModel = new PageContentModel {
                        Header = pageContent.Header,
                        Url = pageContent.Url,
                        IsPublished = false,
                        PageSeoModel = pageSeoModel,
                        HtmlContentModel = htmlContentModel
                    };
                    _pageContentEssenceManager.CreatePage(pageContentModel);

                    return RedirectToAction("ManagementPage");
                }
            }
            catch (ValidationException ex) {
                ModelState.AddModelError(ex.Property, ex.Message);
            }

            return View(pageContent);
        }

        public ActionResult EditPage(int? id) {
            PageContentModel page = _pageContentEssenceManager.Get(id.Value);
            var pageContent = new PageContentViewModel {
                Id = page.Id,
                Header = page.Header,
                Url = page.Url,
                IsPublished = page.IsPublished,
                SeoID = page.PageSeoModel.Id,
                HtmlContentID = page.HtmlContentModel.Id,
                Title = page.PageSeoModel.Title,
                Descriptions = page.PageSeoModel.Descriptions,
                KeyWords = page.PageSeoModel.KeyWords,
                HtmlContent = page.HtmlContentModel.HtmlContent,
                UniqueCode = page.HtmlContentModel.UniqueCode
            };

            return View(pageContent);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult EditPage(PageContentViewModel pageContent) {
            try {
                if (ModelState.IsValid) {
                    var htmlContentModel = new HtmlContentModel {
                        HtmlContent = pageContent.HtmlContent,
                        UniqueCode = System.Guid.NewGuid().ToString()
                    };
                    _htmlContentEssenceManager.SaveHtmlContent(htmlContentModel);
                    int htmlContentId = _htmlContentEssenceManager.GetId(htmlContentModel.UniqueCode);

                    var pageSeoModel = new PageSeoModel {
                        Id = pageContent.SeoID.Value,
                        Title = pageContent.Title,
                        KeyWords = pageContent.KeyWords,
                        Descriptions = pageContent.Descriptions
                    };
                    _pageSeoEssenceManager.EditSeo(pageSeoModel);

                    var pageContentModel = new PageContentModel {
                        Id = pageContent.Id,
                        Header = pageContent.Header,
                        Url = pageContent.Url,
                        IsPublished = false,
                        SeoID = pageSeoModel.Id,
                        HtmlContentID = htmlContentId
                    };
                    _pageContentEssenceManager.EditPage(pageContentModel);

                    return RedirectToAction("ManagementPage");
                }
            }
            catch (ValidationException ex) {
                ModelState.AddModelError(ex.Property, ex.Message);
            }

            return View(pageContent);
        }

        public ActionResult DeletePage(int? id) {
            PageContentModel page = _pageContentEssenceManager.Get(id.Value);

            var pageContent = new PageContentViewModel {
                Id = page.Id,
                Header = page.Header,
                Url = page.Url,
                IsPublished = page.IsPublished,
                SeoID = page.SeoID,
                Title = page.PageSeoModel.Title,
                Descriptions = page.PageSeoModel.Descriptions,
                KeyWords = page.PageSeoModel.KeyWords,
                HtmlContent = page.HtmlContentModel.HtmlContent,
                UniqueCode = page.HtmlContentModel.UniqueCode
            };

            return View(pageContent);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePage(int id) {
            var deletePage = _pageContentEssenceManager.Get(id);
            _pageContentEssenceManager.Delete(id);
            _pageSeoEssenceManager.Delete(deletePage.PageSeoModel.Id);

            return RedirectToAction("ManagementPage");
        }

        public ActionResult PublishPage(int? id) {
            bool isPublished = _pageContentEssenceManager.IsPublishPage(id.Value);

            return RedirectToAction("ManagementPage");
        }
        #endregion


        // ------------
        // --- Menu ---
        // ------------
        #region Menu
        public ActionResult ManagementMenu(int? page, string searchString, string currentFilter) {
            if (searchString != null) {
                page = 1;
            }
            else {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;

            IEnumerable<MenuItemModel> menuItemModels = _menuItemManager.GetAll();
            var menus = new List<MenuViewModel> { };
            foreach (var item in menuItemModels) {
                var menu = new MenuViewModel {
                    Id = item.Id,
                    MenuID = item.MenuID,
                    Url = item.Url,
                    PageID = item.PageID,
                    TitleMenuItem = item.TitleMenuItem,
                    TitleMenu = item.MenuModel.TitleMenu,
                    TitlePage = item.PageModel.Header,
                    Code = item.MenuModel.Code,
                    Pages = item.PageModel
                };
                menus.Add(menu);
            }
            menus.Reverse();

            if (!String.IsNullOrEmpty(searchString)) {
                menus = menus.Where(m => m.TitleMenuItem.ToUpper().Contains(searchString.ToUpper())).ToList();
            }

            int pageNumber = (page ?? 1);

            return View(menus.ToPagedList(pageNumber, PAGE_SIZE));
        }

        public ActionResult CreateMenu() {
            List<PageContentModel> pages = _pageContentEssenceManager.GetAll().ToList();
            pages.Reverse();
            ViewBag.Pages = new SelectList(pages, "Id", "Header");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult CreateMenu(MenuViewModel menuViewModel) {
            try {
                if (ModelState.IsValid) {
                    var menu = new MenuModel {
                        Code = menuViewModel.Code,
                        TitleMenu = menuViewModel.TitleMenu
                    };

                    var page = (menuViewModel.Pages == null) ? menuViewModel.PageID : menuViewModel.Pages.Id;

                    var menuItem = new MenuItemModel {
                        MenuID = menuViewModel.MenuID,
                        Url = menuViewModel.Url,
                        PageID = page,
                        TitleMenuItem = menuViewModel.TitleMenuItem,
                        MenuModel = menu
                    };

                    _menuItemManager.SaveMenu(menuItem);

                    return RedirectToAction("ManagementMenu");
                }
            }
            catch (ValidationException ex) {
                ModelState.AddModelError(ex.Property, ex.Message);
            }

            List<PageContentModel> pages = _pageContentEssenceManager.GetAll().ToList();
            pages.Reverse();
            ViewBag.Pages = new SelectList(pages, "Id", "Header");

            return View(menuViewModel);
        }

        public ActionResult EditMenu(int? id) {
            List<PageContentModel> pages = _pageContentEssenceManager.GetAll().ToList();
            pages.Reverse();
            ViewBag.Pages = new SelectList(pages, "Id", "Header");

            MenuItemModel menuItem = _menuItemManager.Get(id.Value);
            var menu = new MenuViewModel {
                Id = menuItem.Id,
                MenuID = menuItem.MenuID,
                Url = menuItem.Url,
                PageID = menuItem.PageID,
                TitleMenuItem = menuItem.TitleMenuItem,
                TitleMenu = menuItem.MenuModel.TitleMenu,
                TitlePage = menuItem.PageModel.Header,
                Code = menuItem.MenuModel.Code,
                Pages = menuItem.PageModel
            };

            return View(menu);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult EditMenu(PageContentViewModel pageContent) {
            try {
                if (ModelState.IsValid) {


                    //var htmlContentModel = new HtmlContentModel {
                    //    HtmlContent = pageContent.HtmlContent,
                    //    UniqueCode = System.Guid.NewGuid().ToString()
                    //};
                    //_htmlContentEssenceManager.SaveHtmlContent(htmlContentModel);
                    //int htmlContentId = _htmlContentEssenceManager.GetId(htmlContentModel.UniqueCode);

                    //var pageSeoModel = new PageSeoModel {
                    //    Id = pageContent.SeoID.Value,
                    //    Title = pageContent.Title,
                    //    KeyWords = pageContent.KeyWords,
                    //    Descriptions = pageContent.Descriptions
                    //};
                    //_pageSeoEssenceManager.EditSeo(pageSeoModel);

                    //var pageContentModel = new PageContentModel {
                    //    Id = pageContent.Id,
                    //    Header = pageContent.Header,
                    //    Url = pageContent.Url,
                    //    IsPublished = false,
                    //    SeoID = pageSeoModel.Id,
                    //    HtmlContentID = htmlContentId
                    //};
                    //_pageContentEssenceManager.EditPage(pageContentModel);

                    return RedirectToAction("ManagementMenu");
                }
            }
            catch (ValidationException ex) {
                ModelState.AddModelError(ex.Property, ex.Message);
            }

            return View(pageContent);
        }

        #endregion

    }
}