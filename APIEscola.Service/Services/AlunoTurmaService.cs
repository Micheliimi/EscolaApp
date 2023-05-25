using APIEscola.Model.DTO;
using APIEscola.Model.Models;
using APIEscola.Repositorio.Interfaces;
using APIEscola.Service.Interfaces;

namespace APIEscola.Service.Services
{
    public class AlunoTurmaService : IAlunoTurmaService
    {
        private readonly IRepositorioAlunoTurma _repositorio;

        public AlunoTurmaService(IRepositorioAlunoTurma repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<List<AlunoTurmaDTO>> BuscaAlunosTurmas()
        {
            var resultado = await _repositorio.BuscaAlunosTurmas();

            List<AlunoTurmaDTO> alunosTurmas = new List<AlunoTurmaDTO>();

            foreach (AlunoTurmaModel aluno in resultado)
            {
                alunosTurmas.Add(aluno.MapAlunoTurmaDTO());
            }

            return alunosTurmas;
        }
        public async Task<AlunoTurmaDTO> BuscaAlunoTurmaId(int alunoTurmaId)
        {
            AlunoTurmaModel alunoTurmaResposta = await _repositorio.BuscaAlunosTurmasId(alunoTurmaId);

            return alunoTurmaResposta.MapAlunoTurmaDTO();
        }

        public async Task<bool> SalvaAlunoTurma(AlunoTurmaDTO alunoTurma)
        {
            bool alunoEstaNaTurma = await VerificaSeAlunoEstaNaTurma(alunoTurma);

            if (!alunoEstaNaTurma)
            {
                return await _repositorio.SalvaAlunoTurma(alunoTurma.MapAlunoTurmaModel());
            }

            return false;
        }

        public async Task<bool> AtualizaAlunoTurma(AlunoTurmaDTO alunoTurma)
        {
            bool alunoEstaNaTurma = await VerificaSeAlunoEstaNaTurma(alunoTurma);

            if (alunoEstaNaTurma)
            {
                return await _repositorio.AtualizaAlunoTurma(alunoTurma.MapAlunoTurmaModel());
            }

            return false;
        }

        public async Task<bool> ExcluiAlunoTurma(int id)
        {
            return await _repositorio.ExcluiAlunoTurma(id);
        }

        internal async Task<bool> VerificaSeAlunoEstaNaTurma(AlunoTurmaDTO alunoTurma)
        {
            AlunoTurmaModel alunoTurmaResposta = await _repositorio.BuscaAlunosTurmasId(alunoTurma.AlunoTurmaId);

            return alunoTurmaResposta != null ? false : true;
        }
    }
}
