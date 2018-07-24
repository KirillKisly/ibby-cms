using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ibby_cms.Models
{
    public class PageContentViewModel
    {
        public int Id { get; set; }
        public int? SeoID { get; set; }

        [Required]
        [Display(Name ="Заголовок")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 50 символов")]
        public string Header { get; set; }

        [Required(ErrorMessage ="Добавьте описание")]
        [Display(Name = "Содержание")]
        public string Content { get; set; }

        [Display(Name ="URL")]
        public string Url { get; set; }
        public bool IsPublished { get; set; }
        public string Title { get; set; }
        public string KeyWords { get; set; }
        public string Descriptions { get; set; }
    }
}