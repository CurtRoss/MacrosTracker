namespace MacrosTracker.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FoodMealAdded : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Meal", "Protein");
            DropColumn("dbo.Meal", "Fat");
            DropColumn("dbo.Meal", "Carbs");
            DropColumn("dbo.Meal", "Calories");
            DropColumn("dbo.FoodItem", "Calories");
        }
        
        public override void Down()
        {
            AddColumn("dbo.FoodItem", "Calories", c => c.Double(nullable: false));
            AddColumn("dbo.Meal", "Calories", c => c.Int(nullable: false));
            AddColumn("dbo.Meal", "Carbs", c => c.Int(nullable: false));
            AddColumn("dbo.Meal", "Fat", c => c.Int(nullable: false));
            AddColumn("dbo.Meal", "Protein", c => c.Int(nullable: false));
        }
    }
}
