using ibby_cms.Common.Models;
using System.Collections.Generic;

namespace ibby_cms.Common.Abstract.Interfaces {
    public interface IHtmlContentEssenceManager {
        IEnumerable<HtmlContentModel> GetAll();
        HtmlContentModel Get(int id);
        int GetId(string uniqueCode);
        void Delete(int id);
        void SaveHtmlContent(HtmlContentModel htmlContentModel);
        void EditHtmlContent(HtmlContentModel htmlContentModel);
    }
}





        
