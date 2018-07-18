using ibby_cms.Models;
using System.Web.Mvc;
using ibby_cms.Common.Abstract.Interfaces;
//using ibby_cms.Common.Models;
using System.Collections.Generic;
using AutoMapper;
using ibby_cms.Common.Models;
using ibby_cms.Common;
using PagedList;

namespace ibby_cms.Controllers {
    [Authorize(Roles = "Admin")] // только адинимтратор может использовать этот контроллер
    public class AdminController : Controller {
        private readonly IPageContentEssenceManager _pageContentEssenceManager;
        private readonly IPageSeoEssenceManager _pageSeoEssenceManager;
        // в контроллер инжектятся 2 менеджера, которые нужны, для выполнения бизнес-логики
        public AdminController(IPageContentEssenceManager pageContentEssenseManager, IPageSeoEssenceManager pageSeoEssenceManager) {
            _pageContentEssenceManager = pageContentEssenseManager;
            _pageSeoEssenceManager = pageSeoEssenceManager;
        }

        public ActionResult Index() {
            return View();
        }

        public ActionResult ManagementPage(int? page) {
            int pageSize = 10;
            int pageNumber = (page ?? 1);

            IEnumerable<PageContentModel> pageContentModels = _pageContentEssenceManager.GetAll();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<PageContentModel, PageContentViewModel>()).CreateMapper();
            var pages = mapper.Map<IEnumerable<PageContentModel>, List<PageContentViewModel>>(pageContentModels);
            pages.Reverse();

            return View(pages.ToPagedList(pageNumber, pageSize));
        }


        public ActionResult DetailsPage(int? id) {
            PageContentModel page = _pageContentEssenceManager.Get(id.Value);
            PageSeoModel seo = _pageSeoEssenceManager.Get(page.SeoID.Value);
            var pageContent = new PageContentViewModel {
                Id = page.Id,
                Header = page.Header,
                Content = page.Content,
                Url = page.Url,
                IsPublished = page.IsPublished,
                SeoID = page.SeoID,
                Title = seo.Title,
                Descriptions = seo.Descriptions,
                KeyWords = seo.KeyWords
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
                    var pageContentModel = new PageContentModel {
                        Content = pageContent.Content,
                        Url = pageContent.Url,
                        Header = pageContent.Header,
                        IsPublished = false,
                        SeoID = pageContent.SeoID
                    };

                    var pageSeoModel = new PageSeoModel {
                        Title = pageContent.Title,
                        KeyWords = pageContent.KeyWords,
                        Descriptions = pageContent.Descriptions
                    };

                    _pageContentEssenceManager.CreatePageContent(pageContentModel, pageSeoModel);

                    return RedirectToAction("ManagementPage");
                }
            }
            catch (ValidationException ex) {
                ModelState.AddModelError(ex.Property, ex.Message);
            }

            return View(pageContent);
        }

        public ActionResult EditPage(int? id) {
            // todo: реализовать и вызвать метод из менеджера

            PageContentModel page = _pageContentEssenceManager.Get(id.Value);
            PageSeoModel seo = _pageSeoEssenceManager.Get(page.SeoID.Value);
            var pageContent = new PageContentViewModel {
                Id = page.Id,
                Header = page.Header,
                Content = page.Content,
                Url = page.Url,
                IsPublished = page.IsPublished,
                SeoID = page.SeoID,
                Title = seo.Title,
                Descriptions = seo.Descriptions,
                KeyWords = seo.KeyWords
            };

            return View(pageContent);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult EditPage(PageContentViewModel pageContent) {
            // todo: реализовать и вызвать метод из менеджера

            try {
                if (ModelState.IsValid) {
                    var pageContentModel = new PageContentModel {
                        Id = pageContent.Id,
                        Content = pageContent.Content,
                        Url = pageContent.Url,
                        Header = pageContent.Header,
                        IsPublished = pageContent.IsPublished,
                        SeoID = pageContent.SeoID
                    };

                    var pageSeoModel = new PageSeoModel {
                        Id = pageContent.SeoID.Value,
                        Title = pageContent.Title,
                        KeyWords = pageContent.KeyWords,
                        Descriptions = pageContent.Descriptions
                    };


                    _pageContentEssenceManager.EditPage(pageContentModel, pageSeoModel);

                    return RedirectToAction("ManagementPage");
                }
            }
            catch (ValidationException ex) {
                ModelState.AddModelError(ex.Property, ex.Message);
            }

            return View(pageContent);
        }

        public ActionResult DeletePage(int? id) {
            // todo: реализовать и вызвать метод из менеджера

            _pageContentEssenceManager.DeletePage(id.Value);
            return View();
        }


        public ActionResult PublishPage(int? id) {
            _pageContentEssenceManager.PublishPage(id.Value);

            return View();
        }

        //public Boolean isAdminUser(){
        //    if (User.Identity.IsAuthenticated) {
        //        var user = User.Identity;
        //        ApplicationDbContext context = new ApplicationDbContext();

        //        var UserManager = new UserManager<ApplicationUser, int>(new CustomUserStore(context));
        //        var s = UserManager.GetRoles(user.GetUserId<int>());

        //        if (s[0].ToString() == "Admin") {
        //            return true;
        //        }
        //        else {
        //            return false;
        //        }
        //    }
        //    return false;
        //}

        // это то, что должно быть вместо кода выше, если он вообще нужен
        public bool IsAdminUser() {
            if (!User.Identity.IsAuthenticated) {
                return false;
            }
            return User.IsInRole("Admin");
        }
    }
}