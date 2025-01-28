using Microsoft.AspNetCore.Mvc;
using APISistemaGestaoViagens.Model.DTOs;
using APISistemaGestaoViagens.Services.Interfaces;

namespace APISistemaGestaoViagens.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DestinoController : ControllerBase
{
    private readonly IDestinoService _destinoService;

    public DestinoController(IDestinoService destinoService)
    {
        _destinoService = destinoService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<DestinoDTO>>> GetAll()
    {
        try
        {
            var destinos = await _destinoService.GetAllAsync();
            return Ok(destinos);
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
            var destino = await _destinoService.GetByIdAsync(id);
            if (destino == null) return NotFound("Destino não encontrado.");

            return Ok(destino);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao obter destino: {ex.Message}");
        }
    }

    [HttpPost]
    public async Task<ActionResult<DestinoDTO>> Create(DestinoCreateDTO destinoDto)
    {
        try
        {
            var destino = await _destinoService.CreateAsync(destinoDto);
            return CreatedAtAction(nameof(GetById), new { id = destino.DestinoId }, destino);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao criar destino: {ex.Message}");
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, DestinoDTO destinoDto)
    {
        try
        {
            var updated = await _destinoService.UpdateAsync(id, destinoDto);
            if (!updated) return NotFound("Destino não encontrado.");

            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao atualizar destino: {ex.Message}");
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            var deleted = await _destinoService.DeleteAsync(id);
            if (!deleted) return NotFound("Destino não encontrado.");

            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao deletar destino: {ex.Message}");
        }
    }
}
