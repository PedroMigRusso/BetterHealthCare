using BetterHealthCareAPI.Extensions;
using BetterHealthCareAPI.Middleware;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// add foundational services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "BetterHealthCare API", Version = "v1" });
});

// custom dependencies
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplicationServices();

// CORS policy for cross‑origin clients (tune for production)
builder.Services.AddCors(options =>
{
    options.AddPolicy("Default",
        policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
});

var app = builder.Build();

Console.WriteLine($"ENV: {app.Environment.EnvironmentName}");

// middleware pipeline
app.UseCors("Default");
app.UseExceptionHandling();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "BetterHealthCare API V1");
    c.RoutePrefix = "swagger";
});

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
