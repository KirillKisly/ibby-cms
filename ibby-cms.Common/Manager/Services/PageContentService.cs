using ibby_cms.Common.Interfaces;
using ibby_cms.Common.Manager.Interfaces;
using ibby_cms.Common.Manager.Models;
using ibby_cms.Entities.Entitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;

namespace ibby_cms.Common.Manager.Services
{
    public class PageContentService: IPageContentService
    {
        IUnitOfWork Database { get; set; }

        public PageContentService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public void MakePageContent(PageContentModel pageContentModel)
        {
            //PageSeoEssence pageSeoEssence = Database.PageSeoEntities.Get(pageContentModel.SeoID);
            PageContentEssence pageContentEssence = new PageContentEssence {
                HtmlContent = pageContentModel.HtmlContent,
                Content = pageContentModel.Content,
                Url = pageContentModel.Url,
                Header = pageContentModel.Header,
                SeoID = pageContentModel.SeoID // не знаю что делать с SEO ID
            };

            Database.PageContentEntities.Create(pageContentEssence);
            Database.Save();
        }

        public IEnumerable<PageContentModel> GetPages()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<PageContentEssence, PageContentModel>()).CreateMapper();
            return mapper.Map<IEnumerable<PageContentEssence>, List<PageContentModel>>(Database.PageContentEntities.GetAll());
        }

        public PageContentModel GetPage(int? id)
        {
            if (id == null) {
                throw new ValidationException("Не установлено id страницы", "");
            }

            var pageContent = Database.PageContentEntities.Get(id.Value);
            if(pageContent == null) {
                throw new ValidationException("Страница не найдена", "");
            }

            return new PageContentModel {
                HtmlContent = pageContent.HtmlContent,
                Content = pageContent.Content,
                Url = pageContent.Url,
                Header = pageContent.Header,
                SeoID = pageContent.SeoID 
            };
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}