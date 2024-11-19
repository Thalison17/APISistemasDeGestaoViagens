using APISistemaGestaoViagens.Services;  // Importando o ReportService
using Microsoft.AspNetCore.Mvc;

namespace APISistemaGestaoViagens.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RelatorioController : ControllerBase
    {
        private readonly ReportService _reportService;

        public RelatorioController(ReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet("reservas-por-periodo")]
        public IActionResult GetReservasPorPeriodo()
        {
            var result = _reportService.ObterReservasPorPeriodo();
            return Ok(result);  
        }

        [HttpGet("destinos-mais-procurados")]
        public IActionResult GetDestinosMaisProcurados()
        {
            var result = _reportService.ObterDestinosMaisProcurados();
            return Ok(result);
        }

        [HttpGet("clientes-frequentes")]
        public IActionResult GetClientesFrequentes()
        {
            var result = _reportService.ObterClientesFrequentes();
            return Ok(result);
        }

        [HttpGet("receita-por-viagem")]
        public IActionResult GetReceitaPorViagem()
        {
            var result = _reportService.ObterReceitaPorViagem();
            return Ok(result);
        }
    }
}
