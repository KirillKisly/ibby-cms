using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ibby_cms.Common.Manager.Interfaces;
using ibby_cms.Common.Manager.Services;
using Ninject.Modules;

namespace ibby_cms
{
    public class PageContentModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IPageContentService>().To<PageContentService>();
        }
    }
}