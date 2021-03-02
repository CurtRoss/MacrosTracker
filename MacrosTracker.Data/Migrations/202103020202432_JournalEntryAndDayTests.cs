namespace MacrosTracker.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class JournalEntryAndDayTests : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.JournalEntry", "FoodItem_FoodId", "dbo.FoodItem");
            DropForeignKey("dbo.JournalEntry", "Meal_MealId", "dbo.Meal");
            DropIndex("dbo.JournalEntry", new[] { "FoodItem_FoodId" });
            DropIndex("dbo.JournalEntry", new[] { "Meal_MealId" });
            CreateTable(
                "dbo.Recipe",
                c => new
                    {
                        RecipeId = c.Int(nullable: false, identity: true),
                        UserId = c.Guid(nullable: false),
                        RecipeName = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.RecipeId);
            
            AddColumn("dbo.FoodItem", "Recipe_RecipeId", c => c.Int());
            AddColumn("dbo.Day", "UserId", c => c.Guid(nullable: false));
            AddColumn("dbo.JournalEntry", "Calories", c => c.Double(nullable: false));
            AddColumn("dbo.JournalEntry", "Carbs", c => c.Double(nullable: false));
            AddColumn("dbo.JournalEntry", "Proteins", c => c.Double(nullable: false));
            AddColumn("dbo.JournalEntry", "Fats", c => c.Double(nullable: false));
            CreateIndex("dbo.FoodItem", "Recipe_RecipeId");
            AddForeignKey("dbo.FoodItem", "Recipe_RecipeId", "dbo.Recipe", "RecipeId");
            DropColumn("dbo.Day", "TotalCalories");
            DropColumn("dbo.Day", "TotalFats");
            DropColumn("dbo.Day", "TotalCarbs");
            DropColumn("dbo.Day", "TotalProteins");
            DropColumn("dbo.JournalEntry", "FoodItem_FoodId");
            DropColumn("dbo.JournalEntry", "Meal_MealId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.JournalEntry", "Meal_MealId", c => c.Int());
            AddColumn("dbo.JournalEntry", "FoodItem_FoodId", c => c.Int());
            AddColumn("dbo.Day", "TotalProteins", c => c.Int(nullable: false));
            AddColumn("dbo.Day", "TotalCarbs", c => c.Int(nullable: false));
            AddColumn("dbo.Day", "TotalFats", c => c.Int(nullable: false));
            AddColumn("dbo.Day", "TotalCalories", c => c.Int(nullable: false));
            DropForeignKey("dbo.FoodItem", "Recipe_RecipeId", "dbo.Recipe");
            DropIndex("dbo.FoodItem", new[] { "Recipe_RecipeId" });
            DropColumn("dbo.JournalEntry", "Fats");
            DropColumn("dbo.JournalEntry", "Proteins");
            DropColumn("dbo.JournalEntry", "Carbs");
            DropColumn("dbo.JournalEntry", "Calories");
            DropColumn("dbo.Day", "UserId");
            DropColumn("dbo.FoodItem", "Recipe_RecipeId");
            DropTable("dbo.Recipe");
            CreateIndex("dbo.JournalEntry", "Meal_MealId");
            CreateIndex("dbo.JournalEntry", "FoodItem_FoodId");
            AddForeignKey("dbo.JournalEntry", "Meal_MealId", "dbo.Meal", "MealId");
            AddForeignKey("dbo.JournalEntry", "FoodItem_FoodId", "dbo.FoodItem", "FoodId");
        }
    }
}
