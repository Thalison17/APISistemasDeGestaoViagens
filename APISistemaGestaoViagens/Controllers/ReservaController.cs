using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APISistemaGestaoViagens.Model.Entities;
using APISistemaGestaoViagens.Model.DTOs;
using APISistemaGestaoViagens.Repository.Interfaces;

namespace APISistemaGestaoViagens.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ReservaController : ControllerBase
{
    private readonly IGenericRepository<Reserva> _repository;

    public ReservaController(IGenericRepository<Reserva> repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ReservaDTO>>> GetAll()
    {
        var reservas = await _repository.GetAllAsync();
        var reservasDto = reservas.Select(r => new ReservaDTO
        {
            ReservaId = r.ReservaId,
            ClienteId = r.ClienteId,
            ViagemId = r.ViagemId,
            DataReserva = r.DataReserva,
            StatusPagamento = r.StatusPagamento,
            MetodoPagamento = r.MetodoPagamento
        });
        return Ok(reservasDto);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ReservaDTO>> GetById(int id)
    {
        var reserva = await _repository.GetByIdAsync(id);
        if (reserva == null) return NotFound("Reserva não encontrada.");

        var reservaDto = new ReservaDTO
        {
            ReservaId = reserva.ReservaId,
            ClienteId = reserva.ClienteId,
            ViagemId = reserva.ViagemId,
            DataReserva = reserva.DataReserva,
            StatusPagamento = reserva.StatusPagamento,
            MetodoPagamento = reserva.MetodoPagamento
        };

        return Ok(reservaDto);
    }

    [HttpPost]
    public async Task<ActionResult> Create(ReservaDTO reservaDto)
    {
        try
        {
            var reserva = new Reserva
            {
                ClienteId = reservaDto.ClienteId,
                ViagemId = reservaDto.ViagemId,
                DataReserva = reservaDto.DataReserva,
                StatusPagamento = reservaDto.StatusPagamento,
                MetodoPagamento = reservaDto.MetodoPagamento
            };

            _repository.AddAsync(reserva); 
            
            var reservaDto2 = new ReservaDTO
            {
                ReservaId = reserva.ReservaId,
                ViagemId = reserva.ViagemId,
                DataReserva = reserva.DataReserva,
                StatusPagamento = reserva.StatusPagamento,
                MetodoPagamento = reserva.MetodoPagamento,
                ClienteId = reserva.ClienteId
            };

            return CreatedAtAction(nameof(GetById), new { id = reserva.ReservaId }, reservaDto);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao criar reserva: {ex.Message}");
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, ReservaDTO reservaDto)
    {
        if (id != reservaDto.ReservaId) return BadRequest("ID da reserva não corresponde.");

        var reserva = new Reserva
        {
            ReservaId = reservaDto.ReservaId,
            ClienteId = reservaDto.ClienteId,
            ViagemId = reservaDto.ViagemId,
            DataReserva = reservaDto.DataReserva,
            StatusPagamento = reservaDto.StatusPagamento,
            MetodoPagamento = reservaDto.MetodoPagamento
        };

        await _repository.UpdateAsync(reserva);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await _repository.DeleteAsync(id);
        return NoContent();
    }
}
