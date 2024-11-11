using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using APISistemaGestaoViagens.Model.Entities;
using APISistemaGestaoViagens.Repository.Interfaces;
using APISistemaGestaoViagens.Repository.Implementations;
using APISistemaGestaoViagens.Data;

var builder = WebApplication.CreateBuilder(args);

// Configurando o banco de dados em memória
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("ViagensDb"));

// Configuração do repositório genérico para todas as entidades
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

// Configurando o serviço de controladores
builder.Services.AddControllers();

// Configuração do Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "API de Gestão de Viagens",
        Description = "API para gerenciar clientes, destinos, viagens e reservas."
    });
});

var app = builder.Build();

// Configurando o pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();