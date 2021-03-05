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
                                DayCarbs = (int)e.JournalEntries.Sum(x=>x.Carbs),
                                DayFats = (int)e.JournalEntries.Sum(x=>x.Fats),
                                DayProteins = (int)e.JournalEntries.Sum(x=>x.Proteins),
                                DayCalories = (int)e.JournalEntries.Sum(x=>x.Calories),
                                PlusOrMinusCalories = (int)user.DailyCalorieGoalToLoseWeight - (int)e.JournalEntries.Sum(x => x.Calories),
                                PlusOrMinusCarbs = (int)user.CarbGoal - (int)e.JournalEntries.Sum(x => x.Carbs),
                                PlusOrMinusProteins = (int)user.ProteinGoal - (int)e.JournalEntries.Sum(x => x.Proteins),
                                PlusOrMinusFats = (int)user.FatGoal - (int)e.JournalEntries.Sum(x => x.Fats)
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
                        DayCalories = (int)entity.JournalEntries.Sum(x=>x.Calories),
                        DayCarbs = (int)entity.JournalEntries.Sum(x => x.Carbs),
                        DayFats = (int)entity.JournalEntries.Sum(x => x.Fats),
                        DayProteins = (int)entity.JournalEntries.Sum(x => x.Proteins),
                        PlusOrMinusCalories = (int)user.DailyCalorieGoalToLoseWeight - (int)entity.JournalEntries.Sum(x => x.Calories),
                        PlusOrMinusCarbs = (int)user.CarbGoal - (int)entity.JournalEntries.Sum(x => x.Carbs),
                        PlusOrMinusProteins = (int)user.ProteinGoal - (int)entity.JournalEntries.Sum(x => x.Proteins),
                        PlusOrMinusFats = (int)user.FatGoal - (int)entity.JournalEntries.Sum(x => x.Fats)
                    };
            }
        }
    }
}
