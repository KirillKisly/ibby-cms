using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using ibby_cms.Common.Abstract;
using ibby_cms.Common.Abstract.Interfaces;
using ibby_cms.Common.Models;
using ibby_cms.Entities.DAL;
using ibby_cms.Entities.Entitites;

namespace ibby_cms.Common {
    public class PageContentEssenceManager : BaseManager, IPageContentEssenceManager {
        private EntitiesContext context;
        private PageSeoEssenceManager seoManager;

        public PageContentEssenceManager(EntitiesContext entitiesContext) {
            context = entitiesContext;
            seoManager = new PageSeoEssenceManager(context);
        }

        public void Create(PageContentEssence pageContentEssence) {
            //PageContentEssence pageContentEssence = new PageContentEssence {
            //    HtmlContent = pageContentModel.HtmlContent,
            //    Content = pageContentModel.Content,
            //    Header = pageContentModel.Header,
            //    Url = pageContentModel.Url,
            //    SeoID = pageContentModel.SeoID
            //};

            context.PageContentEssences.Add(pageContentEssence);
        }

        public void Delete(int id) {
            PageContentEssence item = context.PageContentEssences.Find(id);
            if (item != null) {
                context.PageContentEssences.Remove(item);
            }
        }

        public PageContentModel Get(int id) {
            PageContentEssence item = context.PageContentEssences.Find(id);

            PageContentModel pageContentModel = new PageContentModel {
                Id = item.Id,
                HtmlContent = item.HtmlContent,
                Content = item.Content,
                Header = item.Header,
                Url = item.Url,
                SeoID = item.SeoID
            };

            return pageContentModel;
        }

        public IEnumerable<PageContentModel> GetAll() {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<PageContentEssence, PageContentModel>()).CreateMapper();
            return mapper.Map<IEnumerable<PageContentEssence>, List<PageContentModel>>(context.PageContentEssences.Include(o => o.PageSeo));
        }

        public void Update(PageContentEssence pageContentEssence) {
            //PageContentEssence pageContentEssence = new PageContentEssence {
            //    Id = pageContentModel.Id,
            //    HtmlContent = pageContentModel.HtmlContent,
            //    Content = pageContentModel.Content,
            //    Url = pageContentModel.Url,
            //    Header = pageContentModel.Header,
            //    SeoID = pageContentModel.SeoID
            //};

            context.Entry(pageContentEssence).State = EntityState.Modified;
        }

        public void CreatePageContent(PageContentModel pageContentModel, PageSeoModel pageSeoModel) {
            PageSeoEssence pageSeoEssence = new PageSeoEssence {
                Title = pageSeoModel.Title,
                KeyWords = pageSeoModel.KeyWords,
                Descriptions = pageSeoModel.Descriptions
            };

            seoManager.Create(pageSeoEssence);
            context.SaveChanges();

            PageContentEssence pageContentEssence = new PageContentEssence {
                HtmlContent = pageContentModel.HtmlContent,
                Content = pageContentModel.Content,
                Url = pageContentModel.Url,
                Header = pageContentModel.Header,
                SeoID = pageSeoEssence.Id
            };

            Create(pageContentEssence);
            context.SaveChanges();


            //PageSeoEssenceManager seoManager = new PageSeoEssenceManager(context);

            //PageSeoEssence pageSeoEssence = new PageSeoEssence {
            //    Title = pageSeoModel.Title,
            //    Descriptions = pageSeoModel.Descriptions,
            //    KeyWords = pageSeoModel.KeyWords
            //};

            //seoManager.Create(pageSeoModel);
            //context.SaveChanges();

            //PageContentModel pageContent = new PageContentModel {
            //    HtmlContent = pageContentModel.HtmlContent,
            //    Content = pageContentModel.Content,
            //    Url = pageContentModel.Url,
            //    Header = pageContentModel.Header,
            //    SeoID = pageSeoEssence.Id
            //};


            // // РЕАЛИЗОВАТЬ ВОТ ЭТО. НА ЧЁМ ОСТАНОВИЛСЯ!!!!!!!
            //Create(pageContent);
            //context.SaveChanges();
        }

        public void EditPage(PageContentModel pageContentModel, PageSeoModel pageSeoModel) {
            if (pageContentModel == null) {
                throw new ValidationException("Страница не найдена", "");
            }

            PageSeoEssence pageSeoEssence = new PageSeoEssence {
                Id = pageSeoModel.Id,
                Title = pageSeoModel.Title,
                KeyWords = pageSeoModel.KeyWords,
                Descriptions = pageSeoModel.Descriptions
            };

            PageContentEssence pageContentEssence = new PageContentEssence {
                Id = pageContentModel.Id,
                HtmlContent = pageContentModel.HtmlContent,
                Content = pageContentModel.Content,
                Url = pageContentModel.Url,
                Header = pageContentModel.Header,
                SeoID = pageContentModel.SeoID,
                PageSeo = pageSeoEssence
            };

            seoManager.Update(pageSeoEssence);
            Update(pageContentEssence);

            context.SaveChanges();
        }

        public void DeletePage(int? id) {
            if (id == null) {
                throw new ValidationException("Не установлено id страницы", "");
            }

            var pageContent = Get(id.Value);
            if (pageContent.SeoID != null) {
                var pageSeo = seoManager.Get(pageContent.SeoID.Value);
                Delete(pageContent.Id);

                if (pageSeo != null) {
                    seoManager.Delete(pageSeo.Id);
                }
            }
            else {
                Delete(pageContent.Id);
            }
            
            context.SaveChanges();
        }
    }
}