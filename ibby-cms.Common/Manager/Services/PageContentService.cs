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
    public class PageContentService : IPageContentService
    {
        IUnitOfWork Database { get; set; }

        public PageContentService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public void MakePageContent(PageContentModel pageContentModel)
        {
            if (pageContentModel.SeoID != null) {
                PageSeoEssence pageSeoEssence = Database.PageSeoEntities.Get(Convert.ToInt32(pageContentModel.SeoID));

                /*if(pageSeoEssence == null) {
                    throw new ValidationException("SEO Не найдено", "");
                }*/

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
            } 
        }

        IEnumerable<PageSeoModel> IPageContentService.GetSeos()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<PageSeoEssence, PageSeoModel>()).CreateMapper();
            return mapper.Map<IEnumerable<PageSeoEssence>, List<PageSeoModel>>(Database.PageSeoEntities.GetAll());
        }

        /*public PageContentModel GetPage(int? id)
        {
            if (id == null) {
                throw new ValidationException("Не установлено id страницы", "");
            }

            var pageContent = Database.PageContentEntities.Get(id.Value);
            if (pageContent == null) {
                throw new ValidationException("Страница не найдена", "");
            }

            return new PageContentModel {
                HtmlContent = pageContent.HtmlContent,
                Content = pageContent.Content,
                Url = pageContent.Url,
                Header = pageContent.Header,
                SeoID = pageContent.SeoID
            };
        }*/

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

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}