using MacrosTracker.Data;
using MacrosTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacrosTracker.Services
{
    public class MealService
    {
        private readonly Guid _userId;

        public MealService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateMeal(MealCreate model)
        {
            var entity =
                new Meal()
                {
                    UserId = _userId,
                    MealName = model.MealName,
                    Category = model.Category,
                    ListOfFoodIds = model.ListOfFoodIds,
                    CreatedUtc = DateTimeOffset.Now
                };

            using (var ctx = new ApplicationDbContext())
            {
                //for each foodID in ListOfFoodIds, add that food to this meals list of foods.
                //foreach (int i in entity.ListOfFoodIds)
                //{
                //    var food = ctx.FoodItems.Find(i);
                //    entity.ListOfFoods.Add(food);
                //}


                //Add meal to users list of meals
                //var user = ctx.Users.Find(entity.UserId.ToString());
                //user.ListOfMeals.Add(entity);
                foreach (int i in entity.ListOfFoodIds)
                {
                    var foodMealEntity =
                        new FoodMeal()
                        {
                            MealId = entity.MealId,
                            FoodId = i
                        };
                    ctx.FoodMeals.Add(foodMealEntity);
                }

                ctx.DailyMeals.Add(entity);
                return ctx.SaveChanges() > 0;
            }
        }

        public IEnumerable<MealListItem> GetMeals()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .DailyMeals
                        .Where(e => e.UserId == _userId)
                        .Select(
                        e =>
                            new MealListItem
                            {
                                MealId = e.MealId,
                                MealName = e.MealName,
                                CreatedUtc = e.CreatedUtc
                            });
                return query.ToArray();
            }
        }

        public MealDetail GetMealById(int id)
        {
         

            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .DailyMeals
                        .Single(e => e.MealId == id && e.UserId == _userId);


                List<int> foods = entity.ListOfFoodIds;
                foreach (int i in entity.ListOfFoodIds)
                {
                    var foodMealEntity =
                        new FoodMeal()
                        {
                            MealId = entity.MealId,
                            FoodId = i
                        };
                    ctx.FoodMeals.Find(foodMealEntity);
                }

                //entity.ListOfFoods = new List<FoodItem>();
                //foreach (int i in entity.ListOfFoodIds)
                //{
                //    var foodMealEntity =
                //        new FoodMeal()
                //        {
                //            MealId = entity.MealId,
                //            FoodId = i
                //        };
                //    ctx.FoodMeals.Find(foodMealEntity);
                //}

                return
                    new MealDetail
                    {
                        //display list of foods for this meal

                        MealId = entity.MealId,
                        MealName = entity.MealName,
                        Category = entity.Category,
                        Protein = entity.Protein,
                        Fat = entity.Fat,
                        Carbs = entity.Carbs,
                        Calories = entity.Calories,
                        CreatedUtc = entity.CreatedUtc,
                        ModifiedUtc = entity.ModifiedUtc,
                        ListOfFoodIds = foods
                    };
            }
        }


        public bool UpdateMeal(MealEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .DailyMeals
                        .Single(e => e.MealId == model.MealId && e.UserId == _userId);

                entity.MealName = model.MealName;
                entity.Category = model.Category;
                //entity.Carbs = model.Carbs;
                //entity.Fat = model.Fat;
                //entity.Protein = model.Protein;
                //entity.Calories = model.Calories;
                entity.ModifiedUtc = DateTimeOffset.UtcNow;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteMeal(int mealId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .DailyMeals
                        .Single(e => e.MealId == mealId && e.UserId == _userId);

                ctx.DailyMeals.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
