using Microsoft.AspNetCore.Mvc;
using SAAU.Models;
using SAAU.Repositories;

namespace SAAU.Controllers
{
    [ApiController]
    [Route("api/v1/agendamento")]
    public class AgendamentoController : Controller
    {
        private readonly AgendamentoRepository _agendamentoRepository;
        private readonly CoordenadorRepository _coordenadorRepository;
        private readonly AtendimentoRepository _atendimentoRepository;

        public AgendamentoController
        (
            AgendamentoRepository agendamentoRepository,
            CoordenadorRepository coordenadorRepository,
            AtendimentoRepository atendimentoRepository
        )
        {
            _agendamentoRepository = agendamentoRepository;
            _coordenadorRepository = coordenadorRepository;
            _atendimentoRepository = atendimentoRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var agendamento = await _agendamentoRepository.Tudo();
                return Ok(agendamento);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpPost]

        public async Task<IActionResult> Add([FromBody] Agendamento agendamento)
        {
            try
            {
                if (agendamento.CoordenadorId == null)
                {
                    return BadRequest();
                }
                else
                {
                    agendamento.HoraFim = agendamento.HoraInicio.Add(TimeSpan.FromMinutes(30));

                    agendamento.Coordenador = await _coordenadorRepository.BuscarPorId(agendamento.CoordenadorId);

                    agendamento.Coordenador.Atendimentos = await _atendimentoRepository.BuscarPorCoordenadorId(agendamento.Coordenador.Id);

                    var Atendimentos = agendamento.Coordenador.Atendimentos;
                    var diaSemana = agendamento.Data.DayOfWeek;

                    var atendimentoNoDia = agendamento.Coordenador.Atendimentos
                        .FirstOrDefault(a => a.DiaDaSemana == diaSemana);

                    if (atendimentoNoDia == null)
                    {
                        return BadRequest();
                    }

                    var agendamentosExistentes = await _agendamentoRepository.BuscarPorCoordenadorEData(agendamento.CoordenadorId, agendamento.Data.Date);

                    bool conflito = agendamentosExistentes.Any(a =>
                        (agendamento.HoraInicio < a.HoraFim) &&
                        (agendamento.HoraFim > a.HoraInicio)
                    );

                    if (conflito)
                    {
                        return BadRequest("Horario não disponivel");
                    }

                    bool dentroDoHorario = agendamento.HoraInicio >= atendimentoNoDia.HoraInicio &&
                                           agendamento.HoraFim <= atendimentoNoDia.HoraFim;
                    if(dentroDoHorario)
                    {
                        _agendamentoRepository.Adicionar(agendamento);
                        return Ok();
                    }
                    return BadRequest("Horario não disponivel");

                }

            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
