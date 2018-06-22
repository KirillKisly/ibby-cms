using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using ibby_cms.Entities.Entitites;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ibby_cms.Entities.DAL
{
    public class EntitiesContext : DbContext
    {
        public DbSet<PageContentEssence> PageContentEssences { get; set; }
        public DbSet<PageSeoEssence> PageSeoEssences { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PageContentEssence>().ToTable("dbo.PageContentEssence");
            modelBuilder.Entity<PageContentEssence>()
                .HasOptional(c => c.PageSeo).WithMany(a => a.PageContent).HasForeignKey(k => k.SeoID);

            modelBuilder.Entity<PageSeoEssence>().ToTable("dbo.PageSeoEssence");
            modelBuilder.Entity<PageSeoEssence>()
                .HasMany(c => c.PageContent).WithOptional(a => a.PageSeo);
        }
    }
}