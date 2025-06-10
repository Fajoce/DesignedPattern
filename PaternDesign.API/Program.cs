using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PaternDesign.API.Application.Abstractions;
using PaternDesign.API.Application.Behaviors;
using PaternDesign.API.Application.Features.Commands;
using PaternDesign.API.Application.Handlers;
using PaternDesign.API.Application.Implementations.StrategyPattern;
using PaternDesign.API.Application.Services;
using PaternDesign.API.Domain.Abstractions;
using PaternDesign.API.Domain.Abstractions.Implementations;
using PaternDesign.API.Domain.Abstractions.Repositories;
using PaternDesign.API.Domain.Mappings;
using PaternDesign.API.Infraestructure.Data;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Configuración para una base de datos en memoria
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseInMemoryDatabase("BaseDeDatos")
           .EnableDetailedErrors() // Útil para depuración.
           .EnableSensitiveDataLogging(); // Útil para depuración, deshabilitar en producción.
});

builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
// Registrar MediatR (ajusta el assembly según tu proyecto)
builder.Services.AddMediatR(typeof(CreateProductCommand).Assembly);

// Registrar los validadores de FluentValidation
builder.Services.AddValidatorsFromAssembly(typeof(CreateProductCommandValidator).Assembly);

// Registrar el pipeline behavior de validación
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

builder.Services.AddAutoMapper(typeof(ProductProfile));
// Inyección de dependencias
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repositorio<>));
// Unit of Work
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<INotificationHandler<ProductCreatedEvent>, ProductCreatedEventHandler>();  // Registrar el handler  // Registrar el handler

builder.Services.AddScoped<IPricingStrategyFactory, PromotionalPricingStrategy>();

builder.Services.AddScoped<ProductService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

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
