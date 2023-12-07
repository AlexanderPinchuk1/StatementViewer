using Microsoft.EntityFrameworkCore;
using StatementViewer.Models;

namespace StatementViewer.Repositories
{
    /// <summary>
    /// Контекст для работы с БД
    /// </summary>
    public class StatementViewerDbContext : DbContext
    {
        public DbSet<AccountInfo> AccountsInfo { get; set; }
        public DbSet<AccountUnit> AccountClasses { get; set; }
        public DbSet<Statement> Documents { get; set; }



        public StatementViewerDbContext(DbContextOptions<StatementViewerDbContext> options)
            : base(options)
        {
            Database.Migrate();
        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(
                assembly: typeof(AccountInfo).Assembly);
        }
    }
}
