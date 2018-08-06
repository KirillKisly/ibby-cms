using AutoMapper;
using ibby_cms.Common.Abstract;
using ibby_cms.Common.Abstract.Interfaces;
using ibby_cms.Common.Models;
using ibby_cms.Entities.DAL;
using ibby_cms.Entities.Entitites;
using System.Collections.Generic;
using System.Data.Entity;

namespace ibby_cms.Common {
    public class MenuManager : IMenuManager {
        public void Delete(int id) {
            using (EntitiesContext context = new EntitiesContext()) {
                var item = context.Menus.Find(id);

                if (item != null) {
                    context.Menus.Remove(item);

                    context.SaveChanges();
                }
            }
        }

        public void EditMenu(MenuModel menuModel) {
            if (menuModel == null) {
                throw new ValidationException("Меню отсутствует", "");
            }

            var menu = new Menu {
                Id = menuModel.Id,
                Code = menuModel.Code,
                Title = menuModel.Title
            };

            using (EntitiesContext context = new EntitiesContext()) {
                context.Entry(menu).State = EntityState.Modified;

                context.SaveChanges();
            }
        }

        public MenuModel Get(int id) {
            using (EntitiesContext context = new EntitiesContext()) {
                var item = context.Menus.Find(id);

                if (item == null) {
                    throw new ValidationException("Меню не найдено", "");
                }

                var menuModel = new MenuModel {
                  Id = item.Id,
                  Code = item.Code,
                  Title = item.Title
                };

                return menuModel;
            }
        }

        public IEnumerable<MenuModel> GetAll() {
            using (EntitiesContext context = new EntitiesContext()) {
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Menu, MenuModel>()).CreateMapper();
                return mapper.Map<IEnumerable<Menu>, List<MenuModel>>(context.Menus);
            }
        }

        public void SaveMenu(MenuModel menuModel) {
            if (menuModel == null) {
                throw new ValidationException("Меню отсутствует", "");
            }

            var menu = new Menu {
                Code = menuModel.Code,
                Title = menuModel.Title
            };

            using (EntitiesContext context = new EntitiesContext()) {
                context.Menus.Add(menu);

                context.SaveChanges();
            }
        }
    }
}