using first.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace first.Data
{
    public class StudentDbContext : DbContext
    {
        public StudentDbContext(DbContextOptions<StudentDbContext> options) : base(options)
        {
            
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Faculty> faculties { get; set; }
        public DbSet<AccountModel> accounts { get; set; }
        public DbSet<MenuModel> menus { get; set; }
        public DbSet<MultipleFileUpload> Files { get; set; }

    }
}
