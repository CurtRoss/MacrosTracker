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
        
    }
}
