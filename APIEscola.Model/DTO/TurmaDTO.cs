using APIEscola.Model.Models;

namespace APIEscola.Model.DTO
{
    public class TurmaDTO
    {
        public int Id { get; set; }
        public int CursoId { get; set; }
        public string? Turma { get; set; }
        public int Ano { get; set; }

        public TurmaModel MapeiaTurmaModel()
        {
            return new TurmaModel
            {
                Id = Id,
                CursoId = CursoId,
                Turma = Turma,
                Ano = Ano
            };
        }
    }
}
