namespace AspNetCoreIdentityApp.MVC.Models
{
    public class ApiSettings
    {
        public string BaseUrl { get; set; } = null!;
        public int TimeoutSeconds { get; set; }
        public string DefaultAcceptHeader { get; set; } = "application/json";
    }
}
