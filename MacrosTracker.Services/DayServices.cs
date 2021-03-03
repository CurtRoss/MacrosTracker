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
                                DayCalories = e.JournalEntries.Sum(x=>x.Calories)
                            }) ;
                return query.ToArray();
            }
        }

        public DayDetail GetDayById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
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
                        DayProteins = entity.JournalEntries.Sum(x => x.Proteins)
                    };
            }
        }
    }
}
