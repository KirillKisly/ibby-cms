using System.ComponentModel.DataAnnotations;

namespace ibby_cms.Models {
    public class MenuItemViewModel {
        public int Id { get; set; }
        [Required(ErrorMessage = "Это поле обязательно для заполнения")]
        public int MenuId { get; set; }
        public string Url { get; set; }
        public int? PageId { get; set; }

        [Display(Name = "Заголовок элемента меню")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 100 символов")]
        public string TitleMenuItem { get; set; }

        [Required(ErrorMessage = "Это поле обязательно для заполнения")]
        public string Code { get; set; }

        [Display(Name = "Заголовок меню")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 100 символов")]
        public string TitleMenu { get; set; }
    }
}