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

        public PageController(IPageContentEssenceManager pageContentEssenseManager, IPageSeoEssenceManager pageSeoEssenceManager) {
            _pageContentEssenceManager = pageContentEssenseManager;
            _pageSeoEssenceManager = pageSeoEssenceManager;
        }

        public ActionResult Index(string permalink) {
            PageContentModel page = _pageContentEssenceManager.FindUrl(permalink);

            if (!page.IsPublished) {
                return new HttpStatusCodeResult(404);
            }

            PageSeoModel seo = _pageSeoEssenceManager.Get(page.SeoID.Value);
            var pageContent = new PageContentViewModel {
                Id = page.Id,
                HtmlContent = page.HtmlContent,
                Header = page.Header,
                Content = page.Content,
                Url = page.Url,
                SeoID = page.SeoID,
                Title = seo.Title,
                Descriptions = seo.Descriptions,
                KeyWords = seo.KeyWords
            };

            return View(pageContent);
        }
    }
}