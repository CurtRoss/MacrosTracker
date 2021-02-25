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
}
