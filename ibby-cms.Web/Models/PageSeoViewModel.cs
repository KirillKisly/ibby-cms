using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ibby_cms.Models
{
    public class PageSeoViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string KeyWords { get; set; }
        public string Descriptions { get; set; }
    }
}