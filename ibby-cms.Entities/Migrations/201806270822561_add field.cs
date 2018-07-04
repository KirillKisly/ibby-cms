namespace ibby_cms.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addfield : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PageSeoEssence", "KeyWords", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PageSeoEssence", "KeyWords");
        }
    }
}
