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

        public EntitiesContext()
        {
            Database.SetInitializer<EntitiesContext>(new DbInitializer());
        }

        public EntitiesContext(string connectionString) : base(connectionString)
        {

        }

        public class DbInitializer : DropCreateDatabaseIfModelChanges<EntitiesContext>
        {
            protected override void Seed(EntitiesContext context)
            {
                var pageSeoEssence = new List<PageSeoEssence> {
                    new PageSeoEssence{Title="Title", KeyWords= "1, 2, 3, 4, 5", Descriptions="Description" },
                    new PageSeoEssence{Title="Title", KeyWords= "1, 2, 3, 4, 5", Descriptions="Description" },
                    new PageSeoEssence{Title="Title", KeyWords= "1, 2, 3, 4, 5", Descriptions="Description" },
                    new PageSeoEssence{Title="Title", KeyWords= "1, 2, 3, 4, 5", Descriptions="Description" },
                    new PageSeoEssence{Title="Title", KeyWords= "1, 2, 3, 4, 5", Descriptions="Description" },
                    new PageSeoEssence{Title="Title", KeyWords= "1, 2, 3, 4, 5", Descriptions="Description" },
                    new PageSeoEssence{Title="Title", KeyWords= "1, 2, 3, 4, 5", Descriptions="Description" },
                    new PageSeoEssence{Title="Title", KeyWords= "1, 2, 3, 4, 5", Descriptions="Description" }
                };

                pageSeoEssence.ForEach(c => context.PageSeoEssences.Add(c));
                context.SaveChanges();

                var pageContentEssece = new List<PageContentEssence> {
                    new PageContentEssence { HtmlContent = "HtmlContent", Content = "Content", Header = "Header", Url = "Url", SeoID=1 },
                    new PageContentEssence { HtmlContent = "HtmlContent", Content = "Content", Header = "Header", Url = "Url", SeoID=2 },
                    new PageContentEssence { HtmlContent = "HtmlContent", Content = "Content", Header = "Header", Url = "Url", SeoID=1 },
                    new PageContentEssence { HtmlContent = "HtmlContent", Content = "Content", Header = "Header", Url = "Url", SeoID=2 },
                    new PageContentEssence { HtmlContent = "HtmlContent", Content = "Content", Header = "Header", Url = "Url", SeoID=1 },
                    new PageContentEssence { HtmlContent = "HtmlContent", Content = "Content", Header = "Header", Url = "Url", SeoID=2 },
                    new PageContentEssence { HtmlContent = "HtmlContent", Content = "Content", Header = "Header", Url = "Url", SeoID=1 },
                    new PageContentEssence { HtmlContent = "HtmlContent", Content = "Content", Header = "Header", Url = "Url", SeoID=2 }
                };

                pageContentEssece.ForEach(c => context.PageContentEssences.Add(c));
                context.SaveChanges();
            }
        }

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