using APIEscola.Model.DTO;

namespace APIEscola.Model.Models
{
    public class AlunoTurmaModel
    {
        public int AlunoTurmaId { get; set; }
        public int AlunoId { get; set; }
        public string? AlunoNome { get; set; }
        public int TurmaId { get; set; }
        public string? TurmaNome { get; set; }
        public AlunoTurmaDTO MapAlunoTurmaDTO()
        {
            return new AlunoTurmaDTO
            {
                AlunoTurmaId = AlunoTurmaId,
                AlunoId = AlunoId,
                AlunoNome = AlunoNome,
                TurmaId = TurmaId,
                TurmaNome = TurmaNome
            };
        }
    }
}
