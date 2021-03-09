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
    public class FoodItemController : ApiController
    {
        private FoodItemServices CreateFoodItemServices()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var foodItem = new FoodItemServices(userId);
            return foodItem;
        }

        /// <summary>
        /// Returns a list of all FoodItems.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult Get()
        {
            FoodItemServices foodItemServices = CreateFoodItemServices();
            var foodItem = foodItemServices.GetFoodItem();
            return Ok(foodItem);
        }

        /// <summary>
        /// Creates a new FoodItem.
        /// </summary>
        /// <param name="foodItem"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult Post(FoodItemCreate foodItem)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateFoodItemServices();

            if (!service.CreateFoodItem(foodItem))
                return InternalServerError();

            return Ok();
        }

        /// <summary>
        /// Returns detailed information for the selected FoodItem.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            FoodItemServices foodItemServices = CreateFoodItemServices();
            var foodItem = foodItemServices.GetFoodItemById(id);
            return Ok(foodItem);
        }

        /// <summary>
        /// Edit the specified FoodItem.
        /// </summary>
        /// <param name="foodItem"></param>
        /// <returns></returns>
        [HttpPut]
        public IHttpActionResult Put(FoodItemEdit foodItem)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateFoodItemServices();

            if (!service.UpdateFoodItem(foodItem))
                return InternalServerError();

            return Ok();
        }

        /// <summary>
        /// Delete the specified FoodItem.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var service = CreateFoodItemServices();

            if (!service.DeleteFoodItem(id))
                return InternalServerError();

            return Ok();
        }
    }
}
