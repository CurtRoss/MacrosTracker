using MacrosTracker.Data;
using MacrosTracker.Models.DayModels;
using MacrosTracker.Models.JournaleEntrymodels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacrosTracker.Services
{
    public class JournalServices
    {
        private readonly Guid _userId;

        public JournalServices(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateJournalEntry(JournalEntryCreate model)
        {

            var entity =
                new JournalEntry()
                {
                    UserId = _userId,
                    FoodList = model.FoodList,
                    MealList = model.MealList,
                    TimeStamp = model.JournalDate,

                };

            double carbs = 0;
            double protein = 0;
            double fats = 0;
            double calories = 0;

            if (model.FoodList != null)
            {
                foreach (int i in model.FoodList)
                {
                    using (var ctx = new ApplicationDbContext())
                    {
                        carbs += ctx.FoodItems.Find(i).Carbs;
                        protein += ctx.FoodItems.Find(i).Protein;
                        fats += ctx.FoodItems.Find(i).Fat;
                        calories += ctx.FoodItems.Find(i).Calories;
                    }
                }
            }

            if (model.MealList != null)
            {
                foreach (int i in model.MealList)
                {
                    using (var ctx = new ApplicationDbContext())
                    {
                        carbs += ctx.DailyMeals.Find(i).Carbs;
                        protein += ctx.DailyMeals.Find(i).Protein;
                        fats += ctx.DailyMeals.Find(i).Fat;
                        calories += ctx.DailyMeals.Find(i).Calories;
                    }
                }
            }

            entity.Carbs = carbs;
            entity.Proteins = protein;
            entity.Fats = fats;
            entity.Calories = calories;

            //find Days where e.journalDate = 
            using (var ctx = new ApplicationDbContext())
            {
                var dayEntity =
                    ctx
                        .Days
                        //.SingleOrDefault(e => e.DateOfEntry.Date == entity.TimeStamp.Date);
                        .Where(e => e.UserId.Equals(_userId))
                        .ToList()
                        .SingleOrDefault(e => e.DateOfEntry.Date == entity.TimeStamp.Date);


                //If there is no day object for the date of the journal entry, create a day and give the journal entries dayID the new DayID
                if (dayEntity == null)
                {
                    var newDayEntity =
                    new Day
                    {
                        DateOfEntry = model.JournalDate,
                        UserId = _userId
                    };

                    //save new day
                    ctx.Days.Add(newDayEntity);
                    var didItWork = ctx.SaveChanges();

                    //add the new dayID to our journal entry and add to the database.
                    if (didItWork > 0)
                    {
                        entity.DayId = newDayEntity.DayId;

                        //add journal entry to list of journal entries in day
                        
                        newDayEntity.JournalEntries.Add(entity);
                        ctx.JournalEntries.Add(entity);
                        return ctx.SaveChanges() > 0;
                    }
                }

                //If the day exists, make the journal entrys dayId the existing day ID
                entity.DayId = ctx.Days.Find(dayEntity.DayId).DayId;

                //add journal entry to Day's list of journal entries
                dayEntity.JournalEntries.Add(entity);

                ctx.JournalEntries.Add(entity);
                return ctx.SaveChanges() > 0;
            }
        }

        public IEnumerable<JournalEntryDetail> GetJournalEntries()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .JournalEntries
                        .Where(e => e.UserId == _userId)
                        .Select(
                        e =>
                            new JournalEntryDetail
                            {
                                JournalEntryId = e.JournalEntryId,
                                JournalDate = e.TimeStamp,
                                Calories = e.Calories,
                                Carbs = e.Carbs,
                                Fats = e.Fats,
                                Proteins = e.Proteins
                            });
                return query.ToArray();
            }
        }

        public JournalEntryDetail GetJournalEntryById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .JournalEntries
                        .Single(e => e.JournalEntryId == id && e.UserId == _userId);
                return
                    new JournalEntryDetail
                    {
                        JournalEntryId = entity.JournalEntryId,
                        JournalDate = entity.TimeStamp.Date,
                        Calories = entity.Calories,
                        Carbs = entity.Carbs,
                        Fats = entity.Fats,
                        Proteins = entity.Proteins
                    };
            }
        }

        public bool UpdateJournalEntry(JournalEntryEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .JournalEntries
                        .Single(e => e.JournalEntryId == model.JournalEntryId && e.UserId == _userId);

                entity.TimeStamp = model.JournalDate;
                entity.MealList = model.MealList;
                entity.FoodList = model.FoodList;

                double carbs = 0;
                double protein = 0;
                double fats = 0;
                double calories = 0;

                if (model.FoodList != null)
                {
                    foreach (int i in model.FoodList)
                    {
                        using (var ctx2 = new ApplicationDbContext())
                        {
                            carbs += ctx2.FoodItems.Find(i).Carbs;
                            protein += ctx2.FoodItems.Find(i).Protein;
                            fats += ctx2.FoodItems.Find(i).Fat;
                            calories += ctx2.FoodItems.Find(i).Calories;
                        }
                    }
                }

                if (model.MealList != null)
                {
                    foreach (int i in model.MealList)
                    {
                        using (var ctx2 = new ApplicationDbContext())
                        {
                            carbs += ctx2.DailyMeals.Find(i).Carbs;
                            protein += ctx2.DailyMeals.Find(i).Protein;
                            fats += ctx2.DailyMeals.Find(i).Fat;
                            calories += ctx2.DailyMeals.Find(i).Calories;
                        }
                    }
                }

                entity.Carbs = carbs;
                entity.Proteins = protein;
                entity.Fats = fats;
                entity.Calories = calories;


                var dayEntity =
                    ctx
                        .Days
                        .SingleOrDefault(e => e.DateOfEntry.Date == entity.TimeStamp.Date);


                //If there is no day object for the date of the journal entry, create a day and give the journal entries dayID the new DayID
                if (dayEntity == null)
                {
                    var newDayEntity =
                    new Day
                    {
                        DateOfEntry = model.JournalDate,
                        UserId = _userId
                    };

                    //save new day
                    ctx.Days.Add(newDayEntity);
                    var didItWork = ctx.SaveChanges();

                    //add the new dayID to our journal entry and add to the database.
                    if (didItWork > 0)
                    {
                        entity.DayId = newDayEntity.DayId;
                        ctx.JournalEntries.Add(entity);
                        return ctx.SaveChanges() > 0;
                    }
                }
                //If the day exists, make the journal entrys dayId the existing day ID
                entity.DayId = ctx.Days.Find(entity.DayId).DayId;
                ctx.JournalEntries.Add(entity);
                return ctx.SaveChanges() > 0;
            }
        }

        public bool DeleteJournalEntry(int journalId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .JournalEntries
                        .Single(e => e.JournalEntryId == journalId && e.UserId == _userId);
                ctx.JournalEntries.Remove(entity);
                return ctx.SaveChanges() > 0;
            }
        }
    }
}
