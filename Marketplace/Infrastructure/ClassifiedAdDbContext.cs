using Marketplace.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Marketplace.Infrastructure;

public class ClassifiedAdDbContext : DbContext
{
    private readonly ILoggerFactory _loggerFactory;

    public ClassifiedAdDbContext(DbContextOptions<ClassifiedAdDbContext> options, ILoggerFactory loggerFactory)
        : base(options) => _loggerFactory = loggerFactory;

    public DbSet<ClassifiedAd> ClassifiedAds { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseLoggerFactory(_loggerFactory);
        optionsBuilder.EnableSensitiveDataLogging();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) =>
        modelBuilder.ApplyConfiguration(new ClassifiedAdEntityTypeConfiguration());

    public class ClassifiedAdEntityTypeConfiguration : IEntityTypeConfiguration<ClassifiedAd>
    {
        public void Configure(EntityTypeBuilder<ClassifiedAd> builder) => builder.HasKey(x => x.ClassifiedAdId);
    }
}

    public static class AppBuilderDatabaseExtensions
    {
        public static void EnsureDatabase(this IApplicationBuilder app)
        {
            var context = app.ApplicationServices.GetService<ClassifiedAdDbContext>();

            if (!context.Database.EnsureCreated())
            {
                context.Database.Migrate();
            }
        }
    }