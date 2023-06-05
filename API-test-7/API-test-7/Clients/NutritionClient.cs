using API_test_7.Models;
using Newtonsoft.Json;

namespace API_test_7.Clients
{
    public class NutritionClient
    {
        public static async Task<List<Nutrition>> GetNutritionsAsync(string dish)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://nutrition-by-api-ninjas.p.rapidapi.com/v1/nutrition?query={dish}"),
                Headers =
                {
                    { "X-RapidAPI-Key", "34d7f3c379mshf511c2d5acb645fp1ddca2jsn939b03ca067e" },
                    { "X-RapidAPI-Host", "nutrition-by-api-ninjas.p.rapidapi.com" },
                },
            };
            using (var response = await client.SendAsync(request))
            {

                var json = await response.Content.ReadAsStringAsync();
                List<Nutrition> nutrition = JsonConvert.DeserializeObject<List<Nutrition>>(json);
                return nutrition;

            }
        }

    }
}
