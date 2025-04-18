using Microsoft.EntityFrameworkCore;
using SAAU.Models;

namespace SAAU.Repositories
{
    public class AlunoRepository
    {
        private readonly AppDbContext _context;

        public AlunoRepository(AppDbContext context)
        {
            _context = context;
        }

        public Task<List<Aluno>> Tudo()
        {
            return _context.Alunos.ToListAsync();
        }

        public async Task Adicionar(Aluno aluno)
        {
            _context.Alunos.Add(aluno);
            _context.SaveChanges();
        }

        public void Atualizar(Aluno aluno)
        {

        }
    }
}
