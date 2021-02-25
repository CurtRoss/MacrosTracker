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
                foreach (int i in entity.ListOfFoodIds)
                {
                    var foodMealEntity =
                        new FoodMeal()
                        {
                            MealId = entity.MealId,
                            FoodId = i
                        };
                    ctx.FoodMeals.Add(foodMealEntity);
                    entity.ListOfFoods.Add(ctx.FoodItems.Find(i));
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

                var foodMealsToRemove = ctx.FoodMeals.Where(e => e.MealId == entity.MealId);
                foreach (FoodMeal item in foodMealsToRemove)
                {
                   // Console.WriteLine("Testing");
                   // Console.WriteLine(item);
                    ctx.FoodMeals.Remove(item);
                   //Console.WriteLine("Removed");
                }

                foreach (int i in model.ListOfFoodIds)
                {
                    var foodMealEntity =
                        new FoodMeal()
                        {
                            MealId = model.MealId,
                            FoodId = i
                        };
                    ctx.FoodMeals.Add(foodMealEntity);
                }

                entity.MealName = model.MealName;
                entity.Category = model.Category;
                entity.ListOfFoodIds = model.ListOfFoodIds;
                entity.ModifiedUtc = DateTimeOffset.UtcNow;

                return ctx.SaveChanges() > 0;
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

                var foodMealsToRemove = ctx.FoodMeals.Where(e => e.MealId == entity.MealId);
                foreach (FoodMeal item in foodMealsToRemove)
                {
                    ctx.FoodMeals.Remove(item);
                }

                ctx.DailyMeals.Remove(entity);

                return ctx.SaveChanges() > 0;
            }
        }

        //Helper Method
        public IEnumerable<FoodMealListItem> GetFoodMeals(MealEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .FoodMeals
                        .Where(e => e.Meal.UserId == _userId && model.MealId == e.MealId)
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
