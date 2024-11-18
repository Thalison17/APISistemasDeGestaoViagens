using APISistemaGestaoViagens.Data;
using APISistemaGestaoViagens.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace APISistemaGestaoViagens.Services
{
    public class ReportService
    {
        private readonly AppDbContext _context;

        public ReportService(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<string> ObterReservasPorPeriodo()
        {
            var reservasPorPeriodo = _context.Reservas
                .Include(r => r.Cliente)
                .Include(r => r.Viagem)
                .Where(r => r.DataReserva >= new DateTime(2024, 11, 1) && r.DataReserva <= new DateTime(2024, 11, 30))
                .Select(r => $"Cliente: {r.Cliente.Nome}, Viagem: {r.Viagem.Destino.Localizacao}, Data: {r.DataReserva.ToString("yyyy-MM-dd")}")
                .ToList();

            return reservasPorPeriodo;
        }

        public IEnumerable<string> ObterDestinosMaisProcurados()
        {
            var destinosMaisProcurados = _context.Reservas
                .Include(r => r.Viagem)
                .GroupBy(r => r.Viagem.Destino.Localizacao)
                .Select(g => new
                {
                    Destino = g.Key,
                    Reservas = g.Count()
                })
                .OrderByDescending(g => g.Reservas)
                .Select(g => $"Destino: {g.Destino}, Reservas: {g.Reservas}")
                .ToList();

            return destinosMaisProcurados;
        }

        public IEnumerable<string> ObterClientesFrequentes()
        {
            var clientesFrequentes = _context.Reservas
                .Include(r => r.Cliente)
                .GroupBy(r => r.Cliente.Nome)
                .Select(g => new
                {
                    Cliente = g.Key,
                    TotalDeReservas = g.Count()
                })
                .OrderByDescending(g => g.TotalDeReservas)
                .Select(g => $"Cliente: {g.Cliente}, Total de Reservas: {g.TotalDeReservas}")
                .ToList();

            return clientesFrequentes;
        }

        public IEnumerable<string> ObterReceitaPorViagem()
        {
            var receitaPorViagem = _context.Reservas
                .Include(r => r.Viagem)
                .ThenInclude(v => v.Destino)
                .GroupBy(r => r.Viagem.Destino.Localizacao)
                .Select(g => new
                {
                    Destino = g.Key,
                    ReceitaTotal = g.Sum(r => r.CustoTotal)
                })
                .OrderByDescending(g => g.ReceitaTotal)
                .Select(g => $"Destino: {g.Destino}, Receita Total: R${g.ReceitaTotal:F2}")
                .ToList();

            return receitaPorViagem;
        }
    }
}
