using Microsoft.EntityFrameworkCore;
using SAAU.Models;

namespace SAAU.Repositories
{
    public class AtendimentoRepository
    {
        private readonly AppDbContext _context;

        public AtendimentoRepository(AppDbContext context)
        {
            _context = context;
        }

        public Task<List<Atendimento>> Tudo()
        {
            return _context.Atendimentos.Include(a => a.Coordenador).ToListAsync();
        }

        public async Task Adicionar(Atendimento atendimento)
        {
            _context.Atendimentos.Add(atendimento);
            _context.SaveChanges();
        }

        public Task<List<Atendimento>> BuscarPorCoordenadorId(int id)
        {
            return _context.Atendimentos.Where(a => a.CoordenadorId == id).ToListAsync();
        }
    }
}
