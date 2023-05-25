using APIEscola.Model.DTO;
using APIEscola.Model.Models;
using APIEscola.Repositorio.Interfaces;
using APIEscola.Service.Interfaces;
using Escola.API.Validacoes;

namespace Escola.API.Routers
{
    internal static class AlunoRouter
    {
        public static RouteGroupBuilder MapeiaAluno(this RouteGroupBuilder group)
        {
            group.MapGet("/", BuscaAlunos).Produces(StatusCodes.Status400BadRequest)
                 //.RequireAuthorization()
                 .Produces(StatusCodes.Status200OK, typeof(List<AlunoDTO>))
                 .Produces(StatusCodes.Status404NotFound)
                .WithSummary("");

            group.MapGet("/{id}", BuscoAlunoId).Produces(StatusCodes.Status400BadRequest)
                //.RequireAuthorization()
                .Produces(StatusCodes.Status200OK, typeof(List<AlunoDTO>))
                .Produces(StatusCodes.Status404NotFound)
                .WithSummary("");

            group.MapPost("/", SalvaAluno).AddEndpointFilter<ValidaAluno>()
                //.RequireAuthorization()
                .Produces(StatusCodes.Status201Created, typeof(AlunoDTO))
                .Produces(StatusCodes.Status400BadRequest)
                .WithSummary("");

            group.MapPut("/", AtualizaAluno).AddEndpointFilter<ValidaAluno>()
               .Produces(StatusCodes.Status400BadRequest)
               //.RequireAuthorization()
               .Produces(StatusCodes.Status200OK)
               .Produces(StatusCodes.Status404NotFound)
               .WithSummary("");

            group.MapDelete("/{id}", ExcluiAluno).Produces(StatusCodes.Status400BadRequest)
              //.RequireAuthorization()
              .Produces(StatusCodes.Status200OK)
              .Produces(StatusCodes.Status404NotFound)
              .WithSummary("");

            return group;
        }

        internal static async Task<IResult> BuscaAlunos(IAlunoService service)
        {
            try
            {
                List<AlunoDTO> alunos = await service.BuscaAlunos();

                return Results.Json(alunos);
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }

        }

        internal static async Task<IResult> BuscoAlunoId(int id, IAlunoService service)
        {
            try
            {
                AlunoDTO aluno = await service.BuscaAlunoId(id);

                return Results.Json(aluno);
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }

        }

        internal static async Task<IResult> SalvaAluno(AlunoDTO aluno, IAlunoService service)
        {
            try
            {
                bool resultado = await service.SalvaAluno(aluno);

                if (resultado)
                {
                    return Results.Created($"/aluno", aluno);
                }
                else
                {
                    return Results.BadRequest("Não foi possível fazer o cadastro");
                }
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        internal static async Task<IResult> AtualizaAluno(AlunoDTO aluno, IAlunoService service)
        {
            try
            {
                bool resultado = await service.AtualizaAluno(aluno);

                if (resultado)
                {
                    return Results.Ok("Cadastro atualizado com sucesso.");
                }
                else
                {
                    return Results.BadRequest("Não foi possível fazer a atualização do cadastro.");
                }
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        internal static async Task<IResult> ExcluiAluno(int id, IAlunoService service)
        {
            try
            {
                bool resultado = await service.ExcluiAluno(id);

                if (resultado)
                {
                    return Results.Ok("Cadastro excluído.");
                }
                else
                {
                    return Results.BadRequest("Não foi possível excluir cadastro.");
                }
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }
    }
}
