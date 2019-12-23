using UserService.Entities;
using Microsoft.EntityFrameworkCore;

namespace UserService.Database
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
           // var seedUsers = new UserSeed();
        }
        
        public DbSet<User> Users { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Assistent> Assistents { get; set; }
        public DbSet<Professor> Professors { get; set; }
    }
}