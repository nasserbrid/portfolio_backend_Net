using Microsoft.EntityFrameworkCore;
using portfolio_backend_Csharp.Models;

namespace portfolio_backend_Csharp.Data
{
    public class ProjectBackendContext: DbContext
    {
        public ProjectBackendContext(DbContextOptions<ProjectBackendContext> options) : base(options) 
        { 
        }
        
        public DbSet<Project> Projects { get; set; }
        public DbSet<Contact> Contacts { get; set; }
    }
}
