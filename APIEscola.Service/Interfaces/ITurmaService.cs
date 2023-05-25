using APIEscola.Model.DTO;

namespace APIEscola.Service.Interfaces
{
    public interface ITurmaService
    {
        Task<List<TurmaDTO>> BuscaTurmas();
        Task<TurmaDTO> BuscaTurmaId(int id);
        Task<bool> SalvaTurma(TurmaDTO turma);
        Task<bool> AtualizaTurma(TurmaDTO turma);
        Task<bool> ExcluiTurma(int id);
    }
}