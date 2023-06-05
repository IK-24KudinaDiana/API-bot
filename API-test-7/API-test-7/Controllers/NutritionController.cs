using API_test_7.Clients;
using API_test_7.Models;
using Microsoft.AspNetCore.Mvc;

namespace API_test_7.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NutritionController : ControllerBase
    {
        private readonly ILogger<NutritionController> _logger;

        public NutritionController(ILogger<NutritionController> logger)
        {
            _logger = logger;

        }

        [HttpGet(Name = "Nutrition")]
        public async Task<ActionResult<IEnumerable<Nutrition>>> GetNutritionsAsync(string dish)
        {
            try
            {
                return NutritionClient.GetNutritionsAsync(Uri.EscapeDataString(dish)).Result;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
