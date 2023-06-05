using API_test_7.Models;
using Newtonsoft.Json;

namespace API_test_7.Clients
{
    public class RecipeClient
    {
        public static async Task<List<Recipe>> GetRecipesAsync(string dish)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://recipe-by-api-ninjas.p.rapidapi.com/v1/recipe?query={dish}"),
                Headers =
            {
                { "X-RapidAPI-Key", "34d7f3c379mshf511c2d5acb645fp1ddca2jsn939b03ca067e" },
                { "X-RapidAPI-Host", "recipe-by-api-ninjas.p.rapidapi.com" },
            },
            };

            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync();
                List<Recipe> recipes = JsonConvert.DeserializeObject<List<Recipe>>(json);
                return recipes;
            }
        }
    }
}
