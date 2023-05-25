using APIEscola.Model.Models;

namespace APIEscola.Model.DTO
{
    public class AlunoDTO
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? Usuario { get; set; }
        public string? Senha { get; set; }

        public AlunoModel MapAlunoModel()
        {
            return new AlunoModel
            {
                Id = Id,
                Nome = Nome,
                Usuario = Usuario
            };
        }
    }
}
