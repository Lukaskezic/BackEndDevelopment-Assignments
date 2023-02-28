using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Assignment3.Models;
using Assignment3.Models.Users;

namespace Assignment3.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<KitchenUser>? KitchenUsers { get; set; }
        public DbSet<ReceptionUser>? ReceptionUsers { get; set; }
        public DbSet<WaiterUser>? WaiterUsers { get; set; }
        public DbSet<CheckIn> BreakfastCheckIns { get; set; } = default!;
        public DbSet<Expected> BreakfastGuestsExpecteds { get; set; } = default!;
    }
}