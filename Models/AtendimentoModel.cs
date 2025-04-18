namespace SAAU.Models
{
    public class Atendimento
    {
        public int Id { get; set; }
        public DayOfWeek DiaDaSemana { get; set; }  // Aqui está o dia da semana
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFim { get; set; }
        public int CoordenadorId { get; set; }
        public Coordenador? Coordenador { get; set; }
    }

}
