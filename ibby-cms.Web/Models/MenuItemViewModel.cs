using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ibby_cms.Models {
    public class MenuItemViewModel {
        public int Id { get; set; }
        public int? PageID { get; set; }
        public int MenuID { get; set; }

        [Display(Name = "Заголовок")]
        [Required(ErrorMessage = "Это поле обязательно для заполнения")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 30 символов")]
        public string TitleMenuItem { get; set; }

        [Display(Name = "URL")]
        [Required(ErrorMessage = "Выберите страницу из системы или укажите внешний URL")]
        public string Url { get; set; }

        [Display(Name ="Приоритет")]
        [Required(ErrorMessage ="Задайте приоритет отображения элемента меню")]
        public int Weight { get; set; }

        //public virtual Common.Models.PageContentModel Page { get; set; }
    }
}