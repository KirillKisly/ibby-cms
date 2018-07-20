using ibby_cms.Common.Models;
using ibby_cms.Entities.Entitites;
using System.Collections.Generic;

namespace ibby_cms.Common.Abstract.Interfaces {
    public interface IPageContentEssenceManager {
        IEnumerable<PageContentModel> GetAll();
        PageContentModel Get(int id);
        void Create(PageContentEssence pageContentEssence);
        void Update(PageContentEssence pageContentEssence);
        void Delete(int id);
        
        // new methods
        void CreatePage(PageModel pageModel);
        void EditPage(PageModel pageModel);

        void CreatePageContent(PageContentModel pageContentModel, PageSeoModel pageSeoModel);
        void EditPage(PageContentModel pageContentModel, PageSeoModel pageSeoModel);
        void DeletePage(int? id);
        void PublishPage(int? id);
        string GenerateUrl(string url);
        PageContentModel FindUrl(string url);
    }
}
