using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Assignment2.Models;

namespace Assignment2.Data
{
    public class Db : DbContext
    {
        public Db(DbContextOptions<Db> options)
            : base(options) { }

        public DbSet<Expense> Expenses => Set<Expense>();
        public DbSet<Job> Jobs => Set<Job>();
        public DbSet<Model> Models => Set<Model>();
    }
}
