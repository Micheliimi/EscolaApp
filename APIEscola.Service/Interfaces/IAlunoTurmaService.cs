using APIEscola.Model.DTO;

namespace APIEscola.Service.Interfaces
{
    public interface IAlunoTurmaService
    {
        Task<List<AlunoTurmaDTO>> BuscaAlunosTurmas();
        Task<AlunoTurmaDTO> BuscaAlunoTurmaId(int alunoTurmaId);
        Task<bool> SalvaAlunoTurma(AlunoTurmaDTO alunoTurma);
        Task<bool> AtualizaAlunoTurma(AlunoTurmaDTO alunoTurma);
        Task<bool> ExcluiAlunoTurma(int id);
    }
}