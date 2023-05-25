using APIEscola.Repositorio.Interfaces;
using APIEscola.Repositorio.Factory;
using APIEscola.Model.Models;
using Dapper;
using System.Data;

namespace APIEscola.Repositorio.Repositorios
{
    public class RepositorioAluno : IRepositorioAluno
    {
        private readonly IDbConnection _connection;
        public RepositorioAluno()
        {
            _connection = new SqlFactory().SqlConnection();
        }

        public async Task<IEnumerable<AlunoModel>> BuscaAlunos()
        {
            var query = "SELECT * FROM ALUNO";

            using (_connection)
            {
                return await _connection.QueryAsync<AlunoModel>(query);
            }
        }

        public async Task<AlunoModel> BuscaAlunoId(int id)
        {
            var query = "SELECT * FROM ALUNO WHERE ID = @alunoId";
            var parameters = new
            {
                alunoId = id
            };

            using (_connection)
            {
                return await _connection.QueryFirstOrDefaultAsync<AlunoModel>(query, parameters);
            }
        }

        public async Task<bool> SalvaAluno(AlunoModel aluno)
        {
            int resultado;
            var query =
            @"INSERT INTO ALUNO
            VALUES
            (
                @nome,
                @usuario,
                @senha
            )";

            var parameters = new
            {
                nome = aluno.Nome,
                usuario = aluno.Usuario,
                senha = aluno.Senha
            };

            using (_connection)
            {
                resultado = await _connection.ExecuteAsync(query, parameters);
            }

            return resultado > 0;
        }

        public async Task<bool> AtualizaAluno(AlunoModel aluno)
        {
            int resultado;
            var query =
            @"UPDATE ALUNO
            SET
            NOME = @nome,
            USUARIO = @usuario,
            SENHA = @senha
            WHERE ID = @alunoId";

            var parameters = new
            {
                nome = aluno.Nome,
                usuario = aluno.Usuario,
                senha = aluno.Senha,
                alunoId = aluno.Id
            };

            using (_connection)
            {
                resultado = await _connection.ExecuteAsync(query, parameters);
            }

            return resultado > 0;
        }

        public async Task<bool> ExcluiAluno(int id)
        {
            int result;
            var query = "DELETE ALUNO WHERE ID = @alunoId";
            var parameters = new { alunoId = id };

            using (_connection)
            {
                result = await _connection.ExecuteAsync(query, parameters);
            }

            return result > 0;
        }
    }
}
