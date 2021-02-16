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
        private readonly Guid foodId;

        public FoodItemServices(Guid userId)
        {
            foodId = userId;
        }

        public bool CreateFoodItem(FoodItemCreate model)
        {
            var entity =
                 new FoodItem()
                 {
                     FoodId= foodId,

                     FoodName = model.FoodName,
                     CreatedUtc = DateTimeOffset.Now
                 };

            using (var ctx = new ApplicationDbContext())
            {
                var post = ctx.Posts.Find(entity.PostId);
                post.ListOfFoodItem.Add(entity);
                ctx.FoodItem.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<FoodItemListItem> GetComments()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                   ctx
                       .FoodItem
                       .Where(e => e.OwnerId == foodId)
                       .Select(
                           e =>
                               new FoodItemListItem
                               {
                                   FoodId = e.FoodId,
                                   FoodName = e.FoodName,
                                   CreatedUtc = e.CreateUtc,
                                   ModifiedUtc = e.ModifiedUtc,
                               }
                   );
                return query.ToArray();
            }
        }

        public FoodItemDetail GetCommentById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .FoodItem
                        .Single(e => e.Id == id && e.OwnerId == foodId);
                return
                    new FoodItemDetail
                    {
                        FoodId = entity.FoodId,
                        FoodName = entity.FoodName,
                        CreatedUtc = entity.CreatedUtc,
                        ModifiedUtc = entity.ModifiedUtc,
                        ListOfMeals = entity.ListOfMeals
                    };
            }


        }

        public bool UpdateFoodItem(FoodItemEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .FoodItem
                        .Single(e => e.FoodId == model.FoodId && e.OwnerId == foodId);

                entity.FoodName = model.FoodName;
                entity.FoodId = model.FoodId;
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
                        .FoodItem
                        .Single(e => e.Id == Id && e.OwnerId == foodId);

                ctx.FoodItem.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }


    }
}