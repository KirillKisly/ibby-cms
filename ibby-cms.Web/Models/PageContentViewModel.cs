using System.ComponentModel.DataAnnotations;

namespace ibby_cms.Models
{
    public class PageContentViewModel
    {
        public int Id { get; set; }
        public int? SeoID { get; set; }
        public int? HtmlContentID { get; set; }

        [Required(ErrorMessage = "Это поле обязательно для заполнения")]
        [Display(Name ="Заголовок")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 100 символов")]
        public string Header { get; set; }

        [Display(Name ="URL")]
        public string Url { get; set; }

        public bool IsPublished { get; set; }

        [Display(Name = "Название (Title)")]
        public string Title { get; set; }

        [Display(Name = "Ключевые слова")]
        public string KeyWords { get; set; }

        [Display(Name = "Описание")]
        public string Descriptions { get; set; }

        [Required(ErrorMessage = "Добавьте содержание")]
        [Display(Name = "Содержание")]
        public string HtmlContent { get; set; }

        public string UniqueCode { get; set; }
    }
}