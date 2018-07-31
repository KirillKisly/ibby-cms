using ibby_cms.Entities.Entitites;
using System.Collections.Generic;
using System.Data.Entity;


namespace ibby_cms.Entities.DAL {
    public class EntitiesContextInitializer : DropCreateDatabaseIfModelChanges<EntitiesContext> {
        protected override void Seed(EntitiesContext context) {
            //var pageSeoEssence = new List<PageSeoEssence> {
            //        new PageSeoEssence{Title="Title1", KeyWords= "1, 2, 3, 4, 5", Descriptions="Description" },
            //        new PageSeoEssence{Title="Title2", KeyWords= "1, 2, 3, 4, 5", Descriptions="Description" },
            //        new PageSeoEssence{Title="Title3", KeyWords= "1, 2, 3, 4, 5", Descriptions="Description" },
            //        new PageSeoEssence{Title="Title4", KeyWords= "1, 2, 3, 4, 5", Descriptions="Description" },
            //        new PageSeoEssence{Title="Title5", KeyWords= "1, 2, 3, 4, 5", Descriptions="Description" },
            //        new PageSeoEssence{Title="Title6", KeyWords= "1, 2, 3, 4, 5", Descriptions="Description" },
            //        new PageSeoEssence{Title="Title7", KeyWords= "1, 2, 3, 4, 5", Descriptions="Description" },
            //        new PageSeoEssence{Title="Title8", KeyWords= "1, 2, 3, 4, 5", Descriptions="Description" }
            //    };

            //pageSeoEssence.ForEach(c => context.PageSeoEssences.Add(c));
            //context.SaveChanges();

            //var pageContentEssece = new List<PageContentEssence> {
            //        new PageContentEssence { Content = "Content1", Header = "Header1", Url = "Url1", IsPublished=false, SeoID=1 },
            //        new PageContentEssence { Content = "Content2", Header = "Header2", Url = "Url2", IsPublished=false, SeoID=2 },
            //        new PageContentEssence { Content = "Content3", Header = "Header3", Url = "Url3", IsPublished=false, SeoID=3 },
            //        new PageContentEssence { Content = "Content4", Header = "Header4", Url = "Url4", IsPublished=false, SeoID=4 },
            //        new PageContentEssence { Content = "Content5", Header = "Header5", Url = "Url5", IsPublished=false, SeoID=5 },
            //        new PageContentEssence { Content = "Content6", Header = "Header6", Url = "Url6", IsPublished=false, SeoID=6 },
            //        new PageContentEssence { Content = "Content7", Header = "Header7", Url = "Url7", IsPublished=false, SeoID=7 },
            //        new PageContentEssence { Content = "Content8", Header = "Header8", Url = "Url8", IsPublished=false, SeoID=8 }
            //    };

            //pageContentEssece.ForEach(c => context.PageContentEssences.Add(c));
            //context.SaveChanges();
        }
    }
}