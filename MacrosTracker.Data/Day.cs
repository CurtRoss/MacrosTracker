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
        public DateTime DateOfEntry { get; set; }

        [Key]
        public DateTime DayId
        {
            get
            {
                return DateOfEntry.Date;
            }
        }

        public int TotalCalories { get; set; }
        public int TotalFats { get; set; }
        public int TotalCarbs { get; set; }
        public int TotalProteins { get; set; }

    }
}
