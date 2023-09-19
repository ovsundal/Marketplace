using Marketplace;
using Marketplace.Api;
using Marketplace.Domain;
using Marketplace.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Raven.Client.Documents;

var store = new DocumentStore
{
    Urls = new[] { "http://localhost:8080" },
    Database = "Marketplace",
    Conventions =
    {
        FindIdentityProperty = m => m.Name == "_databaseId"
    }
};
store.Initialize();

const string connectionString = "Host=localhost;Database=Marketplace;Username=ddd;Password=ddd";

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSingleton<ICurrencyLookup, FixedCurrencyLookup>();
builder.Services.AddScoped(c => store.OpenAsyncSession());
builder.Services.AddScoped<IUnitOfWork, RavenDbUnitOfWork>();
builder.Services.AddScoped<IClassifiedAdRepository, ClassifiedAdRepository>();
builder.Services.AddScoped<ClassifiedAdsApplicationService>();
builder.Services.AddSwaggerGen();
builder.Services
    .AddDbContext<ClassifiedAdDbContext>(options => options.UseNpgsql(connectionString));

var app = builder.Build();
app.EnsureDatabase();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();