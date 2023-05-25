using APIEscola.Model.DTO;

namespace Escola.API.Validacoes
{
    public class ValidaTurma : IEndpointFilter
    {
        public async ValueTask<object> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            TurmaDTO turma = context.GetArgument<TurmaDTO>(0);
            string validacao = Valida(turma);
            if (!string.IsNullOrEmpty(validacao))
            {
                return Results.BadRequest(validacao);
            }

            return await next(context);
        }

        internal string Valida(TurmaDTO turma)
        {
            if (string.IsNullOrEmpty(turma.Turma))
            {
                return "Nome da turma é obrigatório.";
            }
            if (turma.Turma.Length < 3)
            {
                return "O nome da turma precisa ter no mínimo três caracteres.";
            }
            if (turma.Ano < DateTime.Now.Year)
            {
                return "Não é possível cadastrar turmas com datas anteriores da atual.";
            }
            return string.Empty;
        }
    }
}
