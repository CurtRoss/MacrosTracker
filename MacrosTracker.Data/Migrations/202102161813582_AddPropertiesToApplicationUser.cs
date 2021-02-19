namespace MacrosTracker.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPropertiesToApplicationUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ApplicationUser", "DateOfBirth", c => c.DateTime(nullable: false));
            DropColumn("dbo.ApplicationUser", "Age");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ApplicationUser", "Age", c => c.Int(nullable: false));
            DropColumn("dbo.ApplicationUser", "DateOfBirth");
        }
    }
}
