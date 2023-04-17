using Microsoft.EntityFrameworkCore;

namespace Text_Editor.Models
{
    public class ApplicationDbContext: DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<UserModel> Users { get; set; }
        public DbSet<DocModel> Docs { get; set; }
    }
}
