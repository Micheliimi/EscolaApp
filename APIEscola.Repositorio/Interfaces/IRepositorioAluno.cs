using APIEscola.Model.Models;

namespace APIEscola.Repositorio.Interfaces
{
    public interface IRepositorioAluno
    {
        Task<IEnumerable<AlunoModel>> BuscaAlunos();
        Task<AlunoModel> BuscaAlunoId(int id);
        Task<bool> SalvaAluno(AlunoModel aluno);
        Task<bool> AtualizaAluno(AlunoModel aluno);
        Task<bool> ExcluiAluno(int id);
    }
}