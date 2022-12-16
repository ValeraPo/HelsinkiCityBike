using HelsinkiCityBike.API.Configuration;
using HelsinkiCityBike.API.Infrastructure;
using HelsinkiCityBike.BLL.Configurations;
using HelsinkiCityBike.BLL.Services;
using HelsinkiCityBike.DAL;
using HelsinkiCityBike.DAL.Repositories;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddSingleton<DbContext>();
builder.Services.AddScoped<IDbContext, DbContext>();
builder.Services.AddScoped<IJourneyRepository, JourneyRepository>();
builder.Services.AddScoped<IStationRepository, StationRepository>();
builder.Services.AddScoped<IJourneyService, JourneyService>();
builder.Services.AddScoped<IStationService, StationService>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(AutoMapperApi), typeof(AutoMapperBLL));
builder.Services.AddCors();


var app = builder.Build();
app.UseCors(
       options => options.WithOrigins("*").AllowAnyMethod()
   );
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseMiddleware<ErrorExceptionMiddleware>();

app.MapControllers();

app.Run();
