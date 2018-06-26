using ibby_cms.Common.Manager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ibby_cms.Common.Manager.Interfaces
{
    public interface IPageContentService
    {
        void MakePageContent(PageContentModel pageContentModel);
        IEnumerable<PageContentModel> GetPages();
        PageContentModel GetPage(int? id);
        PageSeoModel GetSeo(int? id);
        void Dispose();
    }
}
