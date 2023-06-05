namespace API_test_7.Constants
{
    public class DatabaseConstants
    {
        public static string Host = "localhost";
        public static string Username = "postgres";
        public static string Password = "z1x2c3v4";
        public static string DatabaseName = "postgres";
        public static string Connect => $"Host={Host};Username={Username};Password={Password};Database={DatabaseName}";
    }
}
