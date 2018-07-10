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
        private EntitiesContext context;

        public PageSeoEssenceManager(EntitiesContext entitiesContext) {
            context = entitiesContext;
        }

        public void Create(PageSeoEssence pageSeoEssence) {
            //PageSeoEssence pageSeoEssence = new PageSeoEssence {
            //    Title = pageSeoModel.Title,
            //    Descriptions = pageSeoModel.Descriptions,
            //    KeyWords = pageSeoModel.KeyWords
            //};

            context.PageSeoEssences.Add(pageSeoEssence);
        }

        public void Delete(int id) {
            PageSeoEssence item = context.PageSeoEssences.Find(id);
            if (item != null) {
                context.PageSeoEssences.Remove(item);
            }
        }

        public PageSeoModel Get(int id) {
            PageSeoEssence item = context.PageSeoEssences.Find(id);

            PageSeoModel pageSeoModel = new PageSeoModel {
                Id = item.Id,
                Title = item.Title,
                Descriptions = item.Descriptions,
                KeyWords = item.KeyWords
            };

            return pageSeoModel;
        }

        public IEnumerable<PageSeoModel> GetAll() {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<PageSeoEssence, PageSeoModel>()).CreateMapper();
            return mapper.Map<IEnumerable<PageSeoEssence>, List<PageSeoModel>>(context.PageSeoEssences);
        }

        public void Update(PageSeoEssence pageSeoEssence) {
            //PageSeoEssence pageSeoEssence = new PageSeoEssence {
            //    Id = pageSeoModel.Id,
            //    Title = pageSeoModel.Title,
            //    Descriptions = pageSeoModel.Descriptions,
            //    KeyWords = pageSeoModel.KeyWords
            //};

            context.Entry(pageSeoEssence).State = EntityState.Modified;
        }
    }
}