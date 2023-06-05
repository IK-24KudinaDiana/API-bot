using API_test_7.Models;
using API_test_7.Constants;
using Npgsql;

namespace API_test_7.Clients
{
    public class DatabaseClient
    {
        NpgsqlConnection con = new NpgsqlConnection(DatabaseConstants.Connect);
        //додавання
        public async Task InsertRecipeAsync(MyRecipes myRecipes)
        {
            var sql = "insert into public.\"MyRecipes\"(\"Id\", \"NameRecipe\", \"Recipe\", \"Time\")"
                + $"values (@Id, @NameRecipe, @Recipe, @Time)";
            NpgsqlCommand comm = new NpgsqlCommand(sql, con);
            comm.Parameters.AddWithValue("Id", myRecipes.Id);
            comm.Parameters.AddWithValue("NameRecipe", myRecipes.NameRecipe);
            comm.Parameters.AddWithValue("Recipe", myRecipes.Recipe);
            comm.Parameters.AddWithValue("Time", DateTime.Now);
            await con.OpenAsync();
            await comm.ExecuteNonQueryAsync();
            await con.CloseAsync();
        }

        
        //виведення всіх рецептів одного користувача
        public async Task<List<MyRecipes>> SelectRecipesAsync(long id)
        {
            List<MyRecipes> recipes = new List<MyRecipes>();
            await con.OpenAsync();
            var sql = "SELECT \"Id\", \"NameRecipe\", \"Recipe\", \"Time\" FROM public.\"MyRecipes\" WHERE \"Id\" = @Id";
            NpgsqlCommand comm = new NpgsqlCommand(sql, con);
            comm.Parameters.AddWithValue("@Id", id);
            NpgsqlDataReader reader = await comm.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                int recipeId = reader.GetInt32(0);
                string nameRecipe = reader.GetString(1);
                string[] recipeArray = (string[])reader.GetValue(2);
                DateTimeOffset time = reader.GetDateTime(3);

                recipes.Add(new MyRecipes { Id = recipeId, NameRecipe = nameRecipe, Recipe = recipeArray, Time = time });
            }
            await con.CloseAsync();
            return recipes;
        }

        //видалення
        //public async Task DeleteAllRecipesAsync()
        //{
        //    await con.OpenAsync();
        //    var sql = "DELETE FROM public.\"MyRecipes\"";
        //    NpgsqlCommand comm = new NpgsqlCommand(sql, con);
        //    await comm.ExecuteNonQueryAsync();
        //    await con.CloseAsync();
        //}



        //виведення за назвою та id
        public async Task<MyRecipes> GetRecipeByIdAndNameAsync(long id, string nameRecipe)
        {
            await con.OpenAsync();
            var sql = "SELECT \"Id\", \"NameRecipe\", \"Recipe\", \"Time\" FROM public.\"MyRecipes\" WHERE \"Id\" = @Id AND LOWER(\"NameRecipe\") = LOWER(@NameRecipe)";
            NpgsqlCommand comm = new NpgsqlCommand(sql, con);
            comm.Parameters.AddWithValue("@Id", id);
            comm.Parameters.AddWithValue("@NameRecipe", nameRecipe);
            NpgsqlDataReader reader = await comm.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                long recipeId = reader.GetInt64(0);
                string recipeName = reader.GetString(1);
                string[] recipeArray = (string[])reader.GetValue(2);
                DateTimeOffset time = reader.GetFieldValue<DateTimeOffset>(3);

                MyRecipes recipe = new MyRecipes { Id = recipeId, NameRecipe = recipeName, Recipe = recipeArray, Time = time };
                await con.CloseAsync();
                return recipe;
            }

            await con.CloseAsync();
            return null;
        }
        
        //видалення одного рецепту за назвою та id
        public async Task DeleteRecipeByNameAndIdAsync(string recipeName, long recipeId)
        {
            await con.OpenAsync();
            var sql = "DELETE FROM public.\"MyRecipes\" WHERE \"NameRecipe\" = @RecipeName AND \"Id\" = @Id";
            NpgsqlCommand comm = new NpgsqlCommand(sql, con);
            comm.Parameters.AddWithValue("RecipeName", recipeName);
            comm.Parameters.AddWithValue("Id", recipeId);
            await comm.ExecuteNonQueryAsync();
            await con.CloseAsync();

        }
       
        //оновлення за назвою та id
        public async Task UpdateRecipeByIdAndNameAsync(long recipeId, string recipeName, MyRecipes updatedRecipe)
        {
            await con.OpenAsync();
            var sql = "UPDATE public.\"MyRecipes\" SET \"Id\" = @UpdatedId, \"NameRecipe\" = @UpdatedName, \"Recipe\" = @UpdatedRecipe, \"Time\" = @UpdatedTime WHERE \"Id\" = @RecipeId AND LOWER(\"NameRecipe\") = LOWER(@RecipeName)";
            NpgsqlCommand comm = new NpgsqlCommand(sql, con);
            comm.Parameters.AddWithValue("UpdatedId", updatedRecipe.Id);
            comm.Parameters.AddWithValue("UpdatedName", updatedRecipe.NameRecipe);
            comm.Parameters.AddWithValue("UpdatedRecipe", updatedRecipe.Recipe);
            comm.Parameters.AddWithValue("UpdatedTime", DateTime.Now);
            comm.Parameters.AddWithValue("RecipeId", recipeId);
            comm.Parameters.AddWithValue("RecipeName", recipeName);
            await comm.ExecuteNonQueryAsync();
            await con.CloseAsync();
        }
    }
}
