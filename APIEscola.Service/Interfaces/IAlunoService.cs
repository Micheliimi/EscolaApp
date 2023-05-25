using APIEscola.Model.DTO;

namespace APIEscola.Service.Interfaces
{
    public interface IAlunoService
    {
        Task<List<AlunoDTO>> BuscaAlunos();
        Task<AlunoDTO> BuscaAlunoId(int id);
        Task<bool> SalvaAluno(AlunoDTO aluno);
        Task<bool> AtualizaAluno(AlunoDTO aluno);
        Task<bool> ExcluiAluno(int id);
    }
}