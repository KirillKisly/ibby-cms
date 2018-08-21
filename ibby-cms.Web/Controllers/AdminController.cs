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
            try {

                _pageContentEssenceManager.Delete(id);
                _pageSeoEssenceManager.Delete(deletePage.PageSeoModel.Id);

                return RedirectToAction("ManagementPage");
            }
            catch (ValidationException ex) {
                ModelState.AddModelError(ex.Property, ex.Message);

                var pageContent = new PageContentViewModel {
                    Id = deletePage.Id,
                    Header = deletePage.Header,
                    Url = deletePage.Url,
                    IsPublished = deletePage.IsPublished,
                    SeoID = deletePage.SeoID,
                    Title = deletePage.PageSeoModel.Title,
                    Descriptions = deletePage.PageSeoModel.Descriptions,
                    KeyWords = deletePage.PageSeoModel.KeyWords,
                    HtmlContent = deletePage.HtmlContentModel.HtmlContent,
                    UniqueCode = deletePage.HtmlContentModel.UniqueCode
                };

                return View(pageContent);
            }
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

            IEnumerable<MenuModel> menuModels = _menuManager.GetAll();
            var menus = new List<MenuViewModel>();

            foreach (var item in menuModels) {
                var menuViewModel = new MenuViewModel {
                    Id = item.Id,
                    TitleMenu = item.TitleMenu,
                    Code = item.Code,
                };

                if (item.MenuItemsModel != null) {
                    var listMenuItem = new List<MenuItemViewModel>();
                    foreach (var menuItem in item.MenuItemsModel) {
                        var newMenuitem = new MenuItemViewModel {
                            Id = menuItem.Id,
                            MenuID = menuItem.MenuID,
                            PageID = menuItem.PageID,
                            TitleMenuItem = menuItem.TitleMenuItem,
                            Url = menuItem.Url,
                            Weight = menuItem.Weight
                        };
                        listMenuItem.Add(newMenuitem);
                    }
                    menuViewModel.MenuItems = listMenuItem;
                }

                menus.Add(menuViewModel);
            }
            menus.Reverse();

            if (!String.IsNullOrEmpty(searchString)) {
                menus = menus.Where(m => m.TitleMenu.ToUpper().Contains(searchString.ToUpper()) || m.Code.ToUpper().Contains(searchString.ToUpper())).ToList();
            }

            int pageNumber = (page ?? 1);

            return View(menus.ToPagedList(pageNumber, PAGE_SIZE));
        }

        //public ActionResult AddNewMenuItem(int clickCount) {

        //    List<PageContentModel> pages = _pageContentEssenceManager.GetAll().ToList();
        //    pages.Reverse();
        //    ViewBag.Pages = pages;
        //    ViewBag.Number = clickCount;
        //    var menuItem = new MenuItemViewModel();

        //    return PartialView("MenuItemsPartial", menuItem);
        //}

        //public ActionResult CreateMenuTest() {
        //    List<PageContentModel> pages = _pageContentEssenceManager.GetAll().ToList();
        //    pages.Reverse();
        //    ViewBag.Pages = pages;

        //    var menu = new MenuViewModel();
        //    return View(menu);
        //}

        //[HttpPost]
        //public ActionResult CreateMenuTest(IEnumerable<MenuItemViewModel> menuItem) {
        //    List<PageContentModel> pages = _pageContentEssenceManager.GetAll().ToList();
        //    pages.Reverse();
        //    ViewBag.Pages = pages;

        //    //var menu = new MenuViewModel();
        //    return View("CreateMenuTest", menuItem);
        //}

        //public ViewResult Add() {
        //    List<PageContentModel> pages = _pageContentEssenceManager.GetAll().ToList();
        //    pages.Reverse();
        //    ViewBag.Pages = pages;

        //    return View("MenuItemsPartial", new MenuItemViewModel());
        //}


        public ActionResult CreateMenu() {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateMenu(MenuViewModel menuViewModel) {
            try {
                if (ModelState.IsValid) {
                    var menuModel = new MenuModel {
                        Code = menuViewModel.Code,
                        TitleMenu = menuViewModel.TitleMenu
                    };

                    _menuManager.SaveMenu(menuModel);

                    //var pageId = (menuViewModel.Pages == null) ? menuViewModel.PageID : menuViewModel.Pages.Id;

                    //var menuItem = new MenuItemModel {
                    //    MenuID = menuViewModel.MenuID,
                    //    Url = menuViewModel.Url,
                    //    PageID = pageId,
                    //    TitleMenuItem = menuViewModel.TitleMenuItem,
                    //    MenuModel = menu
                    //};

                    //_menuItemManager.SaveMenu(menuItem);

                    return RedirectToAction("ManagementMenu");
                }
            }
            catch (ValidationException ex) {
                ModelState.AddModelError(ex.Property, ex.Message);
            }

            //List<PageContentModel> pages = _pageContentEssenceManager.GetAll().ToList();
            //pages.Reverse();
            //ViewBag.Pages = pages;



            return View(menuViewModel);
        }

        public ActionResult EditMenu(int? id) {
            var menuModel = _menuManager.Get(id.Value);

            var menuViewModel = new MenuViewModel {
                Id = menuModel.Id,
                TitleMenu = menuModel.TitleMenu,
                Code = menuModel.Code,
            };

            return View(menuModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult EditMenu(MenuViewModel menuViewModel) {
            try {
                if (ModelState.IsValid) {
                    var menu = new MenuModel {
                        Id = menuViewModel.Id,
                        Code = menuViewModel.Code,
                        TitleMenu = menuViewModel.TitleMenu
                    };
                    _menuManager.EditMenu(menu);

                    return RedirectToAction("ManagementMenu");
                }
            }
            catch (ValidationException ex) {
                ModelState.AddModelError(ex.Property, ex.Message);
            }

            return View(menuViewModel);
        }

        public ActionResult DeleteMenu(int? id) {
            var menuModel = _menuManager.Get(id.Value);

            var menuViewModel = new MenuViewModel {
                Id = menuModel.Id,
                TitleMenu = menuModel.TitleMenu,
                Code = menuModel.Code,
            };

            if (menuModel.MenuItemsModel != null) {
                var listMenuItem = new List<MenuItemViewModel>();

                foreach (var menuItem in menuModel.MenuItemsModel) {
                    var newMenuitem = new MenuItemViewModel {
                        Id = menuItem.Id,
                        MenuID = menuItem.MenuID,
                        PageID = menuItem.PageID,
                        TitleMenuItem = menuItem.TitleMenuItem,
                        Url = menuItem.Url,
                        Weight = menuItem.Weight
                    };
                    listMenuItem.Add(newMenuitem);
                }

                menuViewModel.MenuItems = listMenuItem;
            }

            return View(menuViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteMenu(int id) {
            var deleteMenu = _menuManager.Get(id);
            _menuManager.Delete(id);

            return RedirectToAction("ManagementMenu");
        }

        //public ActionResult DetailsMenu(int? id) {
        //    MenuItemModel menuItem = _menuItemManager.Get(id.Value);

        //    var menu = new MenuViewModel {
        //        Id = menuItem.Id,
        //        MenuID = menuItem.MenuID,
        //        TitleMenu = menuItem.MenuModel.TitleMenu,
        //        TitleMenuItem = menuItem.TitleMenuItem,
        //        Code = menuItem.MenuModel.Code,
        //        Url = menuItem.Url,
        //        PageID = menuItem.PageID
        //    };

        //    if (menuItem.PageID != null) {
        //        menu.Pages = _pageContentEssenceManager.Get(menuItem.PageID.Value);
        //        menu.TitlePage = menu.Pages.Header;
        //    }

        //    return View(menu);
        //}

        //[HttpPost]
        //[ValidateInput(false)]
        //public ActionResult DetailsMenu(MenuViewModel menuViewModel) {
        //    return View(menuViewModel);
        //}


        //public ActionResult CreateMenuItem(int id) {
        //List<PageContentModel> pages = _pageContentEssenceManager.GetAll().ToList();
        //pages.Reverse();

        //private List<PageContentModel> GetPublishedPages() {
        //    List<PageContentModel> pages = _pageContentEssenceManager.GetAll().ToList();
        //    List<PageContentModel> publishedPages = new List<PageContentModel>();
        //    foreach (var item in pages) {
        //        if (item.IsPublished) {
        //            publishedPages.Add(item);
        //        }
        //    }
        //    publishedPages.Reverse();
        //    //ViewBag.Pages = new SelectList(publishedPages, "Id", "Header");

        //    return publishedPages;
        //}

        public ActionResult CreateMenuItem(int menuID) {
            ViewBag.Pages = GetPublishedPages();
            var menuItem = new MenuItemViewModel() { MenuID = menuID };

            return View(menuItem);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult CreateMenuItem(MenuItemViewModel menuItemViewModel) {
            try {
                if (ModelState.IsValid) {
                    var menuItem = new MenuItemModel {
                        MenuID = menuItemViewModel.MenuID,
                        Url = menuItemViewModel.Url,
                        PageID = menuItemViewModel.PageID,
                        TitleMenuItem = menuItemViewModel.TitleMenuItem,
                        Weight = menuItemViewModel.Weight
                    };

                    _menuItemManager.SaveMenu(menuItem);

                    return RedirectToAction("ManagementMenu");
                }
            }
            catch (ValidationException ex) {
                ModelState.AddModelError(ex.Property, ex.Message);
            }
            ViewBag.Pages = GetPublishedPages();

            return View(menuItemViewModel);
        }

        public ActionResult EditMenuItem(int? id) {
            ViewBag.Pages = GetPublishedPages();

            MenuItemModel menuItem = _menuItemManager.Get(id.Value);

            var menuItemViewModel = new MenuItemViewModel {
                Id = menuItem.Id,
                MenuID = menuItem.MenuID,
                PageID = menuItem.PageID,
                TitleMenuItem = menuItem.TitleMenuItem,
                Url = menuItem.Url,
                Weight = menuItem.Weight
            };

            return View(menuItemViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult EditMenuItem(MenuItemViewModel menuViewModel) {
            try {
                if (ModelState.IsValid) {
                    var menuItem = new MenuItemModel {
                        Id = menuViewModel.Id,
                        MenuID = menuViewModel.MenuID,
                        Url = menuViewModel.Url,
                        PageID = menuViewModel.PageID,
                        TitleMenuItem = menuViewModel.TitleMenuItem,
                        Weight = menuViewModel.Weight
                    };
                    _menuItemManager.EditMenu(menuItem);

                    return RedirectToAction("ManagementMenu");
                }
            }
            catch (ValidationException ex) {
                ModelState.AddModelError(ex.Property, ex.Message);
            }
            ViewBag.Pages = GetPublishedPages();

            return View(menuViewModel);
        }

        public ActionResult DeleteMenuItem(int id) {
            var deleteMenuItem = _menuItemManager.Get(id);
            _menuItemManager.Delete(id);

            return RedirectToAction("ManagementMenu");
        }

        public ActionResult RenderMenu(string menuCode) {
            var menu = _menuManager.RenderMenu(menuCode);
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
                    Url = menuItems.Url,
                    Weight = menuItems.Weight
                };
                listMenuItem.Add(menuItem);
            }
            menuViewModel.MenuItems = listMenuItem;

            ViewBag.Menu = menuViewModel;
            return View();
        }

        private List<PageContentModel> GetPublishedPages() {
            List<PageContentModel> pages = _pageContentEssenceManager.GetAll().ToList();
            List<PageContentModel> publishedPages = new List<PageContentModel>();
            foreach (var item in pages) {
                if (item.IsPublished) {
                    publishedPages.Add(item);
                }
            }
            publishedPages.Reverse();

            return publishedPages;
        }

        #endregion
    }
}