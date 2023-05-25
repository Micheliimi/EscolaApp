using APIEscola.Model.DTO;
using APIEscola.Model.Models;
using APIEscola.Repositorio.Interfaces;
using APIEscola.Service.Interfaces;

namespace APIEscola.Service.Services
{
    public class TurmaService : ITurmaService
    {
        private readonly IRepositorioTurma _repositorio;

        public TurmaService(IRepositorioTurma repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<List<TurmaDTO>> BuscaTurmas()
        {
            var resultado = await _repositorio.BuscaTurmas();

            List<TurmaDTO> turmas = new List<TurmaDTO>();

            foreach (TurmaModel turma in resultado)
            {
                turmas.Add(turma.MapTurmaDTO());
            }

            return turmas;
        }

        public async Task<TurmaDTO> BuscaTurmaId(int id)
        {
            TurmaModel turma = await _repositorio.BuscaTurmaId(id);

            return turma.MapTurmaDTO();
        }

        public async Task<bool> SalvaTurma(TurmaDTO turma)
        {
            bool turmaTemCadastro = await VerificaSeTurmaTemCadastro(turma);

            if (!turmaTemCadastro)
            {
                return await _repositorio.SalvaTurma(turma.MapTurmaModel());
            }

            return false;
        }

        public async Task<bool> AtualizaTurma(TurmaDTO turma)
        {
            bool turmaTemCadastro = await VerificaSeTurmaTemCadastro(turma);

            if (turmaTemCadastro)
            {
                return await _repositorio.AtualizaTurma(turma.MapTurmaModel());
            }

            return false;
        }

        public async Task<bool> ExcluiTurma(int id)
        {
            return await _repositorio.ExcluiTurma(id);
        }

        internal async Task<bool> VerificaSeTurmaTemCadastro(TurmaDTO turma)
        {
            var resultado = await _repositorio.BuscaTurmaId(turma.Id);

            return resultado != null ? false : true;
        }
    }
}
