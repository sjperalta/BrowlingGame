using BrowlingGame.BLL.Features.Browling;
using BrowlingGame.BLL.Features.Counter;
using BrowlingGame.BLL.ScoreCalculator;
using BrowlingGame.BLL.ScoreCalculator.Strategies;
using BrowlingGame.DAL.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var logger = builder.Logging.AddLog4Net();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddLogging();
builder.Services.AddLazyCache();


//Loading Configuration
ConfigurationManager configuration = builder.Configuration;
builder.Services.AddDbContext<BrowlingContext>(
    options => options.UseSqlServer(configuration.GetConnectionString("BrowlingDb"), sqlOptions => {
        sqlOptions.MigrationsAssembly("BrowlingGame.DAL");
    }));

//Registering DI services
builder.Services.AddScoped<IStrategy<SpareStrategy>, SpareStrategy>();
builder.Services.AddScoped<IStrategy<OpenFrameStrategy>, OpenFrameStrategy>();
builder.Services.AddScoped<IStrategy<StrikeStrategy>, StrikeStrategy>();
builder.Services.AddScoped<IStrategyContext, StrategyContext>();
builder.Services.AddScoped<IScoreEngine, ScoreEngine>();
builder.Services.AddScoped<IBrowlingService, BrowlingService>();
builder.Services.AddSingleton<ICounterService, CounterService>();

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
