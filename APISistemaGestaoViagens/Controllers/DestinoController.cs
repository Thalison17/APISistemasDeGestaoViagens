using Microsoft.AspNetCore.Mvc;
using APISistemaGestaoViagens.Model.Entities;
using APISistemaGestaoViagens.Repository.Interfaces;
using APISistemaGestaoViagens.Model.DTOs;

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
    public async Task<ActionResult<IEnumerable<DestinoDTO>>> GetAll()
    {
        try
        {
            var destinos = await _repository.GetAllAsync();
            var destinosDto = destinos.Select(d => new DestinoDTO
            {
                DestinoId = d.DestinoId,
                Localizacao = d.Localizacao,
                Pais = d.Pais,
                PrecoPorDia = d.PrecoPorDia
            });
            return Ok(destinosDto);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao obter destinos: {ex.Message}");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<DestinoDTO>> GetById(int id)
    {
        try
        {
            var destino = await _repository.GetByIdAsync(id);
            if (destino == null) return NotFound("Destino não encontrado.");

            var destinoDto = new DestinoDTO
            {
                DestinoId = destino.DestinoId,
                Localizacao = destino.Localizacao,
                Pais = destino.Pais,
                PrecoPorDia = destino.PrecoPorDia
            };

            return Ok(destinoDto);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao obter destino: {ex.Message}");
        }
    }

    [HttpPost]
    public async Task<ActionResult> Create(Destino destino)
    {
        await _repository.AddAsync(destino);
        return CreatedAtAction(nameof(GetById), new { id = destino.DestinoId }, destino);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, DestinoDTO destinoDto)
    {
        if (id != destinoDto.DestinoId)
            return BadRequest("ID do destino não corresponde.");
        
        var destino = await _repository.GetByIdAsync(id);
        if (destino == null)
            return NotFound("Destino não encontrado.");
        
        if (destinoDto.PrecoPorDia <= 0)
        {
            return BadRequest("O preço por dia deve ser maior que zero.");
        }
        destino.PrecoPorDia = destinoDto.PrecoPorDia;
        
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