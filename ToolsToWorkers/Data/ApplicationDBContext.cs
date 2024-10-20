using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using ToolsToWorkers.Models;
using ToolsToWorkers.Models.Views;

namespace ToolsToWorkers.Data
{
    public class ApplicationDBContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Storage> Storages { get; set; }
        public DbSet<ToolRequest> ToolRequests { get; set; }
        public DbSet<Tool> Tools { get; set; }
        public DbSet<Article> Articles { get; set; }

        public DbSet<ToolsView> toolsView { get; set; }
        public DbSet<ToolRequestsView> toolRequestsView { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ToolsView>(pc =>
            {
                pc.HasNoKey();
                pc.ToView("ToolsView");
            });
            modelBuilder.Entity<ToolRequestsView>(pc =>
            {
                pc.HasNoKey();
                pc.ToView("ToolRequestsView");
            });
        }

        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {

        }
    }
}
