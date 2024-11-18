using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using APISistemaGestaoViagens.Model.Entities;
using APISistemaGestaoViagens.Repository.Interfaces;
using APISistemaGestaoViagens.Repository.Implementations;
using APISistemaGestaoViagens.Data;
using APISistemaGestaoViagens.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("ViagensDb"));

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IDestinoRepository, DestinoRepository>();
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<ReportService>(); 

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

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    
    if (!context.Destino.Any())
    {
        context.Destino.Add(new Destino
        {
            DestinoId = 1,
            Localizacao = "Rio de Janeiro",
            Pais = "Brasil",
            PrecoPorDia = 200
        });

        context.Destino.Add(new Destino
        {
            DestinoId = 2,
            Localizacao = "São Paulo",
            Pais = "Brasil",
            PrecoPorDia = 150
        });

        context.Destino.Add(new Destino
        {
            DestinoId = 3,
            Localizacao = "Paris",
            Pais = "França",
            PrecoPorDia = 500
        });

        context.Destino.Add(new Destino
        {
            DestinoId = 4,
            Localizacao = "Londres",
            Pais = "Reino Unido",
            PrecoPorDia = 450
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

        context.Clientes.Add(new Cliente
        {
            ClienteId = 3,
            Nome = "Carlos Pereira",
            Email = "carlos.pereira@example.com",
            Telefone = "31988888888",
            Cpf = "11223344556"
        });

        context.Clientes.Add(new Cliente
        {
            ClienteId = 4,
            Nome = "Ana Costa",
            Email = "ana.costa@example.com",
            Telefone = "41999999999",
            Cpf = "22334455667"
        });

        context.SaveChanges();
    }

    if (!context.Reservas.Any())
    {
        context.Reservas.Add(new Reserva
        {
            ReservaId = 1,
            ClienteId = 1,
            DestinoId = 1, 
            DataReserva = new DateTime(2024, 11, 10),
            MetodoPagamento = "Cartão de Crédito",
            DuracaoDias = 5 
        });

        context.Reservas.Add(new Reserva
        {
            ReservaId = 2,
            ClienteId = 2,
            DestinoId = 2, 
            DataReserva = new DateTime(2024, 11, 12),
            MetodoPagamento = "Boleto",
            DuracaoDias = 3
        });

        context.Reservas.Add(new Reserva
        {
            ReservaId = 3,
            ClienteId = 3,
            DestinoId = 3, 
            DataReserva = new DateTime(2024, 11, 15),
            MetodoPagamento = "Cartão de Crédito",
            DuracaoDias = 7
        });

        context.Reservas.Add(new Reserva
        {
            ReservaId = 4,
            ClienteId = 4,
            DestinoId = 4, 
            DataReserva = new DateTime(2024, 11, 18),
            MetodoPagamento = "Transferência Bancária",
            DuracaoDias = 10
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
