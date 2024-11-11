using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using APISistemaGestaoViagens.Model.Entities;
using APISistemaGestaoViagens.Repository.Interfaces;
using APISistemaGestaoViagens.Model.DTOs;

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
        try
        {
            var reservas = await _repository.GetAllAsync();
            var reservasDto = reservas.Select(r => new ReservaDTO
            {
                ReservaId = r.ReservaId,
                DataReserva = r.DataReserva,
                StatusPagamento = r.StatusPagamento,
                MetodoPagamento = r.MetodoPagamento
            });
            return Ok(reservasDto);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao obter reservas: {ex.Message}");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ReservaDTO>> GetById(int id)
    {
        try
        {
            var reserva = await _repository.GetByIdAsync(id);
            if (reserva == null) return NotFound("Reserva não encontrada.");

            var reservaDto = new ReservaDTO
            {
                ReservaId = reserva.ReservaId,
                DataReserva = reserva.DataReserva,
                StatusPagamento = reserva.StatusPagamento,
                MetodoPagamento = reserva.MetodoPagamento
            };

            return Ok(reservaDto);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao obter reserva: {ex.Message}");
        }
    }

    [HttpPost]
    public async Task<ActionResult> Create(Reserva reserva)
    {
        await _repository.AddAsync(reserva);
        return CreatedAtAction(nameof(GetById), new { id = reserva.ReservaId }, reserva);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, Reserva reserva)
    {
        if (id != reserva.ReservaId) return BadRequest();
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
