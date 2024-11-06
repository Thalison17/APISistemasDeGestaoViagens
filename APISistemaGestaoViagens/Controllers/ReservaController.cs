using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using APISistemaGestaoViagens.Model.Entities;
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
    public async Task<ActionResult<IEnumerable<Reserva>>> GetAll()
    {
        var reservas = await _repository.GetAllAsync();
        return Ok(reservas);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Reserva>> GetById(int id)
    {
        var reserva = await _repository.GetByIdAsync(id);
        if (reserva == null) return NotFound();
        return Ok(reserva);
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
