using APIEscola.Model.Models;

namespace APIEscola.Model.DTO
{
    public class AlunoTurmaDTO
    {
        public int AlunoTurmaId { get; set; }
        public int AlunoId { get; set; }
        public string? AlunoNome { get; set; }
        public int TurmaId { get; set; }
        public string? TurmaNome { get; set; }

        public AlunoTurmaModel MapAlunoTurmaModel()
        {
            return new AlunoTurmaModel
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
