using HiLoGame.Context;
using HiLoGame.Repositories;
using HiLoGame.Services;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IHiLoGameService, HiLoGameService>();
builder.Services.AddScoped<IGameRepository, GameRepository>();
builder.Services.AddScoped<IGameRepositoryEF, GameRepositoryEF>();

builder.Services.AddDbContext<GameContext>(options =>
{
    options.UseInMemoryDatabase(databaseName: "HiLoGame_DB");
});

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddLocalization(options =>
{
    options.ResourcesPath = "Resources";
});

builder.Services.Configure<RequestLocalizationOptions>(
    options =>
    {
        var supportedCultures = new List<CultureInfo>
        {
            new CultureInfo("fr-FR"),
            new CultureInfo("en-EN")
        };

        options.DefaultRequestCulture = new RequestCulture(culture: "fr-FR", uiCulture: "fr-FR");
        options.SupportedCultures = supportedCultures;
        options.SupportedUICultures = supportedCultures;
    });


var app = builder.Build();

app.UseRequestLocalization();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
