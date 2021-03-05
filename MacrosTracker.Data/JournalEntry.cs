using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacrosTracker.Data
{
    public class JournalEntry
    {
        [Key]
        public int JournalEntryId { get; set; }

        [ForeignKey(nameof(Day))]
        public int DayId { get; set; }
        public virtual Day Day { get; set; }


        public Guid UserId { get; set; }
        public List<int> MealList { get; set; } = new List<int>();
        public List<int> FoodList { get; set; } = new List<int>();
        public List<int> RecipeList { get; set; } = new List<int>();

        [Required]
        public DateTime TimeStamp { get; set; }

        public double Calories { get; set; }
        public double Carbs { get; set; }
        public double Proteins { get; set; }
        public double Fats { get; set; }
    }
}
