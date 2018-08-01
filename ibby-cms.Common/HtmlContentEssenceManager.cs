using AutoMapper;
using ibby_cms.Common.Abstract;
using ibby_cms.Common.Abstract.Interfaces;
using ibby_cms.Common.Models;
using ibby_cms.Entities.DAL;
using ibby_cms.Entities.Entitites;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ibby_cms.Common {
    public class HtmlContentEssenceManager : BaseManager, IHtmlContentEssenceManager {
        public HtmlContentEssenceManager() {
        }

        public void Delete(int id) {
            using (EntitiesContext context = new EntitiesContext()) {
                var item = context.HtmlContentEssences.Find(id);

                if (item != null) {
                    context.HtmlContentEssences.Remove(item);

                    context.SaveChanges();
                }
            }
        }

        public void EditHtmlContent(HtmlContentModel htmlContentModel) {
            if (htmlContentModel == null) {
                throw new ValidationException("SEO не найдено", "");
            }

            var htmlContentEssence = new HtmlContentEssence {
                Id = htmlContentModel.Id,
                HtmlContent = htmlContentModel.HtmlContent,
                UniqueCode = htmlContentModel.UniqueCode
            };

            using (EntitiesContext context = new EntitiesContext()) {
                context.Entry(htmlContentEssence).State = EntityState.Modified;

                context.SaveChanges();
            }
        }

        public HtmlContentModel Get(int id) {
            using (EntitiesContext context = new EntitiesContext()) {
                var item = context.HtmlContentEssences.Find(id);

                if (item == null) {
                    throw new ValidationException("Html-content не найден", "");
                }

                var htmlContentModel = new HtmlContentModel {
                    Id = item.Id,
                    HtmlContent = item.HtmlContent,
                    UniqueCode = item.UniqueCode
                };

                return htmlContentModel;
            }
        }

        public int GetId(string uniqueCode) {
            using (EntitiesContext context = new EntitiesContext()) {
                var item = context.HtmlContentEssences.FirstOrDefault(x => x.UniqueCode == uniqueCode);

                if (item == null) {
                    throw new ValidationException("Html-content не найден", "");
                }

                return item.Id;
            }
        }

        public IEnumerable<HtmlContentModel> GetAll() {
            using (EntitiesContext context = new EntitiesContext()) {
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<HtmlContentEssence, HtmlContentModel>()).CreateMapper();
                return mapper.Map<IEnumerable<HtmlContentEssence>, List<HtmlContentModel>>(context.HtmlContentEssences);
            }
        }

        public void SaveHtmlContent(HtmlContentModel htmlContentModel) {
            if (htmlContentModel == null) {
                throw new ValidationException("Пустые данные HTML", "");
            }

            var htmlContentEssence = new HtmlContentEssence {
                HtmlContent = htmlContentModel.HtmlContent,
                UniqueCode = htmlContentModel.UniqueCode
            };

            using (EntitiesContext context = new EntitiesContext()) {
                context.HtmlContentEssences.Add(htmlContentEssence);

                context.SaveChanges();
            }
        }
    }
}