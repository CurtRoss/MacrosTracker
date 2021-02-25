using MacrosTracker.Data;
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

        public bool CreateJourneyEntry(JournalEntryCreate model)
        {
            var entity =
                new JournalEntry()
                {
                    UserId = _userId,
                    FoodItem = model.FoodItem,
                    Meal = model.Meal,
                    TimeStamp = model.JournalDate
                };
            using (var ctx = new ApplicationDbContext())
            {
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
                                JournalDate = e.TimeStamp.Date,
                                FoodItem = e.FoodItem,
                                Meal = e.Meal
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
                        FoodItem = entity.FoodItem,
                        Meal = entity.Meal
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
                entity.Meal = model.Meal;
                entity.FoodItem = model.FoodItem;

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
