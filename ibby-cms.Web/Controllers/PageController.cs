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

        public PageController(IPageContentEssenceManager pageContentEssenseManager, IPageSeoEssenceManager pageSeoEssenceManager, IHtmlContentEssenceManager htmlContentEssenceManager) {
            _pageContentEssenceManager = pageContentEssenseManager;
            _pageSeoEssenceManager = pageSeoEssenceManager;
            _htmlContentEssenceManager = htmlContentEssenceManager;
        }

        public ActionResult Index(string permalink) {
            var page = _pageContentEssenceManager.FindUrl(permalink);

            if (!page.IsPublished) {
                return new HttpStatusCodeResult(404);
            }

            //PageSeoModel seo = _pageSeoEssenceManager.Get(page.SeoID.Value);

            var pageContent = new PageContentViewModel {
                Id = page.Id,
                Header = page.Header,
                //Content = page.Content,
                Url = page.Url,
                SeoID = page.SeoID,
                Title = page.PageSeoModel.Title,
                Descriptions = page.PageSeoModel.Descriptions,
                KeyWords = page.PageSeoModel.KeyWords,
                HtmlContentID = page.HtmlContentID,
                HtmlContent = page.HtmlContentModel.HtmlContent,
                UniqueCode = page.HtmlContentModel.UniqueCode
            };

            return View(pageContent);
        }
    }
}