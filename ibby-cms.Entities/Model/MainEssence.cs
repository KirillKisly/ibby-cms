using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ibby_cms.Entities.Entitites
{
    public class MainEssence
    {
        public int Id { get; set; }
        public string HtmlContent { get; set; }
        public string Content { get; set; }
        public string Url { get; set; }
        public string Header { get; set; }
    }
}