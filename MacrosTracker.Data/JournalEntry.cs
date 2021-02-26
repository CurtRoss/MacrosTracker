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
        public long DayId { get; set; }
        public virtual Day Day { get; set; }


        public Guid UserId { get; set; }
        public FoodItem FoodItem { get; set; }
        public Meal Meal { get; set; }

        [Required]
        public DateTime TimeStamp { get; set; }
    }
}
