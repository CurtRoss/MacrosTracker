﻿using MacrosTracker.Data;
using MacrosTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MacrosTracker.Categories.MealCategory;

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
                    CreatedUtc = DateTimeOffset.Now.Date
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
                                Category = e.Category,
                                CreatedUtc = e.CreatedUtc
                            });
                return query.ToArray();
            }
        }

        public IEnumerable<MealListItem> GetMealsByCategory(TypeofMealCategory category)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .DailyMeals
                        .Where(e => e.UserId == _userId && e.Category == category)
                        .Select(
                        e =>
                            new MealListItem
                            {
                                MealId = e.MealId,
                                MealName = e.MealName,
                                Category = e.Category,
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

                var foodMealList = GetFoodMealsByMealId(id);
                foreach(FoodMealListItem foodMeal in foodMealList)
                {
                    entity.ListOfFoodIds.Add(foodMeal.FoodId);
                }

                var foodList = new List<string>();
                foreach (int i in entity.ListOfFoodIds)
                {
                    foodList.Add(ctx.FoodItems.Find(i).FoodName);
                }

                return
                    new MealDetail
                    {
                        MealId = entity.MealId,
                        MealName = entity.MealName,
                        Category = entity.Category,
                        Protein = entity.Protein,
                        Fat = entity.Fat,
                        Carbs = entity.Carbs,
                        Calories = entity.Calories,
                        CreatedUtc = entity.CreatedUtc,
                        ModifiedUtc = entity.ModifiedUtc,
                        ListOfFoodNames = foodList
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
                    ctx.FoodMeals.Remove(item);
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
                entity.ModifiedUtc = DateTimeOffset.UtcNow.Date;

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

        public IEnumerable<FoodMealListItem> GetFoodMealsByMealId(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .FoodMeals
                        .Where(e => e.Meal.UserId == _userId && id == e.MealId)
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
