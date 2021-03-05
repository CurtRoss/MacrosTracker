using MacrosTracker.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MacrosTracker.Enum.MealCategoryEnum;

namespace MacrosTracker.Models
{
    public class MealEdit
    {
        public int MealId { get; set; }
        public string MealName { get; set; }
        public TypeofMealCategory Category { get; set; }       
        public List<int> ListOfFoodIds { get; set; } = new List<int>();
    }
}
