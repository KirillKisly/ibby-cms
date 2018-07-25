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
                PageSeoEssence item = context.PageSeoEssences.Find(id);

                if (item != null) {
                    context.PageSeoEssences.Remove(item);

                    context.SaveChanges();
                }
            }
        }

        public PageSeoModel Get(int id) {
            using (EntitiesContext context = new EntitiesContext()) {
                PageSeoEssence item = context.PageSeoEssences.Find(id);

                if (item == null) {
                    throw new ValidationException("SEO-данные не найдены", "");
                }

                PageSeoModel pageSeoModel = new PageSeoModel {
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
    }
}