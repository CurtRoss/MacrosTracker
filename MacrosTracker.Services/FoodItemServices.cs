using MacrosTracker.Data;
using MacrosTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacrosTracker.Services
{
    public class FoodItemServices
    {
        private readonly Guid _userId;

        public FoodItemServices(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateFoodItem(FoodItemCreate model)
        {
            var entity =
                 new FoodItem()
                 {

                     UserId = _userId,
                     FoodName = model.FoodName,
                     Protein = model.Protein,
                     Fat = model.Fat,
                     Carbs = model.Carbs,
                     CreatedUtc = DateTimeOffset.Now
                 };


            using (var ctx = new ApplicationDbContext())
            {
                var user = ctx.Users.Find(entity.UserId.ToString());
                user.ListOfFoods.Add(entity);

                ctx.FoodItems.Add(entity); 
                return ctx.SaveChanges() > 0;
            }
        }

        public IEnumerable<FoodItemListItem> GetFoodItem()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                   ctx
                       .FoodItems
                       .Where(e => e.UserId == _userId)
                       .Select(
                           e =>
                               new FoodItemListItem
                               {
                                   FoodId = e.FoodId,
                                   FoodName = e.FoodName,
                                   CreatedUtc = e.CreatedUtc,
                                   ModifiedUtc = e.ModifiedUtc,
                               }
                   );

                return query.ToArray();
            }
        }

        public FoodItemDetail GetFoodItemById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .FoodItems
                        .Single(e => e.FoodId == id && e.UserId == _userId);

                List<int> meals = entity.ListOfMealIds;
                foreach (int i in entity.ListOfMealIds)
                {
                    var foodMealEntity =
                        new FoodMeal()
                        {
                            FoodId = entity.FoodId,
                            MealId = i
                        };
                    ctx.FoodMeals.Find(foodMealEntity);
                }
                var foodMealList = GetFoodMealsByFoodId(id);
                foreach(FoodMealListItem foodMeal in foodMealList)
                {
                    entity.ListOfMealIds.Add(foodMeal.MealId);
                }

                var mealList = new List<string>();
                foreach(int i in entity.ListOfMealIds)
                {
                    mealList.Add(ctx.DailyMeals.Find(i).MealName);
                }

                return
                    new FoodItemDetail
                    {
                        FoodId = entity.FoodId,
                        FoodName = entity.FoodName,
                        Calories = entity.Calories,
                        Protein = entity.Protein,
                        Fat = entity.Fat,
                        Carbs = entity.Carbs,
                        CreatedUtc = entity.CreatedUtc.Date,
                        ModifiedUtc = entity.ModifiedUtc,
                        ListOfMealNames = mealList
                    };
            }
        }

        public bool UpdateFoodItem(FoodItemEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .FoodItems
                        .Single(e => e.FoodId == model.FoodId && e.UserId == _userId);

                entity.FoodName = model.FoodName;
                entity.FoodId = model.FoodId;
                entity.Carbs = model.Carbs;
                entity.Protein = model.Protein;
                entity.Fat = model.Fat;
                entity.ModifiedUtc = DateTimeOffset.UtcNow;
                return ctx.SaveChanges() > 0;
            }
        }

        public bool DeleteFoodItem(int Id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .FoodItems
                        .Single(e => e.FoodId == Id && e.UserId == _userId);

                var foodMealsToRemove = ctx.FoodMeals.Where(e => e.FoodId == entity.FoodId);
                foreach (FoodMeal item in foodMealsToRemove)
                {
                    ctx.FoodMeals.Remove(item);
                }

                ctx.FoodItems.Remove(entity);

                return ctx.SaveChanges() > 0;
            }
        }
        public IEnumerable<FoodMealListItem> GetFoodMealsByFoodId(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .FoodMeals
                        .Where(e => e.FoodItem.UserId == _userId && id == e.FoodId)
                        .Select(
                        e =>
                            new FoodMealListItem
                            {
                                MealId = e.MealId,
                                FoodId = e.FoodId,
                            });
                return query.ToArray();
            }
        }
    }
}