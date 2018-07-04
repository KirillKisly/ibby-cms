using ibby_cms.Common.Interfaces;
using ibby_cms.Entities.DAL;
using ibby_cms.Entities.Entitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ibby_cms.Common.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private EntitiesContext context;
        private PageContentRepository pageContentRepository;
        private PageSeoRepository pageSeoRepository;

        public EFUnitOfWork(string connectionString)
        {
            context = new EntitiesContext(connectionString);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public IRepository<PageContentEssence> PageContentEntities
        {
            get
            {
                if(pageContentRepository == null) {
                    pageContentRepository = new PageContentRepository(context);
                }

                return pageContentRepository;
            }
        }

        public IRepository<PageSeoEssence> PageSeoEntities
        {
            get
            {
                if(pageSeoRepository == null) {
                    pageSeoRepository = new PageSeoRepository(context);
                }

                return pageSeoRepository;
            }
        }

        private bool disposedValue = false; 

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue) {
                if (disposing) {
                    context.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}