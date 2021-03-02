using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacrosTracker.Models.DayModels
{
    public class DayCreate
    {
        [Required]
        public DateTime Date { get; set; }
    }

    public class DayDetail
    {
        public int DayId { get; set; }
        public DateTime Date { get; set; }
        public double DayCarbs { get; set; }
       
        public double DayProteins { get; set; }
        public double DayFats { get; set; }
        public double DayCalories { get; set; }
    }
    public class SpecificDayDetail : DayDetail
    {
        public List<string> DayFoods { get; set; }
    }
}
