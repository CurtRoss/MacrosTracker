namespace MacrosTracker.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CorrectedForeignKeys : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Meal", "FoodItem_FoodId", "dbo.FoodItem");
            DropForeignKey("dbo.FoodItem", "MealId", "dbo.Meal");
            DropForeignKey("dbo.Meal", "FoodId", "dbo.FoodItem");
            DropForeignKey("dbo.FoodItem", "Meal_MealId", "dbo.Meal");
            DropIndex("dbo.Meal", new[] { "FoodId" });
            DropIndex("dbo.Meal", new[] { "FoodItem_FoodId" });
            DropIndex("dbo.FoodItem", new[] { "MealId" });
            DropIndex("dbo.FoodItem", new[] { "Meal_MealId" });
            CreateTable(
                "dbo.FoodItemMeal",
                c => new
                    {
                        FoodItem_FoodId = c.Int(nullable: false),
                        Meal_MealId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.FoodItem_FoodId, t.Meal_MealId })
                .ForeignKey("dbo.FoodItem", t => t.FoodItem_FoodId, cascadeDelete: false)
                .ForeignKey("dbo.Meal", t => t.Meal_MealId, cascadeDelete: false)
                .Index(t => t.FoodItem_FoodId)
                .Index(t => t.Meal_MealId);
            
            DropColumn("dbo.Meal", "FoodId");
            DropColumn("dbo.Meal", "FoodItem_FoodId");
            DropColumn("dbo.FoodItem", "MealId");
            DropColumn("dbo.FoodItem", "Meal_MealId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.FoodItem", "Meal_MealId", c => c.Int());
            AddColumn("dbo.FoodItem", "MealId", c => c.Int(nullable: false));
            AddColumn("dbo.Meal", "FoodItem_FoodId", c => c.Int());
            AddColumn("dbo.Meal", "FoodId", c => c.Int(nullable: false));
            DropForeignKey("dbo.FoodItemMeal", "Meal_MealId", "dbo.Meal");
            DropForeignKey("dbo.FoodItemMeal", "FoodItem_FoodId", "dbo.FoodItem");
            DropIndex("dbo.FoodItemMeal", new[] { "Meal_MealId" });
            DropIndex("dbo.FoodItemMeal", new[] { "FoodItem_FoodId" });
            DropTable("dbo.FoodItemMeal");
            CreateIndex("dbo.FoodItem", "Meal_MealId");
            CreateIndex("dbo.FoodItem", "MealId");
            CreateIndex("dbo.Meal", "FoodItem_FoodId");
            CreateIndex("dbo.Meal", "FoodId");
            AddForeignKey("dbo.FoodItem", "Meal_MealId", "dbo.Meal", "MealId");
            AddForeignKey("dbo.Meal", "FoodId", "dbo.FoodItem", "FoodId", cascadeDelete: false);
            AddForeignKey("dbo.FoodItem", "MealId", "dbo.Meal", "MealId", cascadeDelete: false);
            AddForeignKey("dbo.Meal", "FoodItem_FoodId", "dbo.FoodItem", "FoodId");
        }
    }
}
