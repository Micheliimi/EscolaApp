using APIEscola.Model.DTO;
using APIEscola.Model.Models;
using APIEscola.Repositorio.Interfaces;
using APIEscola.Service.Interfaces;

namespace Escola.API.Routers
{
    public static class AlunoTurmaRouter
    {
        public static RouteGroupBuilder MapeiaAlunoTurma(this RouteGroupBuilder group)
        {
            group.MapGet("/", BuscaAlunosTurmas).Produces(StatusCodes.Status400BadRequest)
                 //.RequireAuthorization()
                 .Produces(StatusCodes.Status200OK, typeof(List<AlunoTurmaDTO>))
                 .Produces(StatusCodes.Status404NotFound)
                .WithSummary("");

            group.MapGet("/{id}", BuscaAlunoTurmaId).Produces(StatusCodes.Status400BadRequest)
                 //.RequireAuthorization()
                 .Produces(StatusCodes.Status200OK, typeof(List<AlunoTurmaDTO>))
                 .Produces(StatusCodes.Status404NotFound)
                .WithSummary("");

            group.MapPost("/", SalvaAlunoTurma)
                //.RequireAuthorization()
                .Produces(StatusCodes.Status201Created)
                .Produces(StatusCodes.Status400BadRequest)
                .WithSummary("");

            group.MapPut("/", AtualizaAlunoTurma)
                //.RequireAuthorization()
                .Produces(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status400BadRequest)
                .WithSummary("");

            group.MapDelete("/{id}", ExcluiAlunoTurma).Produces(StatusCodes.Status400BadRequest)
              //.RequireAuthorization()
              .Produces(StatusCodes.Status200OK)
              .Produces(StatusCodes.Status404NotFound)
              .WithSummary("");

            return group;
        }

        internal static async Task<IResult> BuscaAlunosTurmas(IAlunoTurmaService service)
        {
            try
            {
                List<AlunoTurmaDTO> alunosTurmas = await service.BuscaAlunosTurmas();

                return Results.Json(alunosTurmas);
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }

        }

        internal static async Task<IResult> BuscaAlunoTurmaId(int alunoTurmaId, IAlunoTurmaService service)
        {
            try
            {
                AlunoTurmaDTO alunoTurmaResponsta = await service.BuscaAlunoTurmaId(alunoTurmaId);

                return Results.Json(alunoTurmaResponsta);
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }

        }

        internal static async Task<IResult> SalvaAlunoTurma(AlunoTurmaDTO alunoTurma, IAlunoTurmaService service)
        {
            try
            {
                bool resultado = await service.SalvaAlunoTurma(alunoTurma);

                if (resultado)
                {
                    return Results.Created($"/alunoturma", alunoTurma);
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

        internal static async Task<IResult> AtualizaAlunoTurma(AlunoTurmaDTO alunoTurma, IAlunoTurmaService service)
        {
            try
            {
                bool resultado = await service.AtualizaAlunoTurma(alunoTurma);

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

        internal static async Task<IResult> ExcluiAlunoTurma(int alunoTurmaId, IAlunoTurmaService service)
        {
            try
            {
                bool resultado = await service.ExcluiAlunoTurma(alunoTurmaId);

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
