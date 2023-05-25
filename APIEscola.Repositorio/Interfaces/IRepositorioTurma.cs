using APIEscola.Model.Models;

namespace APIEscola.Repositorio.Interfaces
{
    public interface IRepositorioTurma
    {
        Task<IEnumerable<TurmaModel>> BuscaTurmas();
        Task<TurmaModel> BuscaTurmaId(int id);
        Task<bool> SalvaTurma(TurmaModel turma);
        Task<bool> AtualizaTurma(TurmaModel turma);
        Task<bool> ExcluiTurma(int id);
    }
}