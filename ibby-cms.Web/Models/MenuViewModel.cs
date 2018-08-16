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

       


        //public int? PageID { get; set; }

        //[Required(ErrorMessage = "Это поле обязательно для заполнения")]
        //public int MenuID { get; set; }

        //[Display(Name = "URL")]
        //[Required(ErrorMessage = "Выберите страницу из системы или укажите внешний URL")]
        //public string Url { get; set; }

        //public string TitlePage { get; set; }

        //[Display(Name = "Заголовок")]
        //[StringLength(30, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 30 символов")]
        //public string TitleMenuItem { get; set; }



        public virtual ICollection<MenuItemViewModel> menuItems { get; set; }
        public virtual Common.Models.PageContentModel Page { get; set; }
    }
}