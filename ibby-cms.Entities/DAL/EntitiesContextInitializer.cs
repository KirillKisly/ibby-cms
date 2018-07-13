using ibby_cms.Entities.Entitites;
using System.Collections.Generic;
using System.Data.Entity;


namespace ibby_cms.Entities.DAL {
    public class EntitiesContextInitializer: DropCreateDatabaseIfModelChanges<EntitiesContext> {
        protected override void Seed(EntitiesContext context) {
            var pageSeoEssence = new List<PageSeoEssence> {
                    new PageSeoEssence{Title="Title1", KeyWords= "1, 2, 3, 4, 5", Descriptions="Description" },
                    new PageSeoEssence{Title="Title2", KeyWords= "1, 2, 3, 4, 5", Descriptions="Description" },
                    new PageSeoEssence{Title="Title3", KeyWords= "1, 2, 3, 4, 5", Descriptions="Description" },
                    new PageSeoEssence{Title="Title4", KeyWords= "1, 2, 3, 4, 5", Descriptions="Description" },
                    new PageSeoEssence{Title="Title5", KeyWords= "1, 2, 3, 4, 5", Descriptions="Description" },
                    new PageSeoEssence{Title="Title6", KeyWords= "1, 2, 3, 4, 5", Descriptions="Description" },
                    new PageSeoEssence{Title="Title7", KeyWords= "1, 2, 3, 4, 5", Descriptions="Description" },
                    new PageSeoEssence{Title="Title8", KeyWords= "1, 2, 3, 4, 5", Descriptions="Description" }
                };

            pageSeoEssence.ForEach(c => context.PageSeoEssences.Add(c));
            context.SaveChanges();

            var pageContentEssece = new List<PageContentEssence> {
                    new PageContentEssence { HtmlContent = "HtmlContent1", Content = "Content1", Header = "Header1", Url = "Url1", SeoID=1 },
                    new PageContentEssence { HtmlContent = "HtmlContent2", Content = "Content2", Header = "Header2", Url = "Url2", SeoID=2 },
                    new PageContentEssence { HtmlContent = "HtmlContent3", Content = "Content3", Header = "Header3", Url = "Url3", SeoID=3 },
                    new PageContentEssence { HtmlContent = "HtmlContent4", Content = "Content4", Header = "Header4", Url = "Url4", SeoID=4 },
                    new PageContentEssence { HtmlContent = "HtmlContent5", Content = "Content5", Header = "Header5", Url = "Url5", SeoID=5 },
                    new PageContentEssence { HtmlContent = "HtmlContent6", Content = "Content6", Header = "Header6", Url = "Url6", SeoID=6 },
                    new PageContentEssence { HtmlContent = "HtmlContent7", Content = "Content7", Header = "Header7", Url = "Url7", SeoID=7 },
                    new PageContentEssence { HtmlContent = "HtmlContent8", Content = "Content8", Header = "Header8", Url = "Url8", SeoID=8 }
                };

            pageContentEssece.ForEach(c => context.PageContentEssences.Add(c));
            context.SaveChanges();
        }
    }
}