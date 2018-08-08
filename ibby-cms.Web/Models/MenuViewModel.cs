using System.ComponentModel.DataAnnotations;

namespace ibby_cms.Models {
    public class MenuViewModel {
        public int Id { get; set; }

        [Required(ErrorMessage = "Это поле обязательно для заполнения")]
        public int MenuID { get; set; }

        [Display(Name = "URL")]
        public string Url { get; set; }

        public int? PageID { get; set; }

        [Display(Name = "Заголовок элемента меню")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 100 символов")]
        public string TitleMenuItem { get; set; }

        [Required(ErrorMessage = "Это поле обязательно для заполнения")]
        [Display(Name = "Код меню")]
        public string Code { get; set; }

        [Display(Name = "Заголовок меню")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 100 символов")]
        public string TitleMenu { get; set; }
    }
}