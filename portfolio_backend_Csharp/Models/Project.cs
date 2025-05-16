namespace portfolio_backend_Csharp.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Skills { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
    }
}
