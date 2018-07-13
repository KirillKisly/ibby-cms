namespace ibby_cms.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteRequiredfield : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PageContentEssence", "Header", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PageContentEssence", "Header", c => c.String(nullable: false));
        }
    }
}
