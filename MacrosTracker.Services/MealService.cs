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
                    //ListOfFoods = model.ListOfFoods,
                    CreatedUtc = DateTimeOffset.Now
                };
            using(var ctx = new ApplicationDbContext())
            {
                ctx.DailyMeals.Add(entity);
                return ctx.SaveChanges() == 1;
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
    }
}
