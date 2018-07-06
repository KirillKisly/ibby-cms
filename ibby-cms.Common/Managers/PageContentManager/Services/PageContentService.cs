using AutoMapper;
using ibby_cms.Common.Interfaces;
using ibby_cms.Common.Managers.PageContentManager.Interfaces;
using ibby_cms.Common.Managers.PageContentManager.Models;
using ibby_cms.Common.Managers.PageSeoManager.Models;
using ibby_cms.Entities.Entitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ibby_cms.Common.Managers.PageContentManager.Services
{
    public class PageContentService : IPageContentService
    {
        IUnitOfWork Database { get; set; }

        public PageContentService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public PageContentModel GetPage(int? id)
        {
            if (id == null) {
                throw new ValidationException("Не установлено id Страницы", "");
            }

            var pageContent = Database.PageContentEntities.Get(id.Value);
            if (pageContent == null) {
                throw new ValidationException("Страница не найдена", "");
            }

            return new PageContentModel {
                Id = pageContent.Id,
                HtmlContent = pageContent.HtmlContent,
                Content = pageContent.Content,
                Url = pageContent.Url,
                Header = pageContent.Header,
                SeoID = pageContent.SeoID
            };
        }

        public IEnumerable<PageContentModel> GetPages()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<PageContentEssence, PageContentModel>()).CreateMapper();
            return mapper.Map<IEnumerable<PageContentEssence>, List<PageContentModel>>(Database.PageContentEntities.GetAll());
        }

        public PageSeoModel GetSeo(int? id)
        {
            if (id == null) {
                throw new ValidationException("Не установлено id SEO", "");
            }

            var pageSeo = Database.PageSeoEntities.Get(id.Value);
            if (pageSeo == null) {
                throw new ValidationException("SEO не найдено", "");
            }

            return new PageSeoModel {
                Id = pageSeo.Id,
                Title = pageSeo.Title,
                KeyWords = pageSeo.KeyWords,
                Descriptions = pageSeo.Descriptions
            };
        }

        public void MakePageContent(PageContentModel pageContentModel, PageSeoModel pageSeoModel)
        {
            PageSeoEssence pageSeoEssence = new PageSeoEssence {
                Title = pageSeoModel.Title,
                KeyWords = pageSeoModel.KeyWords,
                Descriptions = pageSeoModel.Descriptions
            };

            Database.PageSeoEntities.Create(pageSeoEssence);
            Database.Save();

            PageContentEssence pageContentEssence = new PageContentEssence {
                HtmlContent = pageContentModel.HtmlContent,
                Content = pageContentModel.Content,
                Url = pageContentModel.Url,
                Header = pageContentModel.Header,
                SeoID = pageSeoEssence.Id
            };

            Database.PageContentEntities.Create(pageContentEssence);
            Database.Save();


            /*if (pageContentModel.SeoID != null) {
                PageSeoEssence pageSeoEssence = Database.PageSeoEntities.Get(Convert.ToInt32(pageContentModel.SeoID));

                PageContentEssence pageContentEssence = new PageContentEssence {
                    HtmlContent = pageContentModel.HtmlContent,
                    Content = pageContentModel.Content,
                    Url = pageContentModel.Url,
                    Header = pageContentModel.Header,
                    SeoID = pageSeoEssence.Id
                };

                Database.PageContentEntities.Create(pageContentEssence);
                Database.Save();
            }
            else {
                PageContentEssence pageContentEssence = new PageContentEssence {
                    HtmlContent = pageContentModel.HtmlContent,
                    Content = pageContentModel.Content,
                    Url = pageContentModel.Url,
                    Header = pageContentModel.Header
                };

                Database.PageContentEntities.Create(pageContentEssence);
                Database.Save();
            }*/
        }

        public void DeletePage(int? id)
        {
            if (id == null) {
                throw new ValidationException("Не установлено id Страницы", "");
            }

            var pageContent = Database.PageContentEntities.Get(id.Value);

            if (pageContent.SeoID != null) {
                var seo = Database.PageSeoEntities.Get(Convert.ToInt32(pageContent.SeoID));
               
                Database.PageContentEntities.Delete(pageContent.Id);
                //Database.PageSeoEntities.Delete(seo.Id);
            }
            else {
                Database.PageContentEntities.Delete(pageContent.Id);
            }

            Database.Save();
        }
    }
}