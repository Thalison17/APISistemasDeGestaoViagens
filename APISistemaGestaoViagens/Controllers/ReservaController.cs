using Microsoft.AspNetCore.Mvc;
using APISistemaGestaoViagens.Model.Entities;
using APISistemaGestaoViagens.Model.DTOs;
using APISistemaGestaoViagens.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace APISistemaGestaoViagens.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ReservaController : ControllerBase
{
    private readonly IGenericRepository<Reserva> _reservaRepository;
    private readonly IGenericRepository<Viagem> _viagemRepository;
    private readonly IGenericRepository<Destino> _destinoRepository;

    public ReservaController(
        IGenericRepository<Reserva> reservaRepository,
        IGenericRepository<Viagem> viagemRepository,
        IGenericRepository<Destino> destinoRepository)
    {
        _reservaRepository = reservaRepository;
        _viagemRepository = viagemRepository;
        _destinoRepository = destinoRepository;
    }


    [HttpGet]
    public async Task<ActionResult<IEnumerable<ReservaDTO>>> GetAll()
    {
        var reservas = await _reservaRepository.GetAllAsync(include: query => query.Include(r => r.Viagem));

        var reservasDto = reservas.Select(r => new ReservaDTO
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

        return Ok(reservasDto);
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<ReservaDTO>> GetById(int id)
    {
        var reserva = await _reservaRepository.GetByIdAsync(
            id,
            include: query => query.Include(r => r.Viagem));

        if (reserva == null) return NotFound("Reserva não encontrada.");

        var reservaDto = new ReservaDTO
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

        return Ok(reservaDto);
    }


    [HttpPost]
    public async Task<ActionResult> Create([FromBody] ReservaCreateDTO reservaCreateDto)
    {
        try
        {
            var destino = await _destinoRepository.GetByIdAsync(reservaCreateDto.Viagem.DestinoId);
            if (destino == null)
                return NotFound("Destino não encontrado.");
            
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
            
            return CreatedAtAction(nameof(GetById), new { id = reserva.ReservaId }, new ReservaDTO
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
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao criar reserva: {ex.Message}");
        }
    }

    
    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, ReservaDTO reservaDto)
    {
        if (id != reservaDto.ReservaId)
            return BadRequest("ID da reserva não corresponde.");
        
        var reserva = await _reservaRepository.GetByIdAsync(id);
        if (reserva == null)
            return NotFound("Reserva não encontrada.");
        
        if (string.IsNullOrWhiteSpace(reservaDto.StatusPagamento) || 
            (reservaDto.StatusPagamento != "Pago" && reservaDto.StatusPagamento != "Pendente"))
        {
            return BadRequest("StatusPagamento inválido.");
        }
        reserva.StatusPagamento = reservaDto.StatusPagamento;
        
        await _reservaRepository.UpdateAsync(reserva);
        
        return NoContent();
    }


    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await _reservaRepository.DeleteAsync(id);
        return NoContent();
    }
}
