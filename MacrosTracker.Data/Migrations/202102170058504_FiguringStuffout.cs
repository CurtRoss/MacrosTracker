namespace MacrosTracker.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FiguringStuffout : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ApplicationUser", "Weight", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ApplicationUser", "Weight", c => c.Int(nullable: false));
        }
    }
}
