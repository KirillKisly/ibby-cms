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

        public PageContentModel Get(int id) {
            using (EntitiesContext context = new EntitiesContext()) {
                var item = context.PageContentEssences.Include(x => x.PageSeo).Include(x => x.HtmlContent).FirstOrDefault(a => a.Id.Equals(id));

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



        // ---------------------------------------------------

        // не вижу смысла в этом методе
        //public void Create(PageContentEssence pageContentEssence) {
        //    context.PageContentEssences.Add(pageContentEssence);
        //}


        // какой смысл создавать этот метод в одну строку?
        //public void Update(PageContentEssence pageContentEssence) {
        //    context.Entry(pageContentEssence).State = EntityState.Modified;
        //}


        // опять 2 модели. Неужели у тебя 2 страницы?
        //public void CreatePageContent(PageContentModel pageContentModel, PageSeoModel pageSeoModel) {
        //    PageSeoEssence pageSeoEssence = new PageSeoEssence {
        //        Title = pageSeoModel.Title,
        //        KeyWords = pageSeoModel.KeyWords,
        //        Descriptions = pageSeoModel.Descriptions
        //    };

        //    seoManager.Create(pageSeoEssence);
        //    //context.SaveChanges();

        //    string url = "";
        //    if (!string.IsNullOrEmpty(pageContentModel.Url)) {
        //        url = FriendlyUrls.GetFriendlyUrl(pageContentModel.Url);
        //    }
        //    else {
        //        url = FriendlyUrls.GetFriendlyUrl(pageContentModel.Header);
        //    }

        //    PageContentEssence pageContentEssence = new PageContentEssence {
        //        Content = pageContentModel.Content,
        //        Url = url,
        //        Header = pageContentModel.Header,
        //        IsPublished = pageContentModel.IsPublished,
        //        SeoID = pageSeoEssence.Id,
        //        // заполнить данные !!!!!! PageSeo = new 
        //    };

        //    Create(pageContentEssence);
        //    context.SaveChanges();
        //}


        // зачем обновлять все это отдельно?
        // зачем 2 модели на вход? сделай одну общую
        //public void EditPage(PageContentModel pageContentModel, PageSeoModel pageSeoModel) {
        //    if (pageContentModel == null) {
        //        throw new ValidationException("Страница не найдена", "");
        //    }

        //    PageSeoEssence pageSeoEssence = new PageSeoEssence {
        //        Id = pageSeoModel.Id,
        //        Title = pageSeoModel.Title,
        //        KeyWords = pageSeoModel.KeyWords,
        //        Descriptions = pageSeoModel.Descriptions
        //    };

        //    // этот код можно переписать в одну строку
        //    //string url = "";
        //    //if (!string.IsNullOrEmpty(pageContentModel.Url)) {
        //    //    url = FriendlyUrls.GetFriendlyUrl(pageContentModel.Url);
        //    //}
        //    //else {
        //    //    url = FriendlyUrls.GetFriendlyUrl(pageContentModel.Header);
        //    //}
        //    // вот так
        //    var url = FriendlyUrls.GetFriendlyUrl(!string.IsNullOrEmpty(pageContentModel.Url) ? pageContentModel.Url : pageContentModel.Header);

        //    PageContentEssence pageContentEssence = new PageContentEssence {
        //        Id = pageContentModel.Id,
        //        Content = pageContentModel.Content,
        //        Url = url,
        //        Header = pageContentModel.Header,
        //        IsPublished = pageContentModel.IsPublished,
        //        SeoID = pageContentModel.SeoID,
        //        PageSeo = pageSeoEssence
        //    };

        //    seoManager.Update(pageSeoEssence);
        //    Update(pageContentEssence);

        //    context.SaveChanges();
        //}


        // я не понимаю назначение данного метода
        // при удалении в любом из менеджеров ты должен удалить связанные сущности
        // если таковые имеются
        // если тебе нужно делать что-то сложное, заведи еще одного менеджера и сделай это там
        //public void DeletePage(int? id) {
        //    var pageContent = Get(id.Value);
        //    Delete(pageContent.Id);

        //    context.SaveChanges();
        //}

        // это все можно упростить
        //public string GenerateUrl(string url) {
        //    // make the url lowercase
        //    string encodedUrl = (url ?? "").ToLower();

        //    // replace & with and
        //    encodedUrl = Regex.Replace(encodedUrl, @"\&+", "and");

        //    // remove characters
        //    encodedUrl = encodedUrl.Replace("'", "");

        //    // remove invalid characters
        //    encodedUrl = Regex.Replace(encodedUrl, @"[^a-z0-9]", "-");

        //    // remove duplicates
        //    encodedUrl = Regex.Replace(encodedUrl, @"-+", "-");

        //    // trim leading & trailing characters
        //    encodedUrl = encodedUrl.Trim('-');

        //    return encodedUrl;
        //}
    }
}