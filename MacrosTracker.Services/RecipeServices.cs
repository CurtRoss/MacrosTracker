using MacrosTracker.Data;
using MacrosTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacrosTracker.Services
{
    public class RecipeServices
    {
        private readonly Guid _userId;

        public RecipeServices(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateRecipe(RecipeCreate model) 
        {
            var entity =
               new Recipe()
               {
                   UserId = _userId,
                   RecipeName = model.RecipeName,
                   ListOfFoodIds = model.ListOfFoodIds,
                   CreatedUtc = DateTimeOffset.Now
               };
        }
    }
}
