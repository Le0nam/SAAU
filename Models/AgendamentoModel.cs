namespace SAAU.Models
{
    public class Agendamento
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan? HoraFim { get; set; }
        public string Descricao { get; set; }
        public int AlunoId { get; set; }
        public Aluno? Aluno { get; set; }
        public int CoordenadorId { get; set; }
        public Coordenador? Coordenador { get; set; }
    }
}
