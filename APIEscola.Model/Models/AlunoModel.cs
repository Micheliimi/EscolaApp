using APIEscola.Model.DTO;

namespace APIEscola.Model.Models
{
    public class AlunoModel
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? Usuario { get; set; }
        public string? Senha { get; set; }

        public AlunoDTO MapAlunoDTO()
        {
            return new AlunoDTO
            {
                Id = Id,
                Nome = Nome,
                Usuario = Usuario
            };
        }
    }
}
