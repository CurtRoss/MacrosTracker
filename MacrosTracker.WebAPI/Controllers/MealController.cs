using MacrosTracker.Models;
using MacrosTracker.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using static MacrosTracker.Categories.MealCategory;

namespace MacrosTracker.WebAPI.Controllers
{
    [Authorize]
    public class MealController : ApiController
    {
        public MealService CreateMealService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var mealService = new MealService(userId);
            return mealService;
        }

        [HttpPost]
        [Route("api/Meal")]
        public IHttpActionResult Post(MealCreate meal)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateMealService();

            if (!service.CreateMeal(meal))
                return InternalServerError();

            return Ok();
        }

        [HttpGet]
        [Route("api/GetMeal")]
        public IHttpActionResult Get()
        {
            MealService mealService = CreateMealService();
            var meals = mealService.GetMeals();
            return Ok(meals);
        }

        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            MealService mealService = CreateMealService();
            var meal = mealService.GetMealById(id);
            return Ok(meal);
        }

        [HttpGet]
        [Route("api/GetMealCategory/{category}")]
        public IHttpActionResult GetByCategory(TypeofMealCategory category)
        {
            MealService mealService = CreateMealService();
            var meal = mealService.GetMealsByCategory(category);
            return Ok(meal);
        }

        public IHttpActionResult Put(MealEdit meal)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateMealService();

            if (!service.UpdateMeal(meal))
                return InternalServerError();

            return Ok();
        }

        public IHttpActionResult Delete(int id)
        {
            var service = CreateMealService();

            if (!service.DeleteMeal(id))
                return InternalServerError();

            return Ok();
        }
    }
}
