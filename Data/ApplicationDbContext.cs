// Database context managing application data, Identity users and entity relationships

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CTRL.Models;

namespace CTRL.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Event> Events { get; set; } = null!;
        public DbSet<DJ> DJs { get; set; } = null!;
        public DbSet<EventRegistration> EventRegistrations { get; set; } = null!;
    }
}