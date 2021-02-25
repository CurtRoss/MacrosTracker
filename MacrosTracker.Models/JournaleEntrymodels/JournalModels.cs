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
        public Meal Meal { get; set; }
        public FoodItem FoodItem { get; set; }
    }

    public class JournalEntryDetail
    {
        public int JournalEntryId { get; set; }
        public DateTime JournalDate { get; set; }
        public Meal Meal { get; set; }
        public FoodItem FoodItem { get; set; }
    }

    public class JournalEntryEdit
    {
        public int JournalEntryId { get; set; }
        public DateTime JournalDate { get; set; }
        public Meal Meal { get; set; }
        public FoodItem FoodItem { get; set; }
    }

}
