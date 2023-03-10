
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace first.Models
{
    public class MenuModel
    {
        public MenuModel() => MenuList = new List<MenuModel>();
        [Key]
        public int ID { get; set; }
        public string MenuText { get; set; }
        public int? PerentID { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }

        [NotMapped]
        public List<MenuModel> MenuList { get; set; }

        public List<MenuModel> MenuTree(List<MenuModel> menuList, int? perentid)
        {
            return menuList.Where(x => x.PerentID == perentid).Select(
               x => new MenuModel
               {
                   ID = x.ID,
                   MenuText = x.MenuText,
                   PerentID = x.PerentID,
                   Controller = x.Controller,
                   Action = x.Action,
                   MenuList = MenuTree(menuList, x.ID)

               }).ToList();
        }

    }
}