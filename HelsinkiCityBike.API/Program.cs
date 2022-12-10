using HelsinkiCityBike.API.Configuration;
using HelsinkiCityBike.BLL.Services;
using HelsinkiCityBike.DAL;
using HelsinkiCityBike.DAL.Repositories;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddSingleton<DbContext>();
builder.Services.AddScoped<IJourneyRepository, JourneyRepository>();
builder.Services.AddScoped<IStationRepository, StationRepository>();
builder.Services.AddScoped<IJourneyService, JourneyService>();
builder.Services.AddScoped<IStationService, StationService>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(AutoMapperApi));

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();


app.MapControllers();

app.Run();
