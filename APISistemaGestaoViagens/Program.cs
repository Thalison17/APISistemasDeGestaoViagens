using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using APISistemaGestaoViagens.Model.Entities;
using APISistemaGestaoViagens.Repository.Interfaces;
using APISistemaGestaoViagens.Repository.Implementations;
using APISistemaGestaoViagens.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("ViagensDb"));

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IDestinoRepository, DestinoRepository>();
builder.Services.AddScoped<IGenericRepository<Reserva>, GenericRepository<Reserva>>();
builder.Services.AddScoped<IGenericRepository<Viagem>, GenericRepository<Viagem>>();
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();

builder.Services.AddControllers();

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

// Adicionando dados iniciais para testes :)
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    
    if (!context.Destino.Any())
    {
        context.Destino.Add(new Destino
        {
            DestinoId = 1,
            Localizacao = "Paris",
            Pais = "França",
            PrecoPorDia = 40
        });

        context.Destino.Add(new Destino
        {
            DestinoId = 2,
            Localizacao = "Londres",
            Pais = "Reino Unido",
            PrecoPorDia = 50
        });

        context.SaveChanges();
    }
    
    if (!context.Clientes.Any())
    {
        context.Clientes.Add(new Cliente
        {
            ClienteId = 1, 
            Nome = "João Silva",
            Email = "joao.silva@example.com",
            Telefone = "11999999999",
            Cpf = "12345678900"
        });

        context.Clientes.Add(new Cliente
        {
            ClienteId = 2, 
            Nome = "Maria Oliveira",
            Email = "maria.oliveira@example.com",
            Telefone = "21988888888",
            Cpf = "98765432100"
        });

        context.SaveChanges();
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();