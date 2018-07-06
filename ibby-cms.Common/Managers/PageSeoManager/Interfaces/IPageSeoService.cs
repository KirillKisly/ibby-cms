using ibby_cms.Common.Managers.PageSeoManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ibby_cms.Common.Managers.PageSeoManager.Interfaces
{
    public interface IPageSeoService
    {
        void MakePageSeo(PageSeoModel pageSeoModel);
        //PageContentModel GetPage(int? id);
        //PageSeoModel GetSeo(int? id);
        //IEnumerable<PageContentModel> GetPages();
        void Dispose();
    }
}
