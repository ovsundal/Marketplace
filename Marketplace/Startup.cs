using Marketplace.ClassifiedAd;
using Marketplace.Domain;
using Marketplace.Domain.UserProfile;
using Marketplace.Infrastructure;
using Marketplace.UserProfile;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Raven.Client.Documents;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace Marketplace
{
    public class Startup
    {
        public Startup(IHostingEnvironment environment, IConfiguration configuration)
        {
            Environment = environment;
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }
        private IHostingEnvironment Environment { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            const string connectionString =
                "Host=localhost;Database=Marketplace;Username=ddd;Password=ddd";
            services
                .AddEntityFrameworkNpgsql()
                .AddDbContext<ClassifiedAdDbContext>(
                    options => options.UseNpgsql(connectionString));

            var store = new DocumentStore
            {
                Urls = new[] {"http://localhost:8080"},
                Database = "Marketplace",
                Conventions =
                {
                    FindIdentityProperty = x => x.Name == "DbId"
                }
            };
            store.Initialize();

            var purgomalumClient = new PurgomalumClient();

            services.AddScoped(c => store.OpenAsyncSession());
            services.AddScoped<IUnitOfWork, EfCoreUnitOfWork>();
            services.AddScoped<IClassifiedAdRepository, ClassifiedAdRepository>();
            services.AddScoped<IUserProfileRepository, UserProfileRepository>();
            services.AddScoped<ClassifiedAdsApplicationService>();
            services.AddScoped(c =>
                new UserProfileApplicationService(
                    c.GetService<IUserProfileRepository>(),
                    c.GetService<IUnitOfWork>(),
                    text => purgomalumClient.CheckForProfanity(text).GetAwaiter().GetResult()));

            services.AddMvc(options => options.EnableEndpointRouting = false);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.EnsureDatabase();
            app.UseEndpoints(endpoints => {
                endpoints.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
            });
            app.UseSwagger();
            app.UseSwaggerUI(c =>
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ClassifiedAds v1"));
        }
    }
}