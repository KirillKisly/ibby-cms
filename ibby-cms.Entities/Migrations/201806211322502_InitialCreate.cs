namespace ibby_cms.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PageContentEssence",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        HtmlContent = c.String(),
                        Content = c.String(),
                        Url = c.String(),
                        Header = c.String(),
                        SeoID = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PageSeoEssence", t => t.SeoID)
                .Index(t => t.SeoID);
            
            CreateTable(
                "dbo.PageSeoEssence",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Descriptions = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PageContentEssence", "SeoID", "dbo.PageSeoEssence");
            DropIndex("dbo.PageContentEssence", new[] { "SeoID" });
            DropTable("dbo.PageSeoEssence");
            DropTable("dbo.PageContentEssence");
        }
    }
}
