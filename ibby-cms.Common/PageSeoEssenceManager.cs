using AutoMapper;
using ibby_cms.Common.Abstract;
using ibby_cms.Common.Abstract.Interfaces;
using ibby_cms.Common.Models;
using ibby_cms.Entities.DAL;
using ibby_cms.Entities.Entitites;
using System.Collections.Generic;
using System.Data.Entity;

namespace ibby_cms.Common {
    public class PageSeoEssenceManager : BaseManager, IPageSeoEssenceManager {

        public PageSeoEssenceManager() {
        }

        public void Delete(int id) {
            using (EntitiesContext context = new EntitiesContext()) {
                var item = context.PageSeoEssences.Find(id);

                if (item != null) {
                    context.PageSeoEssences.Remove(item);

                    context.SaveChanges();
                }
            }
        }

        public PageSeoModel Get(int id) {
            using (EntitiesContext context = new EntitiesContext()) {
                var item = context.PageSeoEssences.Find(id);

                if (item == null) {
                    throw new ValidationException("SEO-данные не найдены", "");
                }

                var pageSeoModel = new PageSeoModel {
                    Id = item.Id,
                    Title = item.Title,
                    Descriptions = item.Descriptions,
                    KeyWords = item.KeyWords
                };

                return pageSeoModel;
            }
        }

        public IEnumerable<PageSeoModel> GetAll() {
            using (EntitiesContext context = new EntitiesContext()) {
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<PageSeoEssence, PageSeoModel>()).CreateMapper();
                return mapper.Map<IEnumerable<PageSeoEssence>, List<PageSeoModel>>(context.PageSeoEssences);
            }
        }

        public void SaveSeo(PageSeoModel pageSeoModel) {
            if (pageSeoModel == null) {
                throw new ValidationException("Пустые данные SEO", "");
            }

            var pageSeoEssence = new PageSeoEssence {
                Title = pageSeoModel.Title,
                KeyWords = pageSeoModel.KeyWords,
                Descriptions = pageSeoModel.Descriptions
            };

            using (EntitiesContext context = new EntitiesContext()) {
                context.PageSeoEssences.Add(pageSeoEssence);

                context.SaveChanges();
            }
        }

        public void EditSeo(PageSeoModel pageSeoModel) {
            if (pageSeoModel == null) {
                throw new ValidationException("SEO не найдено", "");
            }

            var pageSeoEssence = new PageSeoEssence {
                Id = pageSeoModel.Id,
                Title = pageSeoModel.Title,
                Descriptions = pageSeoModel.Descriptions,
                KeyWords = pageSeoModel.KeyWords
            };

            using (EntitiesContext context = new EntitiesContext()) {
                context.Entry(pageSeoEssence).State = EntityState.Modified;

                context.SaveChanges();
            }
        }
    }
}