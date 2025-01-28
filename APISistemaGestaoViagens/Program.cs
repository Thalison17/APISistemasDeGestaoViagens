using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using APISistemaGestaoViagens.Model.Entities;
using APISistemaGestaoViagens.Repository.Interfaces;
using APISistemaGestaoViagens.Repository.Implementations;
using APISistemaGestaoViagens.Data;
using APISistemaGestaoViagens.Services;
using APISistemaGestaoViagens.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("ViagensDb"));

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<IDestinoService, DestinoService>();
builder.Services.AddScoped<IReservaService, ReservaService>();
builder.Services.AddScoped<IViagemService, ViagemService>();
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
    // Criando viagens únicas
    var viagem1 = new Viagem
    {
        ViagemId = 1,
        DestinoId = 1,
        DataPartida = new DateTime(2024, 11, 15),
        DataRetorno = new DateTime(2024, 11, 20),
        Status = "Pendente"
    };

    var viagem2 = new Viagem
    {
        ViagemId = 2,
        DestinoId = 1,
        DataPartida = new DateTime(2024, 11, 20),
        DataRetorno = new DateTime(2024, 11, 23),
        Status = "Pendente"
    };

    var viagem3 = new Viagem
    {
        ViagemId = 3,
        DestinoId = 1,
        DataPartida = new DateTime(2024, 12, 01),
        DataRetorno = new DateTime(2024, 12, 07),
        Status = "Pendente"
    };

    var viagem4 = new Viagem
    {
        ViagemId = 4,
        DestinoId = 1,
        DataPartida = new DateTime(2024, 12, 10),
        DataRetorno = new DateTime(2024, 12, 17),
        Status = "Pendente"
    };
    
    context.Viagem.AddRange(viagem1, viagem2, viagem3, viagem4);
    
    context.Reservas.Add(new Reserva
    {
        ReservaId = 1,
        ClienteId = 1,
        DataReserva = new DateTime(2024, 11, 10),
        MetodoPagamento = "Cartão de Crédito",
        StatusPagamento = "Pendente",
        CustoTotal = 1000,
        Viagem = viagem1
    });

    context.Reservas.Add(new Reserva
    {
        ReservaId = 2,
        ClienteId = 2,
        DataReserva = new DateTime(2024, 11, 12),
        MetodoPagamento = "Boleto",
        StatusPagamento = "Pendente",
        CustoTotal = 600,
        Viagem = viagem2
    });

    context.Reservas.Add(new Reserva
    {
        ReservaId = 3,
        ClienteId = 3,
        DataReserva = new DateTime(2024, 11, 15),
        MetodoPagamento = "Cartão de Crédito",
        StatusPagamento = "Pendente",
        CustoTotal = 1400,
        Viagem = viagem3
    });

    context.Reservas.Add(new Reserva
    {
        ReservaId = 4,
        ClienteId = 4,
        DataReserva = new DateTime(2024, 11, 18),
        MetodoPagamento = "Transferência Bancária",
        StatusPagamento = "Pendente",
        CustoTotal = 3400,
        Viagem = viagem4
    });

    context.SaveChanges();
}

}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
