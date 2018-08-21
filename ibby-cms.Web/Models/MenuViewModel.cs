using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ibby_cms.Models {
    public class MenuViewModel {
        public int Id { get; set; }

        [Required(ErrorMessage = "Это поле обязательно для заполнения")]
        [Display(Name = "Заголовок меню")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 100 символов")]
        public string TitleMenu { get; set; }

        [Required(ErrorMessage = "Это поле обязательно для заполнения")]
        [Display(Name = "Код меню")]
        public string Code { get; set; }

        public virtual ICollection<MenuItemViewModel> MenuItems { get; set; }
        //public virtual Common.Models.PageContentModel Page { get; set; }
    }
}