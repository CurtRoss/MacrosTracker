using MacrosTracker.Models;
using MacrosTracker.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MacrosTracker.WebAPI.Controllers
{
    public class MealController : ApiController
    {
        public MealService CreateMealService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var mealService = new MealService(userId);
            return mealService;
        }

        public IHttpActionResult Get()
        {
            MealService mealService = CreateMealService();
            var meals = mealService.GetMeals();
            return Ok(meals);
        }

        public IHttpActionResult Post (MealCreate meal)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateMealService();

            if (!service.CreateMeal(meal))
                return InternalServerError();

            return Ok();
        }
    }
}
