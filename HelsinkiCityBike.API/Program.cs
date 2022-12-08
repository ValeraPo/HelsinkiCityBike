using HelsinkiCityBike.DAL;
using HelsinkiCityBike.DAL.Repositories;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddSingleton<DbContext>();
builder.Services.AddScoped<IJourneyRepository, JourneyRepository>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();


app.MapControllers();

app.Run();
