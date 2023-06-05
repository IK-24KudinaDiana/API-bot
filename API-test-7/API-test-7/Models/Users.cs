namespace API_test_7.Models
{
    public class Users
    {
        public int Id { get; set; }
        public string User { get; set; }
    }

    public class MyRecipes
    {
        public long Id { get; set; }
        public string NameRecipe { get; set; }
        public string[] Recipe { get; set; }
        
        public DateTimeOffset Time { get; set; }
    }
}
