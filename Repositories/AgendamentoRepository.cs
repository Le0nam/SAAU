using Microsoft.EntityFrameworkCore;
using SAAU.Models;

namespace SAAU.Repositories
{
    public class AgendamentoRepository
    {
        private readonly AppDbContext _context;

        public AgendamentoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Agendamento>> Tudo()
        {
            return await _context.Agendamentos.Include(a => a.Aluno).Include(a => a.Coordenador).ToListAsync();
        }

        public async Task Adicionar(Agendamento agendamento)
        {
            _context.Agendamentos.Add(agendamento);
            _context.SaveChanges();
        }

        public async Task<List<Agendamento>> BuscarPorCoordenadorEData(int coordenadorId, DateTime data)
        {
            return await _context.Agendamentos
                .Where(a => a.CoordenadorId == coordenadorId && a.Data.Date == data.Date)
                .ToListAsync();
        }

        public async Task<List<Agendamento>> AgendamentosCoordenador(int id)
        {
            return await _context.Agendamentos.Where(a => a.CoordenadorId == id).ToListAsync();
        }

    }
}