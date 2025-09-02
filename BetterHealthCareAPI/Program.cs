using BetterHealthCareAPI.Application.Interfaces;
using BetterHealthCareAPI.Application;
using BetterHealthCareAPI.Infrastructure;
using Microsoft.EntityFrameworkCore;
using BetterHealthCareAPI.Application.Mapper;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<BetterHealthCareDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IPatientService, PatientService>();
builder.Services.AddScoped<IProcedureService, ProcedureService>();
builder.Services.AddScoped<IPatientActionService, PatientActionService>();
builder.Services.AddScoped<IMedicalFileService, MedicalFileService>();
builder.Services.AddAutoMapper(typeof(MappingProfile));

var app = builder.Build();

// DEBUG: mostrar ambiente
Console.WriteLine($"ENV: {app.Environment.EnvironmentName}");

// Ativa Swagger SEM depender de ambiente
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "BetterHealthCare API V1");
    c.RoutePrefix = "swagger"; // vai abrir em /swagger
});

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
