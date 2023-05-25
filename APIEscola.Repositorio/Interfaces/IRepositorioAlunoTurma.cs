using APIEscola.Model.Models;

namespace APIEscola.Repositorio.Interfaces
{
    public interface IRepositorioAlunoTurma
    {
        Task<IEnumerable<AlunoTurmaModel>> BuscaAlunosTurmas();
        Task<AlunoTurmaModel> BuscaAlunosTurmasId(int alunoTurmaId);
        Task<bool> SalvaAlunoTurma(AlunoTurmaModel alunoTurma);
        Task<bool> AtualizaAlunoTurma(AlunoTurmaModel alunoTurma);
        Task<bool> ExcluiAlunoTurma(int id);
    }
}