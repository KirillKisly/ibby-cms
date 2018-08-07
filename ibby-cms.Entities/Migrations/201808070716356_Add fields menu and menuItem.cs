namespace ibby_cms.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddfieldsmenuandmenuItem : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MenuItem",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MenuID = c.Int(),
                        Url = c.String(),
                        PageID = c.Int(),
                        Title = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Menu", t => t.MenuID)
                .ForeignKey("dbo.PageContentEssence", t => t.PageID)
                .Index(t => t.MenuID)
                .Index(t => t.PageID);
            
            CreateTable(
                "dbo.Menu",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(),
                        Title = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MenuItem", "PageID", "dbo.PageContentEssence");
            DropForeignKey("dbo.MenuItem", "MenuID", "dbo.Menu");
            DropIndex("dbo.MenuItem", new[] { "PageID" });
            DropIndex("dbo.MenuItem", new[] { "MenuID" });
            DropTable("dbo.Menu");
            DropTable("dbo.MenuItem");
        }
    }
}
