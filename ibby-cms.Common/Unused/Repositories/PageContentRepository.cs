//using ibby_cms.Common.Interfaces;
//using ibby_cms.Entities.Entitites;
//using ibby_cms.Entities.DAL;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Data.Entity;

//namespace ibby_cms.Common.Repositories
//{
//    public class PageContentRepository : IRepository<PageContentEssence>
//    {
//        private EntitiesContext context;

//        public PageContentRepository(EntitiesContext entitiesContext)
//        {
//            this.context = entitiesContext;
//        }

//        public void Create(PageContentEssence item)
//        {
//            context.PageContentEssences.Add(item);
//        }

//        public void Delete(int id)
//        {
//            PageContentEssence item = context.PageContentEssences.Find(id);
//            if (item != null) {
//                context.PageContentEssences.Remove(item);
//            }
//        }

//        public IEnumerable<PageContentEssence> Find(Func<PageContentEssence, bool> predicate)
//        {
//            return context.PageContentEssences.Include(o => o.PageSeo).Where(predicate).ToList();
//        }

//        public PageContentEssence Get(int id)
//        {
//            return context.PageContentEssences.Find(id);
//        }

//        public IEnumerable<PageContentEssence> GetAll()
//        {
//            return context.PageContentEssences.Include(o => o.PageSeo);
//        }

//        public void Update(PageContentEssence item)
//        {
//            context.Entry(item).State = EntityState.Modified;
//        }
//    }
//}












