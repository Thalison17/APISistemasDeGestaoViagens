using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using APISistemaGestaoViagens.Model.Entities;
using APISistemaGestaoViagens.Repository.Interfaces;

namespace APISistemaGestaoViagens.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ViagemController : ControllerBase
{
    private readonly IGenericRepository<Viagem> _repository;

    public ViagemController(IGenericRepository<Viagem> repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Viagem>>> GetAll()
    {
        var viagens = await _repository.GetAllAsync();
        return Ok(viagens);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Viagem>> GetById(int id)
    {
        var viagem = await _repository.GetByIdAsync(id);
        if (viagem == null) return NotFound();
        return Ok(viagem);
    }

    [HttpPost]
    public async Task<ActionResult> Create(Viagem viagem)
    {
        await _repository.AddAsync(viagem);
        return CreatedAtAction(nameof(GetById), new { id = viagem.ViagemId }, viagem);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, Viagem viagem)
    {
        if (id != viagem.ViagemId) return BadRequest();
        await _repository.UpdateAsync(viagem);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await _repository.DeleteAsync(id);
        return NoContent();
    }
}
