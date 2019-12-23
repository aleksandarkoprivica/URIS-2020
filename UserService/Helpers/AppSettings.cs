namespace UserService.Helpers
{
    public class AppSettings
    {
        public string Secret { get; set; }
        
        public string RedisHost { get; set; }
        
        public string RedisPort { get; set; }
        public string ConnectionStrings { get; set; }
    }
}