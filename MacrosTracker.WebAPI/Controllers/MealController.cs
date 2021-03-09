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
        private MealService CreateMealService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var mealService = new MealService(userId);
            return mealService;
        }


        /// <summary>
        /// Creates a new Meal.
        /// </summary>
        /// <param name="meal"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Returns all Meals for the logged in user.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/GetMeal")]
        public IHttpActionResult Get()
        {
            MealService mealService = CreateMealService();
            var meals = mealService.GetMeals();
            return Ok(meals);
        }

        /// <summary>
        /// Returns the details of the specified meal.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            MealService mealService = CreateMealService();
            var meal = mealService.GetMealById(id);
            return Ok(meal);
        }

        /// <summary>
        /// Returns Meals for the specified category. Use 0 for Breakfast, 1 for Lunch, 2 for Dinner, 3 for Dessert, or 4 for Snack.
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/GetMealCategory/{category}")]
        public IHttpActionResult GetByCategory(TypeofMealCategory category)
        {
            MealService mealService = CreateMealService();
            var meal = mealService.GetMealsByCategory(category);
            return Ok(meal);
        }

        /// <summary>
        /// Edit the specified Meal.
        /// </summary>
        /// <param name="meal"></param>
        /// <returns></returns>
        [HttpPut]
        public IHttpActionResult Put(MealEdit meal)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateMealService();

            if (!service.UpdateMeal(meal))
                return InternalServerError();

            return Ok();
        }

        /// <summary>
        /// Delete the specified Meal.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var service = CreateMealService();

            if (!service.DeleteMeal(id))
                return InternalServerError();

            return Ok();
        }
    }
}
