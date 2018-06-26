 using ibby_cms.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using ibby_cms.Common.Manager.Interfaces;
using ibby_cms.Common.Manager.Models;
using ibby_cms.Common;

namespace ibby_cms.Controllers
{
    public class AdminController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();
        IPageContentService pageContentService;

        public AdminController(IPageContentService serv)
        {
            pageContentService = serv;
        }

        // GET: Role
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated) {
                if (!isAdminUser()) {
                    return RedirectToAction("Index", "Home");
                }
            }
            else {
                return RedirectToAction("Index", "Home");
            }

            IEnumerable<PageContentModel> pageContentModels = pageContentService.GetPages();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<PageContentModel, PageContentViewModel>()).CreateMapper();
            var pages = mapper.Map<IEnumerable<PageContentModel>, List<PageContentViewModel>>(pageContentModels);

            return View(pages);
        }

        public ActionResult MakePageContent(int? id)
        {
            try {
                PageSeoModel seo = pageContentService.GetSeo(id);
                var pageContent = new PageContentViewModel { SeoID = seo.Id };

                return View(pageContent);
            }
            catch(ValidationException ex) {
                return Content(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult MakePageContent(PageContentViewModel pageContent)
        {
            try {
                var pageContentModel = new PageContentModel {
                    HtmlContent = pageContent.HtmlContent,
                    Content = pageContent.Content,
                    Url = pageContent.Url,
                    Header = pageContent.Header,
                    SeoID = pageContent.SeoID
                };
                pageContentService.MakePageContent(pageContentModel);

                return Content("<h2>Страница успешно создана</h2>");
            }
            catch(ValidationException ex) {
                ModelState.AddModelError(ex.Property, ex.Message);
            }

            return View(pageContent);
        }

        protected override void Dispose(bool disposing)
        {
            pageContentService.Dispose();
            base.Dispose(disposing);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult CreatePage()
        {
            return View();
        }

        public Boolean isAdminUser()
        {
            if (User.Identity.IsAuthenticated) {
                var user = User.Identity;
                ApplicationDbContext context = new ApplicationDbContext();

                var UserManager = new UserManager<ApplicationUser, int>(new CustomUserStore(context));
                var s = UserManager.GetRoles(user.GetUserId<int>());

                if (s[0].ToString() == "Admin") {
                    return true;
                }
                else {
                    return false;
                }
            }
            return false;
        }
    }
}