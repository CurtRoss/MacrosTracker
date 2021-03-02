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
        public Guid UserId { get; set; }
        public virtual List<JournalEntry> JournalEntries { get; set; }

        //public virtual List<FoodItem> FoodItems { get; set; }
        //public virtual List<Meal> Meals { get; set; }
        public double TotalFats
        {
            get
            {
                var fats = JournalEntries.Sum(e => e.Fats);
                return fats;
            }
        }
        public double TotalCarbs
        {
            get
            {
                var carbs = JournalEntries.Sum(e => e.Carbs);
                return carbs;
            }
        }

        public double TotalProteins
        {
            get
            {
                var protein = JournalEntries.Sum(e => e.Proteins);
                return protein;
            }
        }
        public double TotalCalories
        {
            get
            {
                var calories = JournalEntries.Sum(e => e.Calories);
                return calories;
            }
        }
    }
}
