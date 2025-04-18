using Microsoft.AspNetCore.Mvc;
using SAAU.Models;
using SAAU.Repositories;

namespace SAAU.Controllers
{
    [ApiController]
    [Route("api/v1/aluno")]
    public class AlunoController : Controller
    {
        private readonly AlunoRepository _alunoRepository;

        public AlunoController(AlunoRepository alunoRepository)
        {
            _alunoRepository = alunoRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var alunos = _alunoRepository.Tudo();
                return Ok(alunos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Aluno aluno)
        {
            try
            {
                _alunoRepository.Adicionar(aluno);
                return Ok("adicionado com sucesso");
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
