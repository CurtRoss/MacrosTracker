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
        [Display(Name = "Journal Date")]
        public DateTime Date { get; set; }
    }

    public class DayDetail
    {
        public int DayId { get; set; }

        [Display(Name = "Journal Date")]
        public DateTime Date { get; set; }
        public int DayCarbs { get; set; }
        public int DayProteins { get; set; }
        public int DayFats { get; set; }
        public int DayCalories { get; set; }
        public int PlusOrMinusCalories { get; set; }
        public int PlusOrMinusCarbs { get; set; }
        public int PlusOrMinusProteins { get; set; }
        public int PlusOrMinusFats { get; set; }
    }

    public class SpecificDayDetail : DayDetail // zero refences -- look at deleting
    {
        public List<string> DayFoods { get; set; } // zero refrences -- look at deleting
    }
}
