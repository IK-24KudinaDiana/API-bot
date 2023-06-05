using API_test_7.Clients;
using API_test_7.Models;
using Microsoft.AspNetCore.Mvc;

namespace API_test_7.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DatabaseController : ControllerBase
    {
        private readonly DatabaseClient _database;

        public DatabaseController()
        {
            _database = new DatabaseClient();
        }
        [HttpPost]
        public async Task<IActionResult> AddRecipe([FromBody] MyRecipes myRecipes)
        {
            try
            {
                await _database.InsertRecipeAsync(myRecipes);
                return Ok("Recipe added successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        

        [HttpGet("{id}")]
        public async Task<ActionResult<List<MyRecipes>>> GetRecipesById(long id)
        {
            try
            {
                var recipes = await _database.SelectRecipesAsync(id);
                if (recipes.Count > 0)
                {
                    return Ok(recipes);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
                
        }


        
        [HttpGet("{id}/{nameRecipe}")]
        public async Task<ActionResult<MyRecipes>> GetRecipeByIdAndName(long id, string nameRecipe)
        {
            try
            {
                var recipe = await _database.GetRecipeByIdAndNameAsync(id, nameRecipe);
                if (recipe != null)
                {
                    return Ok(recipe);
                }
                else
                {
                    return NotFound();
                }
            }
            catch(Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        
        [HttpDelete("{recipeId}")]
        public async Task<IActionResult> DeleteRecipeByNameAndId(string recipeName, long recipeId)
        {
            try
            {
                await _database.DeleteRecipeByNameAndIdAsync(recipeName, recipeId);
               return Ok("Recipe deleted successfully.");
            }
            catch (Exception ex)
            {
                // Обработка ошибки удаления рецепта
                return StatusCode(500, "An error occurred while deleting the recipe.");
            }
        }

        //[HttpDelete]
        //public async Task<IActionResult> DeleteAllRecipes()
        //{
        //    try
        //    {
        //        await _database.DeleteAllRecipesAsync();
        //        return Ok("All recipes deleted successfully.");
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"An error occurred: {ex.Message}");
        //    }
        //}

        [HttpPut("recipes/{recipeId}")]
        public async Task<IActionResult> UpdateRecipeByIdAndName(long recipeId, [FromBody] MyRecipes updatedRecipe)
        {
            try
            {
                await _database.UpdateRecipeByIdAndNameAsync(recipeId, updatedRecipe.NameRecipe, updatedRecipe);
                return Ok("Рецепт успішно оновлено.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Помилка при оновленні рецепту: {ex.Message}");
            }
        }

        
    }
}
