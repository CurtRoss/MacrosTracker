using MacrosTracker.Data;
using MacrosTracker.Models.DayModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacrosTracker.Services
{
    public class DayServices
    {
        private readonly Guid _userId;

        public DayServices(Guid userId)
        {
            _userId = userId;
        }
        public IEnumerable<DayDetail> GetAllDays()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var user = ctx.Users.Find(_userId.ToString());
                var query =
                    ctx
                        .Days
                        .Where(e => e.UserId == _userId)
                        .Select(
                        e =>
                            new DayDetail
                            {
                                DayId = e.DayId,
                                Date = e.DateOfEntry,
                                DayCarbs = e.JournalEntries.Sum(x=>x.Carbs),
                                DayFats = e.JournalEntries.Sum(x=>x.Fats),
                                DayProteins = e.JournalEntries.Sum(x=>x.Proteins),
                                DayCalories = e.JournalEntries.Sum(x=>x.Calories),
                                PlusOrMinusCalories = user.DailyCalorieGoalToLoseWeight - e.JournalEntries.Sum(x => x.Calories),
                                PlusOrMinusCarbs = user.CarbGoal - e.JournalEntries.Sum(x => x.Carbs),
                                PlusOrMinusProteins = user.ProteinGoal - e.JournalEntries.Sum(x => x.Proteins),
                                PlusOrMinusFats = user.FatGoal - e.JournalEntries.Sum(x => x.Fats)
                            }) ;
                return query.ToArray();
            }
        }

        public DayDetail GetDayById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var user = ctx.Users.Find(_userId.ToString());
                var entity =
                    ctx
                        .Days
                        .Single(e => e.DayId == id && e.UserId == _userId);

                return
                    new DayDetail
                    {
                        DayId = entity.DayId,
                        Date = entity.DateOfEntry,
                        DayCalories = entity.JournalEntries.Sum(x=>x.Calories),
                        DayCarbs = entity.JournalEntries.Sum(x => x.Carbs),
                        DayFats = entity.JournalEntries.Sum(x => x.Fats),
                        DayProteins = entity.JournalEntries.Sum(x => x.Proteins),
                        PlusOrMinusCalories = user.DailyCalorieGoalToLoseWeight - entity.JournalEntries.Sum(x => x.Calories),
                        PlusOrMinusCarbs = user.CarbGoal - entity.JournalEntries.Sum(x => x.Carbs),
                        PlusOrMinusProteins = user.ProteinGoal - entity.JournalEntries.Sum(x => x.Proteins),
                        PlusOrMinusFats = user.FatGoal - entity.JournalEntries.Sum(x => x.Fats)
                    };
            }
        }
    }
}
