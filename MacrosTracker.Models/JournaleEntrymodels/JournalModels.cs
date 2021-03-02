using MacrosTracker.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacrosTracker.Models.JournaleEntrymodels
{
    public class JournalEntryCreate
    {
        [Required]
        public DateTime JournalDate { get; set; }
        public List<int> MealList { get; set; }
        public List<int> FoodList { get; set; }
    }

    public class JournalEntryDetail
    {
        public int JournalEntryId { get; set; }
        public DateTime JournalDate { get; set; }
        public Meal Meal { get; set; }
        public FoodItem FoodItem { get; set; }
        public List<string> FoodNames { get; set; }
        public double Calories { get; set; }
        public double Proteins { get; set; }
        public double Carbs { get; set; }
        public double Fats { get; set; }
    }

    public class JournalEntryEdit
    {
        public int JournalEntryId { get; set; }
        public DateTime JournalDate { get; set; }
        public List<int> MealList { get; set; } = new List<int>();
        public List<int> FoodList { get; set; } = new List<int>();
    }

}
