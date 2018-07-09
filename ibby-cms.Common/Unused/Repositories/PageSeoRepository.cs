//using ibby_cms.Common.Interfaces;
//using ibby_cms.Entities.DAL;
//using ibby_cms.Entities.Entitites;
//using System;
//using System.Collections.Generic;
//using System.Data.Entity;
//using System.Linq;
//using System.Web;

//namespace ibby_cms.Common.Repositories
//{
//    public class PageSeoRepository : IRepository<PageSeoEssence>
//    {
//        private EntitiesContext context;

//        public PageSeoRepository(EntitiesContext entitiesContext)
//        {
//            this.context = entitiesContext;
//        }

//        public void Create(PageSeoEssence item)
//        {
//            context.PageSeoEssences.Add(item);
//        }

//        public void Delete(int id)
//        {
//            PageSeoEssence item = context.PageSeoEssences.Find(id);
//            if (item != null) {
//                context.PageSeoEssences.Remove(item);
//            }
//        }

//        public IEnumerable<PageSeoEssence> Find(Func<PageSeoEssence, bool> predicate)
//        {
//            return context.PageSeoEssences.Where(predicate).ToList();
//        }

//        public PageSeoEssence Get(int id)
//        {
//            return context.PageSeoEssences.Find(id);
//        }

//        public IEnumerable<PageSeoEssence> GetAll()
//        {
//            return context.PageSeoEssences;
//        }

//        public void Update(PageSeoEssence item)
//        {
//            context.Entry(item).State = EntityState.Modified;
//        }
//    }
//}