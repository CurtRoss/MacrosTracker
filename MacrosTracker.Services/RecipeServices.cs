using MacrosTracker.Data;
using MacrosTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacrosTracker.Services
{
    public class RecipeServices
    {
        private readonly Guid _userId;

        public RecipeServices(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateRecipe(RecipeCreate model)
        {
            var entity =
               new Recipe()
               {
                   UserId = _userId,
                   RecipeName = model.RecipeName,
                   ListOfFoodIds = model.ListOfFoodIds,
                   CreatedUtc = DateTimeOffset.Now,
                   HowManyPortions = model.HowManyPortionsDoesRecipeMake
               };

            using (var ctx = new ApplicationDbContext())
            {
                foreach (int i in entity.ListOfFoodIds)
                {
                    entity.ListOfFoods.Add(ctx.FoodItems.Find(i));
                }
                ctx.Recipes.Add(entity);
                return ctx.SaveChanges() > 0;
            }
        }

        //Get
        public IEnumerable<RecipeListItem> GetRecipe()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Recipes
                        .Where(e => e.UserId == _userId)
                        .Select(
                        e =>
                            new RecipeListItem
                            {
                                RecipeId = e.RecipeId,
                                RecipeName = e.RecipeName,
                                Portions = e.HowManyPortions,
                                CreatedUtc = e.CreatedUtc
                            });
                return query.ToArray();
            }
        }

        public RecipeDetail GetRecipeById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Recipes
                        .Single(e => e.RecipeId == id && e.UserId == _userId);


                //Build out list of Foods to display in recipedetail.
                var foodNameList = new List<string>();
                var foodItemList = new List<FoodItem>();


                foreach (int i in entity.ListOfFoodIds)
                {
                    foodNameList.Add(ctx.FoodItems.Find(i).FoodName);
                    entity.ListOfFoods.Add(ctx.FoodItems.Find(i));
                }



                return
                    new RecipeDetail
                    {
                        RecipeId = entity.RecipeId,
                        RecipeName = entity.RecipeName,
                        PortionsMade = entity.HowManyPortions,
                        Protein = entity.Protein,
                        Fat = entity.Fat,
                        Carbs = entity.Carbs,
                        Calories = entity.Calories,
                        ProteinPerServing = entity.Protein/entity.HowManyPortions,
                        CarbsPerServing = entity.Carbs/entity.HowManyPortions,
                        FatsPerServing = entity.Fat/entity.HowManyPortions,
                        CaloriesPerServing = entity.Calories/entity.HowManyPortions,
                        CreatedUtc = entity.CreatedUtc,
                        ListOfFoodNames = foodNameList
                    };
            }
        }

        public bool UpdateRecipe(RecipeEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Recipes
                        .Single(e => e.RecipeId == model.RecipeId && e.UserId == _userId);

                entity.RecipeName = model.RecipeName;
                entity.HowManyPortions = model.HowManyPortionsDoesItMake;
                entity.ListOfFoodIds = model.ListOfFoodIds;

                return ctx.SaveChanges() > 0;
            }
        }

        public bool DeleteRecipe(int recipeId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Recipes
                        .Single(e => e.RecipeId == recipeId && e.UserId == _userId);

                ctx.Recipes.Remove(entity);

                return ctx.SaveChanges() > 0;
            }
        }
    }
}
