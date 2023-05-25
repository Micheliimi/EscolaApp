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
                @"SELECT ALUNOTURMA.ID, ALUNOTURMA.ALUNO_ID, ALUNO.NOME, ALUNOTURMA.TURMA_ID, TURMA.NOME FROM ALUNOTURMA
                INNER JOIN ALUNO
                ON ALUNOTURMA.ALUNO_ID = ALUNO.ID
                INNER JOIN TURMA
                ON ALUNOTURMA.TURMA_ID = TURMA.ID";

            using (_connection)
            {
                return await _connection.QueryAsync<AlunoTurmaModel>(query);
            }
        }

        public async Task<AlunoTurmaModel> BuscaAlunosTurmasId(int alunoTurmaId)
        {
            var query =
                @"SELECT ALUNOTURMA.ID, ALUNOTURMA.ALUNO_ID, ALUNO.NOME, ALUNOTURMA.TURMA_ID, TURMA.NOME FROM ALUNOTURMA
                    INNER JOIN ALUNO
                    ON ALUNOTURMA.ALUNO_ID = ALUNO.ID
                    INNER JOIN TURMA
                    ON ALUNOTURMA.TURMA_ID = TURMA.ID
                WHERE ALUNOTURMA.ID = @id";

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
            @"INSERT INTO ALUNOTURMA (
            ALUNO_ID,
            TURMA.ID
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
            @"UPDATE ALUNOTURMA
            SET ALUNO_ID = @alunoId,
            TURMA.ID = @turmaId
            WHERE ID = @id";

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
            var query = "DELETE ALUNOTURMA WHERE ID = @id";
            var parameters = new { id = id };

            using (_connection)
            {
                resultado = await _connection.ExecuteAsync(query, parameters);
            }

            return resultado > 0;
        }
    }
}
