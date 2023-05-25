using APIEscola.Model.DTO;
using APIEscola.Model.Models;
using APIEscola.Repositorio.Interfaces;
using APIEscola.Service.Interfaces;
using Escola.API.Validacoes;

namespace Escola.API.Routers
{
    internal static class TurmaRouter
    {
        public static RouteGroupBuilder MapeiaTurma(this RouteGroupBuilder group)
        {
            group.MapGet("/", BuscaTurmas).Produces(StatusCodes.Status400BadRequest)
                 //.RequireAuthorization()
                 .Produces(StatusCodes.Status200OK, typeof(List<TurmaDTO>))
                 .Produces(StatusCodes.Status404NotFound)
                .WithSummary("");

            group.MapGet("/{id}", BuscaTurmaId).Produces(StatusCodes.Status400BadRequest)
                .RequireAuthorization()
                .Produces(StatusCodes.Status200OK, typeof(List<TurmaDTO>))
                .Produces(StatusCodes.Status404NotFound)
                .WithSummary("");

            group.MapPost("/", SalvaTurma).AddEndpointFilter<ValidaTurma>()
                //.RequireAuthorization()
                .Produces(StatusCodes.Status201Created, typeof(TurmaDTO))
                .Produces(StatusCodes.Status400BadRequest)
                .WithSummary("");

            group.MapPut("/", AtualizaTurma).AddEndpointFilter<ValidaTurma>()
               .Produces(StatusCodes.Status400BadRequest)
               .RequireAuthorization()
               .Produces(StatusCodes.Status200OK)
               .Produces(StatusCodes.Status404NotFound)
               .WithSummary("");

            group.MapDelete("/{id}", ExcluiTurma).Produces(StatusCodes.Status400BadRequest)
              .RequireAuthorization()
              .Produces(StatusCodes.Status200OK)
              .Produces(StatusCodes.Status404NotFound)
              .WithSummary("");

            return group;
        }

        internal static async Task<IResult> BuscaTurmas(ITurmaService service)
        {
            try
            {
                List<TurmaDTO> turmas = await service.BuscaTurmas();

                return Results.Json(turmas);
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }

        }

        internal static async Task<IResult> BuscaTurmaId(int id, ITurmaService service)
        {
            try
            {
                TurmaDTO turma = await service.BuscaTurmaId(id);

                return Results.Json(turma);
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        internal static async Task<IResult> SalvaTurma(TurmaDTO turma, ITurmaService service)
        {
            try
            {
                bool resultado = await service.SalvaTurma(turma);

                if (resultado)
                {
                    return Results.Created($"/turma", turma);
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

        internal static async Task<IResult> AtualizaTurma(TurmaDTO turma, ITurmaService service)
        {
            try
            {
                bool resultado = await service.AtualizaTurma(turma);

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

        internal static async Task<IResult> ExcluiTurma(int id, ITurmaService service)
        {
            try
            {
                bool resultado = await service.ExcluiTurma(id);

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
