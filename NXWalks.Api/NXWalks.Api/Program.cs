using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using NXWalks.Api.Data;
using NXWalks.Api.Repositories;
using NXWalks.Api.Repositories.WalkDifficultyRepository;
using NXWalks.Api.Repositories.WalksRepository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<NzWalksDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("NzWalksConnectionString"));
});
builder.Services.AddScoped<IRegionRepository,RegionRepository>();
builder.Services.AddScoped<IWalksRepository, WalksRepository>();
builder.Services.AddScoped<IWalkDifficultRepository, WalkDifficultRepository>();

builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddControllersWithViews()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
