namespace ibby_cms.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Refreshfields : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MenuItemEssence",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MenuID = c.Int(),
                        Url = c.String(),
                        PageID = c.Int(),
                        TitleMenuItem = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MenuEssence", t => t.MenuID)
                .ForeignKey("dbo.PageContentEssence", t => t.PageID)
                .Index(t => t.MenuID)
                .Index(t => t.PageID);
            
            CreateTable(
                "dbo.MenuEssence",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(),
                        TitleMenu = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MenuItemEssence", "PageID", "dbo.PageContentEssence");
            DropForeignKey("dbo.MenuItemEssence", "MenuID", "dbo.MenuEssence");
            DropIndex("dbo.MenuItemEssence", new[] { "PageID" });
            DropIndex("dbo.MenuItemEssence", new[] { "MenuID" });
            DropTable("dbo.MenuEssence");
            DropTable("dbo.MenuItemEssence");
        }
    }
}
