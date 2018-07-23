using ibby_cms.Common.Models;
using ibby_cms.Entities.Entitites;
using System.Collections.Generic;

namespace ibby_cms.Common.Abstract.Interfaces {
    public interface IPageContentEssenceManager {
        IEnumerable<PageContentModel> GetAll();
        PageContentModel Get(int id);
        void Delete(int id);
        void CreatePage(PageModel pageModel);
        void EditPage(PageModel pageModel);
        void PublishPage(int? id);
        PageContentModel FindUrl(string url);
    }
}
