using ibby_cms.Entities.Entitites;
using System.Collections.Generic;
using System.Data.Entity;


namespace ibby_cms.Entities.DAL {
    public class EntitiesContextInitializer : DropCreateDatabaseIfModelChanges<EntitiesContext> {
        protected override void Seed(EntitiesContext context) {
            var pageSeoEssence = new List<PageSeoEssence> {
                    new PageSeoEssence{Title="Title1", KeyWords= "слово1, слово2, слово3, слово4, слово5", Descriptions="Description" },
                    new PageSeoEssence{Title="Title2", KeyWords= "слово1, слово2, слово3, слово4, слово5", Descriptions="Description" },
                    new PageSeoEssence{Title="Title3", KeyWords= "слово1, слово2, слово3, слово4, слово5", Descriptions="Description" },
                    new PageSeoEssence{Title="Title4", KeyWords= "слово1, слово2, слово3, слово4, слово5", Descriptions="Description" },
                    new PageSeoEssence{Title="Title5", KeyWords= "слово1, слово2, слово3, слово4, слово5", Descriptions="Description" },
                    new PageSeoEssence{Title="Title6", KeyWords= "слово1, слово2, слово3, слово4, слово5", Descriptions="Description" },
                    new PageSeoEssence{Title="Title7", KeyWords= "слово1, слово2, слово3, слово4, слово5", Descriptions="Description" },
                    new PageSeoEssence{Title="Title8", KeyWords= "слово1, слово2, слово3, слово4, слово5", Descriptions="Description" }
                };

            pageSeoEssence.ForEach(c => context.PageSeoEssences.Add(c));
            context.SaveChanges();

            var htmlContentEssence = new List<HtmlContentEssence> {
                new HtmlContentEssence{HtmlContent = "<p>content 1</p>", UniqueCode = "1"},
                new HtmlContentEssence{HtmlContent = "<p>content 2</p>", UniqueCode = "2"},
                new HtmlContentEssence{HtmlContent = "<p>content 3</p>", UniqueCode = "3"},
                new HtmlContentEssence{HtmlContent = "<p>content 4</p>", UniqueCode = "4"},
                new HtmlContentEssence{HtmlContent = "<p>content 5</p>", UniqueCode = "5"},
                new HtmlContentEssence{HtmlContent = "<p>content 6</p>", UniqueCode = "6"},
                new HtmlContentEssence{HtmlContent = "<p>content 7</p>", UniqueCode = "7"},
                new HtmlContentEssence{HtmlContent = "<p>content 8</p>", UniqueCode = "8"}
            };
            htmlContentEssence.ForEach(c => context.HtmlContentEssences.Add(c));
            context.SaveChanges();

            var pageContentEssence = new List<PageContentEssence> {
                    new PageContentEssence {Header = "Header1", Url = "Url1", IsPublished=false, SeoID=1, HtmlContentID = 1 },
                    new PageContentEssence {Header = "Header2", Url = "Url2", IsPublished=false, SeoID=2, HtmlContentID = 2 },
                    new PageContentEssence {Header = "Header3", Url = "Url3", IsPublished=false, SeoID=3, HtmlContentID = 3 },
                    new PageContentEssence {Header = "Header4", Url = "Url4", IsPublished=false, SeoID=4, HtmlContentID = 4 },
                    new PageContentEssence {Header = "Header5", Url = "Url5", IsPublished=false, SeoID=5, HtmlContentID = 5 },
                    new PageContentEssence {Header = "Header6", Url = "Url6", IsPublished=false, SeoID=6, HtmlContentID = 6 },
                    new PageContentEssence {Header = "Header7", Url = "Url7", IsPublished=false, SeoID=7, HtmlContentID = 7 },
                    new PageContentEssence {Header = "Header8", Url = "Url8", IsPublished=false, SeoID=8, HtmlContentID = 8 }
                };

            pageContentEssence.ForEach(c => context.PageContentEssences.Add(c));
            context.SaveChanges();

            var menu = new List<MenuEssence> {
                new MenuEssence {Code="Header_menu1", TitleMenu="Title_menu1"},
                new MenuEssence {Code="Header_menu2", TitleMenu="Title_menu2"},
                new MenuEssence {Code="Header_menu3", TitleMenu="Title_menu3"},
                new MenuEssence {Code="Header_menu4", TitleMenu="Title_menu4"},
                new MenuEssence {Code="Header_menu5", TitleMenu="Title_menu5"},
                new MenuEssence {Code="Header_menu6", TitleMenu="Title_menu6"},
                new MenuEssence {Code="Header_menu7", TitleMenu="Title_menu7"},
                new MenuEssence {Code="Header_menu8", TitleMenu="Title_menu8"}
            };

            menu.ForEach(c => context.MenuEssences.Add(c));
            context.SaveChanges();

            var menuItem = new List<MenuItemEssence> {
                new MenuItemEssence{MenuID =1, PageID = 1, TitleMenuItem="Title_menuItem1", Url="Url_menuItem1"},
                new MenuItemEssence{MenuID =2, PageID = 2, TitleMenuItem="Title_menuItem2", Url="Url_menuItem2"},
                new MenuItemEssence{MenuID =3, PageID = 3, TitleMenuItem="Title_menuItem3", Url="Url_menuItem3"},
                new MenuItemEssence{MenuID =4, PageID = 4, TitleMenuItem="Title_menuItem4", Url="Url_menuItem4"},
                new MenuItemEssence{MenuID =5, PageID = 5, TitleMenuItem="Title_menuItem5", Url="Url_menuItem5"},
                new MenuItemEssence{MenuID =6, PageID = 6, TitleMenuItem="Title_menuItem6", Url="Url_menuItem6"},
                new MenuItemEssence{MenuID =7, PageID = 7, TitleMenuItem="Title_menuItem7", Url="Url_menuItem7"},
                new MenuItemEssence{MenuID =8, PageID = 8, TitleMenuItem="Title_menuItem8", Url="Url_menuItem8"}
            };

            menuItem.ForEach(c => context.MenuItemEssences.Add(c));
            context.SaveChanges();
        }
    }
}