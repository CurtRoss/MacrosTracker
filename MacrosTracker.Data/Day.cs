using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacrosTracker.Data
{
    public class Day
    {
        [Key]
        public int DayId { get; set; }
        public DateTime DateOfEntry { get; set; }

        public double TotalCalories { get; set; }
        public double TotalFats { get; set; }
        public double TotalCarbs { get; set; }
        public double TotalProteins { get; set; }
        public virtual List<FoodItem> FoodItems { get; set; }
        public virtual List<Meal> Meals { get; set; }

    }
}
