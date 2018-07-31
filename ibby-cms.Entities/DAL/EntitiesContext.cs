﻿using System.Data.Entity;
using ibby_cms.Entities.Entitites;

namespace ibby_cms.Entities.DAL{
    public class EntitiesContext : DbContext{
        public EntitiesContext(){
            Database.SetInitializer(new EntitiesContextInitializer());
        }
        public DbSet<PageContentEssence> PageContentEssences { get; set; }
        public DbSet<PageSeoEssence> PageSeoEssences { get; set; }
        public DbSet<HtmlContentEssence> HtmlContentEssences { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            modelBuilder.Entity<PageContentEssence>().ToTable("dbo.PageContentEssence");
            modelBuilder.Entity<PageContentEssence>().HasOptional(c => c.PageSeo).WithMany(a => a.PageContent).HasForeignKey(k => k.SeoID);
            modelBuilder.Entity<PageContentEssence>().HasOptional(c => c.HtmlContent).WithMany(a => a.PageContent).HasForeignKey(k => k.HtmlContentID);

            modelBuilder.Entity<PageSeoEssence>().ToTable("dbo.PageSeoEssence");
            modelBuilder.Entity<PageSeoEssence>().HasMany(c => c.PageContent).WithOptional(a => a.PageSeo);

            modelBuilder.Entity<HtmlContentEssence>().ToTable("dbo.HtmlContentEssence");
            modelBuilder.Entity<HtmlContentEssence>().HasMany(c => c.PageContent).WithOptional(a => a.HtmlContent);
        }
    }
}