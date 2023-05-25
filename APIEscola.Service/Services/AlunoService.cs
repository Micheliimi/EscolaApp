using APIEscola.Model.DTO;
using APIEscola.Model.Models;
using APIEscola.Repositorio.Interfaces;
using APIEscola.Repositorio.Repositorios;
using APIEscola.Service.Interfaces;

namespace APIEscola.Service.Services
{
    public class AlunoService : IAlunoService
    {
        private readonly IRepositorioAluno _repositorio;

        public AlunoService(IRepositorioAluno repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<List<AlunoDTO>> BuscaAlunos()
        {
            var resultado = await _repositorio.BuscaAlunos();

            List<AlunoDTO> alunos = new List<AlunoDTO>();

            foreach (AlunoModel aluno in resultado)
            {
                alunos.Add(aluno.MapAlunoDTO());
            }

            return alunos;
        }

        public async Task<AlunoDTO> BuscaAlunoId(int id)
        {
            AlunoModel aluno = await _repositorio.BuscaAlunoId(id);
            return aluno.MapAlunoDTO();
        }

        public async Task<bool> SalvaAluno(AlunoDTO aluno)
        {
            bool alunoTemCadastro = await VerificaSeAlunoTemCadastro(aluno);

            if (!alunoTemCadastro)
            {
                return await _repositorio.SalvaAluno(aluno.MapAlunoModel());
            }

            return false;
        }

        public async Task<bool> AtualizaAluno(AlunoDTO aluno)
        {
            bool alunoTemCadastro = await VerificaSeAlunoTemCadastro(aluno);

            if (alunoTemCadastro)
            {
                return await _repositorio.AtualizaAluno(aluno.MapAlunoModel());
            }

            return false;
        }

        public async Task<bool> ExcluiAluno(int id)
        {
            return await _repositorio.ExcluiAluno(id);
        }

        private async Task<bool> VerificaSeAlunoTemCadastro(AlunoDTO aluno)
        {
            var resultado = await _repositorio.BuscaAlunoId(aluno.Id);

            return resultado != null ? false : true;
        }
    }
}
