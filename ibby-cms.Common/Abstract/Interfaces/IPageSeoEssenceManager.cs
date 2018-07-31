using ibby_cms.Common.Models;
using ibby_cms.Entities.Entitites;
using System.Collections.Generic;

namespace ibby_cms.Common.Abstract.Interfaces {
    public interface IPageSeoEssenceManager {
        IEnumerable<PageSeoModel> GetAll();
        PageSeoModel Get(int id);
        //void Create(PageSeoEssence pageSeoEssence);
        //void Update(PageSeoEssence pageSeoEssence);
        void Delete(int id);

        void SaveSeo(PageSeoModel pageSeoModel);
        void EditSeo(PageSeoModel pageSeoModel);
    }
}
