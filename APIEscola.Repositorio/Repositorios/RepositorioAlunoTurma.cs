using APIEscola.Model.DTO;
using APIEscola.Model.Models;
using APIEscola.Repositorio.Factory;
using APIEscola.Repositorio.Interfaces;
using Dapper;
using System.Data;

namespace APIEscola.Repositorio.Repositorios
{
    public class RepositorioAlunoTurma : IRepositorioAlunoTurma
    {
        private readonly IDbConnection _connection;
        public RepositorioAlunoTurma()
        {
            _connection = new SqlFactory().SqlConnection();
        }

        public async Task<IEnumerable<AlunoTurmaModel>> BuscaAlunosTurmas()
        {
            var query =
                @"SELECT alunoturma.id, alunoturma.aluno_id, aluno.nome, alunoturma.turma_id, turma.nome FROM alunoturma
                INNER JOIN aluno
                ON alunoturma.aluno_id = aluno.id
                INNER JOIN TURMA
                ON alunoturma.turma_id = turma.id";

            using (_connection)
            {
                return await _connection.QueryAsync<AlunoTurmaModel>(query);
            }
        }

        public async Task<AlunoTurmaModel> BuscaAlunosTurmasId(int alunoTurmaId)
        {
            var query =
                @"SELECT alunoturma.id, alunoturma.aluno_id, aluno.nome, alunoturma.turma_id, turma.nome FROM alunoturma
                    INNER JOIN aluno
                    ON alunoturma.aluno_id = aluno.id
                    INNER JOIN TURMA
                    ON alunoturma.turma_id = turma.id
                WHERE alunoturma.id = @id";

            var parameters = new
            {
                id = alunoTurmaId,
            };

            using (_connection)
            {
                return await _connection.QueryFirstOrDefaultAsync<AlunoTurmaModel>(query, parameters);
            }
        }

        public async Task<bool> SalvaAlunoTurma(AlunoTurmaModel alunoTurma)
        {
            int result;
            var query =
            @"INSERT INTO alunoturma (
            aluno_id,
            turma_id
            )
            VALUES
            (
                @alunoId,
                @turmaId,
            )";

            var parameters = new
            {
                alunoId = alunoTurma.AlunoId,
                turmaId = alunoTurma.TurmaId
            };

            using (_connection)
            {
                result = await _connection.ExecuteAsync(query, parameters);
            }

            return result > 0;
        }

        public async Task<bool> AtualizaAlunoTurma(AlunoTurmaModel alunoTurma)
        {
            int result;
            var query =
            @"UPDATE alunoturma
            SET aluno_id = @alunoId, turma_id = @turmaId
            WHERE id = @id";

            var parameters = new
            {
                id = alunoTurma.AlunoTurmaId,
                alunoId = alunoTurma.AlunoId,
                turmaId = alunoTurma.TurmaId
            };

            using (_connection)
            {
                result = await _connection.ExecuteAsync(query, parameters);
            }

            return result > 0;
        }

        public async Task<bool> ExcluiAlunoTurma(int id)
        {
            int resultado;
            var query = "DELETE alunoturma WHERE id = @id";
            var parameters = new { id = id };

            using (_connection)
            {
                resultado = await _connection.ExecuteAsync(query, parameters);
            }

            return resultado > 0;
        }
    }
}
