using Marketplace.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Marketplace.ClassifiedAd;

public class ClassifiedAdDbContext : DbContext
{
    private readonly ILoggerFactory _loggerFactory;

    public ClassifiedAdDbContext(DbContextOptions<ClassifiedAdDbContext> options, ILoggerFactory loggerFactory)
        : base(options) => _loggerFactory = loggerFactory;

    public DbSet<Domain.ClassifiedAd> ClassifiedAds { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseLoggerFactory(_loggerFactory);
        optionsBuilder.EnableSensitiveDataLogging();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ClassifiedAdEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new PictureEntityTypeConfiguration());
    }

    public class ClassifiedAdEntityTypeConfiguration : IEntityTypeConfiguration<Domain.ClassifiedAd>
    {
        public void Configure(EntityTypeBuilder<Domain.ClassifiedAd> builder)
        {
            builder.HasKey(x => x.ClassifiedAdId);
            builder.OwnsOne(x => x.Id);
            builder.OwnsOne(x => x.Price, p => p.OwnsOne(c => c.Currency));
            builder.OwnsOne(x => x.Text);
            builder.OwnsOne(x => x.Title);
            builder.OwnsOne(x => x.ApprovedBy);
            builder.OwnsOne(x => x.OwnerId);

        }
    }

    public class PictureEntityTypeConfiguration : IEntityTypeConfiguration<Picture>
    {
        public void Configure(EntityTypeBuilder<Picture> builder)
        {
            builder.HasKey(x => x.PictureId);
            builder.OwnsOne(x => x.Id);
            builder.OwnsOne(x => x.ParentId);
            builder.OwnsOne(x => x.Size);
        }
    }
}

    public static class AppBuilderDatabaseExtensions
    {
        public static void EnsureDatabase(this IApplicationBuilder app)
        {
            app.UseRouting();
            var context = app.ApplicationServices.GetService<ClassifiedAdDbContext>();

            if (!context.Database.EnsureCreated())
            {
                context.Database.Migrate();
            }
        }
    }