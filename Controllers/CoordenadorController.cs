using Microsoft.AspNetCore.Mvc;
using SAAU.Models;
using SAAU.Repositories;
using System.Threading.Tasks;

namespace SAAU.Controllers
{
    [ApiController]
    [Route("api/v1/coordenador")]
    public class CoordenadorController : Controller
    {
        private readonly CoordenadorRepository _coordenadorRepository;
        private readonly AgendamentoRepository _agendamentoRepository;
        public CoordenadorController
        (
            CoordenadorRepository coordenadorRepository,
            AgendamentoRepository agendamentoRepository
        )
        {
            _coordenadorRepository = coordenadorRepository;
            _agendamentoRepository = agendamentoRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() 
        {
            try 
            {
                var coordenadores = await _coordenadorRepository.Tudo();
                return Ok(coordenadores);
            } 
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("agendamento_id/{id}")]
        public async Task<IActionResult> GetAtendimetos([FromRoute] int id)
        {
            var agendamentosDoCoordenador = await _agendamentoRepository.AgendamentosCoordenador(id);
            return Ok(agendamentosDoCoordenador);
        }


        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Coordenador coordenador)
        {
            try
            {
                _coordenadorRepository.Adicionar(coordenador);
                return Ok("adicionado com sucesso");
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
