using Microsoft.AspNetCore.Mvc;
using APISistemaGestaoViagens.Model.DTOs;
using APISistemaGestaoViagens.Services.Interfaces;

namespace APISistemaGestaoViagens.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ViagemController : ControllerBase
{
    private readonly IViagemService _viagemService;

    public ViagemController(IViagemService viagemService)
    {
        _viagemService = viagemService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ViagemDTO>>> GetAll()
    {
        var viagens = await _viagemService.GetAllAsync();
        return Ok(viagens);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ViagemDTO>> GetById(int id)
    {
        var viagem = await _viagemService.GetByIdAsync(id);
        if (viagem == null) return NotFound("Viagem não encontrada.");
        return Ok(viagem);
    }
}