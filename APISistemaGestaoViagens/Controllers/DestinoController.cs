using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using APISistemaGestaoViagens.Model.Entities;
using APISistemaGestaoViagens.Repository.Interfaces;

namespace APISistemaGestaoViagens.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DestinoController : ControllerBase
{
    private readonly IGenericRepository<Destino> _repository;

    public DestinoController(IGenericRepository<Destino> repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Destino>>> GetAll()
    {
        var destinos = await _repository.GetAllAsync();
        return Ok(destinos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Destino>> GetById(int id)
    {
        var destino = await _repository.GetByIdAsync(id);
        if (destino == null) return NotFound();
        return Ok(destino);
    }

    [HttpPost]
    public async Task<ActionResult> Create(Destino destino)
    {
        await _repository.AddAsync(destino);
        return CreatedAtAction(nameof(GetById), new { id = destino.DestinoId }, destino);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, Destino destino)
    {
        if (id != destino.DestinoId) return BadRequest();
        await _repository.UpdateAsync(destino);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await _repository.DeleteAsync(id);
        return NoContent();
    }
}