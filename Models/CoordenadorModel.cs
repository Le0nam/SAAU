namespace SAAU.Models
{
    public class Coordenador
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public List<Atendimento>? Atendimentos { get; set; }
    }
}