using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using SamuraiCore.Entity;

namespace SamuraiCore.Data
{
    public class SamuraiContext : DbContext
    {
        public SamuraiContext(DbContextOptions<SamuraiContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(MyConsoleLoggerFactory).EnableSensitiveDataLogging();
        }

        public static readonly LoggerFactory MyConsoleLoggerFactory
            = new LoggerFactory(new[] {
                new ConsoleLoggerProvider((category, level)
                    => category == DbLoggerCategory.Database.Command.Name
                       && level == LogLevel.Information, true) });

        public DbSet<Samurai> Samurai { get; set; }
        public DbSet<Quote> Quote { get; set; }
        public DbSet<Battle> Battle { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Samurai>().Property(p => p.Name).HasMaxLength(250).IsRequired();

            modelBuilder.Entity<SamuraiBattle>()
                .HasKey(s => new {
                    s.BattleId,
                    s.SamuraiId
                });
        }
    }
}
