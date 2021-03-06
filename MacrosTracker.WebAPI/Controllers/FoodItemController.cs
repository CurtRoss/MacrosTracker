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

        public IHttpActionResult Get()
        {
            FoodItemServices foodItemServices = CreateFoodItemServices();
            var foodItem = foodItemServices.GetFoodItem();
            return Ok(foodItem);
        }

        public IHttpActionResult Post(FoodItemCreate foodItem)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateFoodItemServices();

            if (!service.CreateFoodItem(foodItem))
                return InternalServerError();

            return Ok();
        }

        public IHttpActionResult Get(int id)
        {
            FoodItemServices foodItemServices = CreateFoodItemServices();
            var foodItem = foodItemServices.GetFoodItemById(id);
            return Ok(foodItem);
        }

        public IHttpActionResult Put(FoodItemEdit foodItem)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateFoodItemServices();

            if (!service.UpdateFoodItem(foodItem))
                return InternalServerError();

            return Ok();
        }

        public IHttpActionResult Delete(int id)
        {
            var service = CreateFoodItemServices();

            if (!service.DeleteFoodItem(id))
                return InternalServerError();

            return Ok();
        }
    }
}
