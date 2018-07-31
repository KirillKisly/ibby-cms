namespace ibby_cms.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addPageHtmlContentEssence : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PageHtmlContentEssence",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        HtmlContent = c.String(),
                        UniqueCode = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.PageContentEssence", "HtmlContentID", c => c.Int());
            CreateIndex("dbo.PageContentEssence", "HtmlContentID");
            AddForeignKey("dbo.PageContentEssence", "HtmlContentID", "dbo.PageHtmlContentEssence", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PageContentEssence", "HtmlContentID", "dbo.PageHtmlContentEssence");
            DropIndex("dbo.PageContentEssence", new[] { "HtmlContentID" });
            DropColumn("dbo.PageContentEssence", "HtmlContentID");
            DropTable("dbo.PageHtmlContentEssence");
        }
    }
}
