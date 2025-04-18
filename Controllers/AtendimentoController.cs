using Microsoft.AspNetCore.Mvc;
using SAAU.Models;
using SAAU.Repositories;

namespace SAAU.Controllers
{
    [ApiController]
    [Route("api/v1/atendimento")]
    public class AtendimentoController : Controller
    {
        private readonly AtendimentoRepository _atendimentoRepository;

        public AtendimentoController(AtendimentoRepository atendimentoRepository)
        {
            _atendimentoRepository = atendimentoRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var atendimentos = await _atendimentoRepository.Tudo();
                return Ok(atendimentos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Atendimento atendimento)
        {
            try
            {
                _atendimentoRepository.Adicionar(atendimento);
                return Ok("adicionado com sucesso");
            }
            catch (Exception ex)
            { 
                return BadRequest(ex.Message);
            }
        }
    }
}
