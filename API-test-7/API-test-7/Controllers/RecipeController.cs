using API_test_7.Clients;
using API_test_7.Models;
using Microsoft.AspNetCore.Mvc;

namespace API_test_7.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RecipeController : ControllerBase
    {
        private readonly ILogger<RecipeController> _logger;


        public RecipeController(ILogger<RecipeController> logger)
        {
            _logger = logger;

        }

        [HttpGet(Name = "Recipe")]
        public async Task<ActionResult<IEnumerable<Recipe>>> GetRecipesAsync(string dish)
        {         
            try
            {
                return RecipeClient.GetRecipesAsync(Uri.EscapeDataString(dish)).Result;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
