using ibby_cms.Models;
using System.Web.Mvc;
using ibby_cms.Common.Abstract.Interfaces;

namespace ibby_cms.Controllers{
    [Authorize(Roles = "Admin")] // только адинимтратор может использовать этот контроллер
    public class AdminController : Controller{
        private readonly IPageContentEssenceManager _pageContentEssenceManager;
        private readonly IPageSeoEssenceManager _pageSeoEssenceManager;
        // в контроллер инжектятся 2 менеджера, которые нужны, для выполнения бизнес-логики
        public AdminController(IPageContentEssenceManager pageContentEssenseManager, IPageSeoEssenceManager pageSeoEssenceManager){
            _pageContentEssenceManager = pageContentEssenseManager;
            _pageSeoEssenceManager = pageSeoEssenceManager;
        }

        // GET: Role
        // какая цель преследуется этим кодом?
        public ActionResult Index(){
            if (User.Identity.IsAuthenticated) {
                if (!IsAdminUser()) {
                    return RedirectToAction("Index", "Home");
                }
            }
            else {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        public ActionResult ManagementPage(int? page){
            // todo: реализовать и вызвать метод из менеджера

            //IEnumerable<PageContentModel> pageContentModels = pageContentService.GetPages();
            //var mapper = new MapperConfiguration(cfg => cfg.CreateMap<PageContentModel, PageContentViewModel>()).CreateMapper();
            //var pages = mapper.Map<IEnumerable<PageContentModel>, List<PageContentViewModel>>(pageContentModels);

            //int pageSize = 10;
            //int pageNumber = (page ?? 1);

            //return View(pages.ToPagedList(pageNumber, pageSize));
            return null;
        }

        public ActionResult DetailsPage(int? id){
            // todo: реализовать и вызвать метод из менеджера

            //PageContentModel page = pageContentService.GetPage(id);
            //PageSeoModel seo = pageContentService.GetSeo(page.SeoID);
            //var pageContent = new PageContentViewModel {
            //    Id = page.Id,
            //    HtmlContent = page.HtmlContent,
            //    Header = page.Header,
            //    Content = page.Content,
            //    Url = page.Url,
            //    SeoID = page.SeoID,
            //    Title = seo.Title,
            //    Descriptions = seo.Descriptions,
            //    KeyWords =  seo.KeyWords
            //};

            //return View(pageContent);
            return null;
        }

        public ActionResult CreatePage(){
            return View();
        }

        [HttpPost]
        public ActionResult CreatePage(PageContentViewModel pageContent){
            // todo: реализовать и вызвать метод из менеджера

            //try {
            //    if (ModelState.IsValid) {
            //        var pageContentModel = new PageContentModel {
            //            HtmlContent = pageContent.HtmlContent,
            //            Content = pageContent.Content,
            //            Url = pageContent.Url,
            //            Header = pageContent.Header,
            //            SeoID = pageContent.SeoID
            //        };

            //        var pageSeoModel = new PageSeoModel {
            //            Title = pageContent.Title,
            //            KeyWords = pageContent.KeyWords,
            //            Descriptions = pageContent.Descriptions
            //        };

            //        pageContentService.CreatePageContent(pageContentModel, pageSeoModel);

            //        return RedirectToAction("ManagementPage");
            //    }
            //}
            //catch (ValidationException ex) {
            //    ModelState.AddModelError(ex.Property, ex.Message);
            //}

            //return View(pageContent);
            return null;
        }

        public ActionResult EditPage(int? id){
            // todo: реализовать и вызвать метод из менеджера

            //PageContentModel page = pageContentService.GetPage(id);
            //PageSeoModel seo = pageContentService.GetSeo(page.SeoID);
            //var pageContent = new PageContentViewModel {
            //    Id = page.Id,
            //    HtmlContent = page.HtmlContent,
            //    Header = page.Header,
            //    Content = page.Content,
            //    Url = page.Url,
            //    SeoID = page.SeoID,
            //    Title = seo.Title,
            //    Descriptions = seo.Descriptions,
            //    KeyWords = seo.KeyWords
            //};

            //return View(pageContent);
            return null;
        }

        [HttpPost]
        public ActionResult EditPage(PageContentViewModel pageContent){
            // todo: реализовать и вызвать метод из менеджера

            //try {
            //    var pageContentModel = new PageContentModel {
            //        Id = pageContent.Id,
            //        HtmlContent = pageContent.HtmlContent,
            //        Content = pageContent.Content,
            //        Url = pageContent.Url,
            //        Header = pageContent.Header,
            //        SeoID = pageContent.SeoID
            //    };

            //    var pageSeoModel = new PageSeoModel {
            //        Id = pageContent.SeoID.Value,
            //        Title = pageContent.Title,
            //        KeyWords = pageContent.KeyWords,
            //        Descriptions = pageContent.Descriptions
            //    };

            //    pageContentService.EditPage(pageContentModel, pageSeoModel);

            //    return RedirectToAction("ManagementPage");
            //}
            //catch (ValidationException ex) {
            //    ModelState.AddModelError(ex.Property, ex.Message);
            //}

            //return View(pageContent);
            return null;
        }

        public ActionResult DeletePage(int? id){
            // todo: реализовать и вызвать метод из менеджера

            //pageContentService.DeletePage(id);
            //return View();
            return null;
        }

        public ActionResult MakePageContent(int? id){
            // todo: реализовать и вызвать метод из менеджера

            //try {
            //    PageContentModel page = pageContentService.GetPage(id);
            //    var pageContent = new PageContentViewModel { Id = page.Id };


            //    return View(pageContent);
            //}
            //catch(ValidationException ex) {
            //    return Content("ОШИБКА!!!!" + ex.Message);
            //}
            return null;
        }

        [HttpPost]
        public ActionResult MakePageContent(PageContentViewModel pageContent, PageSeoViewModel pageSeo){
            // todo: реализовать и вызвать метод из менеджера

            //try {
            //    var pageContentModel = new PageContentModel {
            //        HtmlContent = pageContent.HtmlContent,
            //        Content = pageContent.Content,
            //        Url = pageContent.Url,
            //        Header = pageContent.Header,
            //        SeoID = pageContent.SeoID
            //    };
            //    //pageContentService.MakePageContent(pageContentModel);

            //    return Content("<h2>Страница успешно создана</h2>");
            //}
            //catch(ValidationException ex) {
            //    ModelState.AddModelError(ex.Property, ex.Message);
            //}

            //return View(pageContent);
            return null;
        }

        //protected override void Dispose(bool disposing)
        //{
        //    pageContentService.Dispose();
        //    base.Dispose(disposing);
        //}

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