using APIEscola.Model.DTO;

namespace APIEscola.Model.Models
{
    public class TurmaModel
    {
        public int Id { get; set; }
        public int CursoId { get; set; }
        public string? Turma { get; set; }
        public int Ano { get; set; }

        public TurmaDTO MapeiaTurmaDTO()
        {
            return new TurmaDTO
            {
                Id = Id,
                CursoId = CursoId,
                Turma = Turma,
                Ano = Ano
            };
        }
    }
}
