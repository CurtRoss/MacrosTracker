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
                     Amount = model.Amount,
                     FoodName = model.FoodName,
                     Calories = model.Calories,
                     Protein = model.Protein,
                     Fat = model.Fat,
                     Carbs = model.Carbs,
                     CreatedUtc = DateTimeOffset.Now
                 };


            using (var ctx = new ApplicationDbContext())
            {
                //Add food to users list of foods
                var user = ctx.Users.Find(entity.UserId);
                user.ListOfFoods.Add(entity);

                ctx.FoodItems.Add(entity); 
                return ctx.SaveChanges() == 1;
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
                return
                    new FoodItemDetail
                    {
                        FoodId = entity.FoodId,
                        FoodName = entity.FoodName,
                        Calories = entity.Calories,
                        Amount = entity.Amount,
                        Protein = entity.Protein,
                        Fat = entity.Fat,
                        Carbs = entity.Carbs,
                        CreatedUtc = entity.CreatedUtc,
                        ModifiedUtc = entity.ModifiedUtc,
                        //ListOfMeals = entity.ListOfMeals
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
                entity.Amount = model.Amount;
                entity.Calories = model.Calories;
                entity.Carbs = model.Carbs;
                entity.Protein = model.Protein;
                entity.Fat = model.Fat;
                entity.ModifiedUtc = DateTimeOffset.UtcNow;

                return ctx.SaveChanges() == 1;
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

                ctx.FoodItems.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }


    }
}