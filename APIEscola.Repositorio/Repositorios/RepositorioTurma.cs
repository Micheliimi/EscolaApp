using APIEscola.Model.Models;
using APIEscola.Repositorio.Factory;
using APIEscola.Repositorio.Interfaces;
using Dapper;
using System.Data;

namespace APIEscola.Repositorio.Repositorios
{
    public class RepositorioTurma : IRepositorioTurma
    {
        private readonly IDbConnection _connection;
        public RepositorioTurma()
        {
            _connection = new SqlFactory().SqlConnection();
        }

        public async Task<IEnumerable<TurmaModel>> BuscaTurmas()
        {
            var query = "SELECT * FROM turma";

            using (_connection)
            {
                return await _connection.QueryAsync<TurmaModel>(query);
            }
        }

        public async Task<TurmaModel> BuscaTurmaId(int id)
        {
            var query = "SELECT * FROM turma WHERE id = @turmaId";
            var parameters = new
            {
                turmaId = id
            };

            using (_connection)
            {
                return await _connection.QueryFirstOrDefaultAsync<TurmaModel>(query, parameters);
            }
        }

        public async Task<bool> SalvaTurma(TurmaModel turma)
        {
            int result;
            var query =
            @"INSERT INTO turma (
            curso_id,
            turma,
            ano
            )
            VALUES
            (
                @cursoId,
                @turma,
                @ano
            )";

            var parameters = new
            {
                cursoId = turma.CursoId,
                turma = turma.Turma,
                ano = turma.Ano,
            };

            using (_connection)
            {
                result = await _connection.ExecuteAsync(query, parameters);
            }

            return result > 0;
        }

        public async Task<bool> AtualizaTurma(TurmaModel turma)
        {
            int result;
            var query =
            @"UPDATE turma
            SET
            curso_id = @cursoId,
            turma = @turma,
            ano = @ano
            WHERE ID = @turmaId";

            var parameters = new
            {
                cursoId = turma.CursoId,
                turma = turma.Turma,
                ano = turma.Ano,
                turmaId = turma.Id
            };

            using (_connection)
            {
                result = await _connection.ExecuteAsync(query, parameters);
            }

            return result > 0;
        }

        public async Task<bool> ExcluiTurma(int id)
        {
            int result;
            var query = "DELETE turma WHERE id = @turmaId";
            var parameters = new { turmaId = id };

            using (_connection)
            {
                result = await _connection.ExecuteAsync(query, parameters);
            }

            return result > 0;
        }
    }
}
