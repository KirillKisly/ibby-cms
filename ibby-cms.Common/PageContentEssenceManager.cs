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
        public PageContentEssenceManager() {
        }

        public void Delete(int id) {
            using (EntitiesContext context = new EntitiesContext()) {
                var item = context.PageContentEssences.Find(id);

                if (item != null) {
                    context.PageContentEssences.Remove(item);

                    context.SaveChanges();
                }
            }
        }

        public PageContentModel Get(int? id) {
            using (EntitiesContext context = new EntitiesContext()) {
                if (id == null) {
                    return null;
                }
                //var item = context.PageContentEssences.Include(x => x.PageSeo).Include(x => x.HtmlContent).FirstOrDefault(a => a.Id.Equals(id));

                var item = context.PageContentEssences.Find(id);

                if (item == null) {
                    throw new ValidationException("Страница не найдена", "");
                }

                var pageContentModel = new PageContentModel {
                    Id = item.Id,
                    Header = item.Header,
                    Url = item.Url,
                    IsPublished = item.IsPublished,
                    SeoID = item.SeoID,
                    HtmlContentID = item.HtmlContentID,
                    HtmlContentModel = new HtmlContentModel {
                        Id = item.HtmlContent.Id,
                        HtmlContent = item.HtmlContent.HtmlContent,
                        UniqueCode = item.HtmlContent.UniqueCode
                    },
                    PageSeoModel = new PageSeoModel {
                        Id = item.PageSeo.Id,
                        Title = item.PageSeo.Title,
                        KeyWords = item.PageSeo.KeyWords,
                        Descriptions = item.PageSeo.Descriptions
                    }
                };

                return pageContentModel;
            }
        }

        public IEnumerable<PageContentModel> GetAll() {
            using (EntitiesContext context = new EntitiesContext()) {
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<PageContentEssence, PageContentModel>()).CreateMapper();
                return mapper.Map<IEnumerable<PageContentEssence>, List<PageContentModel>>(context.PageContentEssences.Include(o => o.PageSeo).Include(o => o.HtmlContent));
            }
        }

        public void CreatePage(PageContentModel pageContentModel) {
            if (pageContentModel == null) {
                throw new ValidationException("Пустая страница", "");
            }

            var url = FriendlyUrls.GetFriendlyUrl(!string.IsNullOrEmpty(pageContentModel.Url) ? pageContentModel.Url : pageContentModel.Header);
            if (!HasUrl(url)) {
                throw new ValidationException("Такой url уже существует.", "");
            }

            var pageContentEssence = new PageContentEssence {
                Header = pageContentModel.Header,
                Url = url,
                IsPublished = pageContentModel.IsPublished,
                HtmlContent = new HtmlContentEssence {
                    HtmlContent = pageContentModel.HtmlContentModel.HtmlContent,
                    UniqueCode = pageContentModel.HtmlContentModel.UniqueCode
                },
                PageSeo = new PageSeoEssence {
                    Title = pageContentModel.PageSeoModel.Title,
                    KeyWords = pageContentModel.PageSeoModel.KeyWords,
                    Descriptions = pageContentModel.PageSeoModel.Descriptions
                }
            };

            using (EntitiesContext context = new EntitiesContext()) {
                context.PageContentEssences.Add(pageContentEssence);

                context.SaveChanges();
            }
        }

        public void EditPage(PageContentModel pageContentModel) {
            if (pageContentModel == null) {
                throw new ValidationException("Страница не найдена", "");
            }

            var url = FriendlyUrls.GetFriendlyUrl(!string.IsNullOrEmpty(pageContentModel.Url) ? pageContentModel.Url : pageContentModel.Header);
            var pageContentEssence = new PageContentEssence {
                Id = pageContentModel.Id,
                Header = pageContentModel.Header,
                Url = url,
                IsPublished = pageContentModel.IsPublished,
                SeoID = pageContentModel.SeoID,
                HtmlContentID = pageContentModel.HtmlContentID
            };

            using (EntitiesContext context = new EntitiesContext()) {
                context.Entry(pageContentEssence).State = EntityState.Modified;

                context.SaveChanges();
            }
        }

        public bool IsPublishPage(int? id) {
            using (EntitiesContext context = new EntitiesContext()) {
                var item = context.PageContentEssences.Find(id);
                item.IsPublished = !item.IsPublished;

                context.SaveChanges();

                return item.IsPublished;
            }
        }

        // метод First() сгенерирует экспшн, если такого url не существует. И что дальше?
        public PageContentModel FindUrl(string url) {
            using (EntitiesContext context = new EntitiesContext()) {
                var item = context.PageContentEssences.First(o => o.Url == url);

                if (item == null) {
                    throw new ValidationException("", "");
                }
                var pageContentModel = Get(item.Id);

                return pageContentModel;
            }
        }

        private bool HasUrl(string url) {
            IEnumerable<PageContentModel> allPage = GetAll();

            foreach (var item in allPage) {
                if (item.Url.Contains(url)) {
                    return false;
                }
            }

            return true;
        }
    }
}