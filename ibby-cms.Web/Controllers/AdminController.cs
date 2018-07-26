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
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller {
        private readonly IPageContentEssenceManager _pageContentEssenceManager;
        private readonly IPageSeoEssenceManager _pageSeoEssenceManager;

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
                    var pageSeoModel = new PageSeoModel {
                        Title = pageContent.Title,
                        KeyWords = pageContent.KeyWords,
                        Descriptions = pageContent.Descriptions
                    };

                    var pageContentModel = new PageContentModel {
                        Header = pageContent.Header,
                        Content = pageContent.Content,
                        Url = pageContent.Url,
                        IsPublished = false,
                        PageSeoModel = pageSeoModel
                    };
                    _pageContentEssenceManager.CreatePage(pageContentModel);

                    //var pageModel = new PageModel {
                    //    Id = pageContent.Id,
                    //    PageContent = new PageContentModel {
                    //        Header = pageContent.Header,
                    //        Content = pageContent.Content,
                    //        Url = pageContent.Url,
                    //        IsPublished = pageContent.IsPublished,
                    //    },
                    //    PageSeo = new PageSeoModel {
                    //        Title = pageContent.Title,
                    //        Descriptions = pageContent.Descriptions,
                    //        KeyWords = pageContent.KeyWords
                    //    }
                    //};

                    //_pageContentEssenceManager.CreatePage(pageModel);

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
            try {
                if (ModelState.IsValid) {
                    var pageSeoModel = new PageSeoModel {
                        Id = pageContent.SeoID.Value,
                        Title = pageContent.Title,
                        KeyWords = pageContent.KeyWords,
                        Descriptions = pageContent.Descriptions
                    };
                    _pageSeoEssenceManager.EditPage(pageSeoModel);

                    var pageContentModel = new PageContentModel {
                        Id = pageContent.Id,
                        Header = pageContent.Header,
                        Content = pageContent.Content,
                        Url = pageContent.Url,
                        IsPublished = false,
                        SeoID = pageSeoModel.Id
                    };
                    _pageContentEssenceManager.EditPage(pageContentModel);

                    //var pageModel = new PageModel {
                    //    Id = pageContent.Id,
                    //    PageContent = new PageContentModel {
                    //        Id = pageContent.Id,
                    //        Header = pageContent.Header,
                    //        Content = pageContent.Content,
                    //        Url = pageContent.Url,
                    //        IsPublished = pageContent.IsPublished,
                    //        SeoID = pageContent.SeoID
                    //    },
                    //    PageSeo = new PageSeoModel {
                    //        Id = pageContent.SeoID.Value,
                    //        Title = pageContent.Title,
                    //        Descriptions = pageContent.Descriptions,
                    //        KeyWords = pageContent.KeyWords
                    //    }
                    //};

                    //_pageContentEssenceManager.EditPage(pageModel);

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
        public ActionResult DeletePage(int id) {
            var deletePage = _pageContentEssenceManager.Get(id);
            _pageContentEssenceManager.Delete(id);
            _pageSeoEssenceManager.Delete(deletePage.SeoID.Value);

            return RedirectToAction("ManagementPage");
        }

        public ActionResult PublishPage(int? id) {
            bool isPublished = _pageContentEssenceManager.IsPublishPage(id.Value);

            return RedirectToAction("ManagementPage");
        }
    }
}