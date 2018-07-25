using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text.RegularExpressions;
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
                PageContentEssence item = context.PageContentEssences.Find(id);

                if (item != null) {
                    context.PageContentEssences.Remove(item);

                    context.SaveChanges();
                }

            }
        }

        public PageContentModel Get(int id) {
            using (EntitiesContext context = new EntitiesContext()) {
                PageContentEssence item = context.PageContentEssences.Find(id);

                if (item == null) {
                    throw new ValidationException("Страница не найдена", "");
                }

                PageContentModel pageContentModel = new PageContentModel {
                    Id = item.Id,
                    Content = item.Content,
                    Header = item.Header,
                    Url = item.Url,
                    IsPublished = item.IsPublished,
                    SeoID = item.SeoID
                };

                return pageContentModel;
            }
        }

        public IEnumerable<PageContentModel> GetAll() {
            using (EntitiesContext context = new EntitiesContext()) {
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<PageContentEssence, PageContentModel>()).CreateMapper();
                return mapper.Map<IEnumerable<PageContentEssence>, List<PageContentModel>>(context.PageContentEssences.Include(o => o.PageSeo));
            }
        }

        public void CreatePage(PageModel pageModel) {
            if (pageModel == null) {
                throw new ValidationException("Пустая страница", "");
            }

            var url = FriendlyUrls.GetFriendlyUrl(!string.IsNullOrEmpty(pageModel.PageContent.Url) ? pageModel.PageContent.Url : pageModel.PageContent.Header);
            if (!HasUrl(url)) {
                throw new ValidationException("Такой url уже существует. Введите другой", "");
            }

            PageContentEssence pageContentEssence = new PageContentEssence {
                Header = pageModel.PageContent.Header,
                Content = pageModel.PageContent.Content,
                Url = url,
                IsPublished = pageModel.PageContent.IsPublished,
                PageSeo = new PageSeoEssence {
                    Title = pageModel.PageSeo.Title,
                    Descriptions = pageModel.PageSeo.Descriptions,
                    KeyWords = pageModel.PageSeo.KeyWords
                }
            };

            using (EntitiesContext context = new EntitiesContext()) {
                context.PageContentEssences.Add(pageContentEssence);

                context.SaveChanges();
            }
        }

        public void EditPage(PageModel pageModel) {
            if (pageModel == null) {
                throw new ValidationException("Страница не найдена", "");
            }

            PageSeoEssence pageSeoEssence = new PageSeoEssence {
                Id = pageModel.PageSeo.Id,
                Title = pageModel.PageSeo.Title,
                Descriptions = pageModel.PageSeo.Descriptions,
                KeyWords = pageModel.PageSeo.KeyWords
            };

            var url = FriendlyUrls.GetFriendlyUrl(!string.IsNullOrEmpty(pageModel.PageContent.Url) ? pageModel.PageContent.Url : pageModel.PageContent.Header);
            PageContentEssence pageContentEssence = new PageContentEssence {
                Id = pageModel.Id,
                Header = pageModel.PageContent.Header,
                Content = pageModel.PageContent.Content,
                Url = url,
                IsPublished = pageModel.PageContent.IsPublished,
                SeoID = pageModel.PageContent.SeoID,
                PageSeo = pageSeoEssence
            };

            using (EntitiesContext context = new EntitiesContext()) {
                context.Entry(pageSeoEssence).State = EntityState.Modified; // спросить почему через lazy loading не изменяет данные
                context.Entry(pageContentEssence).State = EntityState.Modified;

                context.SaveChanges();
            }
        }

        public bool IsPublishPage(int? id) {
            using (EntitiesContext context = new EntitiesContext()) {
                PageContentEssence item = context.PageContentEssences.Find(id);
                item.IsPublished = !item.IsPublished;

                context.SaveChanges();

                return item.IsPublished;
            }
        }

        // метод First() сгенерирует экспшн, если такого url не существует. И что дальше?
        public PageContentModel FindUrl(string url = "404") {
            using (EntitiesContext context = new EntitiesContext()) {
                PageContentEssence item = context.PageContentEssences.First(o => o.Url == url);

                PageContentModel pageContentModel = new PageContentModel {
                    Id = item.Id,
                    Content = item.Content,
                    Header = item.Header,
                    Url = item.Url,
                    IsPublished = item.IsPublished,
                    SeoID = item.SeoID
                };

                return pageContentModel;
            }
        }

        private bool HasUrl(string url) {
            IEnumerable<PageContentModel> allPage = GetAll();

            foreach(var item in allPage) {
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