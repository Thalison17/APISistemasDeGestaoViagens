using APISistemaGestaoViagens.Model.Entities;
using APISistemaGestaoViagens.Model.DTOs;
using APISistemaGestaoViagens.Repository.Interfaces;
using APISistemaGestaoViagens.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace APISistemaGestaoViagens.Services
{
    public class ReservaService : IReservaService
    {
        private readonly IGenericRepository<Reserva> _reservaRepository;
        private readonly IGenericRepository<Viagem> _viagemRepository;
        private readonly IGenericRepository<Destino> _destinoRepository;

        public ReservaService(
            IGenericRepository<Reserva> reservaRepository,
            IGenericRepository<Viagem> viagemRepository,
            IGenericRepository<Destino> destinoRepository)
        {
            _reservaRepository = reservaRepository;
            _viagemRepository = viagemRepository;
            _destinoRepository = destinoRepository;
        }

        public async Task<IEnumerable<ReservaDTO>> GetAllAsync()
        {
            var reservas = await _reservaRepository.GetAllAsync(include: query => query.Include(r => r.Viagem));
            return reservas.Select(r => new ReservaDTO
            {
                ReservaId = r.ReservaId,
                ClienteId = r.ClienteId,
                ViagemId = r.ViagemId,
                DataReserva = r.DataReserva,
                StatusPagamento = r.StatusPagamento,
                MetodoPagamento = r.MetodoPagamento,
                CustoTotal = r.CustoTotal,
                Viagem = r.Viagem == null ? null : new ViagemDTO
                {
                    ViagemId = r.Viagem.ViagemId,
                    DestinoId = r.Viagem.DestinoId,
                    DataPartida = r.Viagem.DataPartida,
                    DataRetorno = r.Viagem.DataRetorno,
                    Status = r.Viagem.Status
                }
            });
        }

        public async Task<ReservaDTO?> GetByIdAsync(int id)
        {
            var reserva = await _reservaRepository.GetByIdAsync(id, include: query => query.Include(r => r.Viagem));
            if (reserva == null) return null;

            return new ReservaDTO
            {
                ReservaId = reserva.ReservaId,
                ClienteId = reserva.ClienteId,
                ViagemId = reserva.ViagemId,
                DataReserva = reserva.DataReserva,
                StatusPagamento = reserva.StatusPagamento,
                MetodoPagamento = reserva.MetodoPagamento,
                CustoTotal = reserva.CustoTotal,
                Viagem = reserva.Viagem == null ? null : new ViagemDTO
                {
                    ViagemId = reserva.Viagem.ViagemId,
                    DestinoId = reserva.Viagem.DestinoId,
                    DataPartida = reserva.Viagem.DataPartida,
                    DataRetorno = reserva.Viagem.DataRetorno,
                    Status = reserva.Viagem.Status
                }
            };
        }

        public async Task<ReservaDTO> CreateAsync(ReservaCreateDTO reservaCreateDto)
        {
            var destino = await _destinoRepository.GetByIdAsync(reservaCreateDto.Viagem.DestinoId);
            if (destino == null) throw new ArgumentException("Destino não encontrado.");

            var diasViagem = (reservaCreateDto.Viagem.DataRetorno - reservaCreateDto.Viagem.DataPartida).Days;
            var custoTotalViagem = diasViagem * destino.PrecoPorDia;

            var viagem = new Viagem
            {
                DestinoId = reservaCreateDto.Viagem.DestinoId,
                DataPartida = reservaCreateDto.Viagem.DataPartida,
                DataRetorno = reservaCreateDto.Viagem.DataRetorno,
                Status = "Pendente"
            };

            await _viagemRepository.AddAsync(viagem);

            var reserva = new Reserva
            {
                ClienteId = reservaCreateDto.ClienteId,
                ViagemId = viagem.ViagemId,
                DataReserva = reservaCreateDto.DataReserva,
                StatusPagamento = reservaCreateDto.StatusPagamento,
                MetodoPagamento = reservaCreateDto.MetodoPagamento,
                CustoTotal = custoTotalViagem
            };

            await _reservaRepository.AddAsync(reserva);

            return new ReservaDTO
            {
                ReservaId = reserva.ReservaId,
                ClienteId = reserva.ClienteId,
                ViagemId = reserva.ViagemId,
                DataReserva = reserva.DataReserva,
                StatusPagamento = reserva.StatusPagamento,
                MetodoPagamento = reserva.MetodoPagamento,
                CustoTotal = reserva.CustoTotal,
                Viagem = new ViagemDTO
                {
                    ViagemId = viagem.ViagemId,
                    DestinoId = viagem.DestinoId,
                    DataPartida = viagem.DataPartida,
                    DataRetorno = viagem.DataRetorno,
                    Status = viagem.Status
                }
            };
        }

        public async Task<bool> UpdateAsync(int id, ReservaDTO reservaDto)
        {
            try 
            {
                var reserva = await _reservaRepository.GetByIdAsync(id);
                if (reserva == null) return false;

                // Validação do StatusPagamento
                if (string.IsNullOrWhiteSpace(reservaDto.StatusPagamento) || 
                    (reservaDto.StatusPagamento != "Pago" && reservaDto.StatusPagamento != "Pendente"))
                {
                    return false;
                }

                // Atualiza os campos da Reserva
                reserva.StatusPagamento = reservaDto.StatusPagamento;

                if (reservaDto.Viagem != null)
                {
                    if (reserva.Viagem == null)
                    {
                        reserva.Viagem = new Viagem();
                    }

                    // Atualiza o status
                    reserva.Viagem.Status = reservaDto.Viagem.Status ?? reserva.Viagem.Status;
                    if (reservaDto.Viagem.DestinoId != default(int))
                    {
                        reserva.Viagem.DestinoId = reservaDto.Viagem.DestinoId;
                    }
                    
            
                    // Atualiza as datas se elas forem fornecidas
                    if (reservaDto.Viagem.DataPartida != default(DateTime))
                    {
                        reserva.Viagem.DataPartida = reservaDto.Viagem.DataPartida;
                    }
            
                    if (reservaDto.Viagem.DataRetorno != default(DateTime))
                    {
                        reserva.Viagem.DataRetorno = reservaDto.Viagem.DataRetorno;
                    }
                }

                // Salva as alterações
                await _reservaRepository.UpdateAsync(reserva);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var reserva = await _reservaRepository.GetByIdAsync(id);
            if (reserva == null) return false;

            await _reservaRepository.DeleteAsync(id);
            return true;
        }
    }
}
