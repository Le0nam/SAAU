using Microsoft.EntityFrameworkCore;
using SAAU.Models;

namespace SAAU.Repositories
{
    public class CoordenadorRepository
    {
        private readonly AppDbContext _context;

        public CoordenadorRepository(AppDbContext context)
        {
            _context = context;
        }

        public Task<List<Coordenador>> Tudo()
        {
            return _context.Coordenadores.Include(c => c.Atendimentos).ToListAsync();
        }

        public async Task Adicionar(Coordenador coordenador)
        {
            _context.Coordenadores.Add(coordenador);
            _context.SaveChanges();
        }

        public Task<Coordenador> BuscarPorId(int id)
        {
            return _context.Coordenadores.Where(c => c.Id == id).FirstOrDefaultAsync();
        }
    }
}
