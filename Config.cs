namespace QuestDbPOC
{
    public class Config
    {
        public Config()
        {
        }
        public static string ConnectionString = string.Empty;
        public static ApiSettings ApiSettings;


        public static string AdminUsers { get; set; }
    }
}
