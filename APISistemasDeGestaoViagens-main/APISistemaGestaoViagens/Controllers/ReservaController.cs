using Microsoft.AspNetCore.Mvc;
using APISistemaGestaoViagens.Model.DTOs;
using APISistemaGestaoViagens.Services.Interfaces;

namespace APISistemaGestaoViagens.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ReservaController : ControllerBase
{
    private readonly IReservaService _reservaService;

    public ReservaController(IReservaService reservaService)
    {
        _reservaService = reservaService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ReservaDTO>>> GetAll()
    {
        var reservas = await _reservaService.GetAllAsync();
        return Ok(reservas);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ReservaDTO>> GetById(int id)
    {
        var reserva = await _reservaService.GetByIdAsync(id);
        if (reserva == null) return NotFound("Reserva não encontrada.");
        return Ok(reserva);
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] ReservaCreateDTO reservaCreateDto)
    {
        try
        {
            var reserva = await _reservaService.CreateAsync(reservaCreateDto);
            return CreatedAtAction(nameof(GetById), new { id = reserva.ReservaId }, reserva);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao criar reserva: {ex.Message}");
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, ReservaDTO reservaDto)
    {
        var updated = await _reservaService.UpdateAsync(id, reservaDto);
        if (!updated) return NotFound("Reserva não encontrada.");

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var deleted = await _reservaService.DeleteAsync(id);
        if (!deleted) return NotFound("Reserva não encontrada.");

        return NoContent();
    }
}