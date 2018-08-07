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
                var item = context.MenuEssences.Find(id);

                if (item != null) {
                    context.MenuEssences.Remove(item);

                    context.SaveChanges();
                }
            }
        }

        public void EditMenu(MenuModel menuModel) {
            if (menuModel == null) {
                throw new ValidationException("Меню отсутствует", "");
            }

            var menu = new MenuEssence {
                Id = menuModel.Id,
                Code = menuModel.Code,
                TitleMenu = menuModel.TitleMenu
            };

            using (EntitiesContext context = new EntitiesContext()) {
                context.Entry(menu).State = EntityState.Modified;

                context.SaveChanges();
            }
        }

        public MenuModel Get(int id) {
            using (EntitiesContext context = new EntitiesContext()) {
                var item = context.MenuEssences.Find(id);

                if (item == null) {
                    throw new ValidationException("Меню не найдено", "");
                }

                var menuModel = new MenuModel {
                  Id = item.Id,
                  Code = item.Code,
                  TitleMenu = item.TitleMenu
                };

                return menuModel;
            }
        }

        public IEnumerable<MenuModel> GetAll() {
            using (EntitiesContext context = new EntitiesContext()) {
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<MenuEssence, MenuModel>()).CreateMapper();
                return mapper.Map<IEnumerable<MenuEssence>, List<MenuModel>>(context.MenuEssences);
            }
        }

        public void SaveMenu(MenuModel menuModel) {
            if (menuModel == null) {
                throw new ValidationException("Меню отсутствует", "");
            }

            var menu = new MenuEssence {
                Code = menuModel.Code,
                TitleMenu = menuModel.TitleMenu
            };

            using (EntitiesContext context = new EntitiesContext()) {
                context.MenuEssences.Add(menu);

                context.SaveChanges();
            }
        }
    }
}