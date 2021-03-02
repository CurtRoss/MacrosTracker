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
                                DayCarbs = e.TotalCarbs,
                                DayFats = e.TotalFats,
                                DayProteins = e.TotalFats,
                                DayCalories = e.TotalCalories
                            });
                return query.ToArray();
            }
        }
    }
}
