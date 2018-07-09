using ibby_cms.Entities.Entitites;
using System.Collections.Generic;
using System.Data.Entity;


namespace ibby_cms.Entities.DAL {
    public class EntitiesContextInitializer: DropCreateDatabaseIfModelChanges<EntitiesContext> {
        protected override void Seed(EntitiesContext context) {
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
}