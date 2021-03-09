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
    public class RecipeController : ApiController
    {
        private RecipeServices CreateRecipeServices()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var recipeServices = new RecipeServices(userId);
            return recipeServices;
        }

        /// <summary>
        /// Displays all Recipes.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/GetRecipe")]
        public IHttpActionResult Get()
        {
            RecipeServices recipeServices = CreateRecipeServices();
            var recipes = recipeServices.GetRecipe();
            return Ok(recipes);
        }

        /// <summary>
        /// Create a new Recipe.
        /// </summary>
        /// <param name="recipe"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/Recipe")]
        public IHttpActionResult Post(RecipeCreate recipe)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateRecipeServices();

            if (!service.CreateRecipe(recipe))
                return InternalServerError();

            return Ok();
        }

        /// <summary>
        /// Displays the details of the specified Recipe.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            RecipeServices recipeServices = CreateRecipeServices();
            var recipe = recipeServices.GetRecipeById(id);
            return Ok(recipe);
        }

        /// <summary>
        /// Edit the specified Recipe.
        /// </summary>
        /// <param name="recipe"></param>
        /// <returns></returns>
        [HttpPut]
        public IHttpActionResult Put(RecipeEdit recipe)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateRecipeServices();

            if (!service.UpdateRecipe(recipe))
                return InternalServerError();

            return Ok();
        }

        /// <summary>
        /// Delete the specified Recipe.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var service = CreateRecipeServices();

            if (!service.DeleteRecipe(id))
                return InternalServerError();

            return Ok();
        }
    }
}
