namespace ibby_cms.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Cascadedelete : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.MenuItemEssence", "MenuID", "dbo.MenuEssence");
            AddForeignKey("dbo.MenuItemEssence", "MenuID", "dbo.MenuEssence", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MenuItemEssence", "MenuID", "dbo.MenuEssence");
            AddForeignKey("dbo.MenuItemEssence", "MenuID", "dbo.MenuEssence", "Id");
        }
    }
}
