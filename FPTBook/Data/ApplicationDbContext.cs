using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using FPTBook.Models;

namespace FPTBook.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<FPTBook.Models.PublishingCompanies> PublishingCompanies { get; set; } = default!;
        public DbSet<FPTBook.Models.Category> Category { get; set; } = default!;
        public DbSet<FPTBook.Models.TmpCategory> TmpCategory { get; set; } = default!;
    }
}