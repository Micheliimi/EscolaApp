using System.Text.RegularExpressions;
using APIEscola.Model.DTO;

namespace Escola.API.Validacoes
{
    public class ValidaAluno : IEndpointFilter
    {
        public ValidaAluno() { }
        public async ValueTask<object> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            AlunoDTO aluno = context.GetArgument<AlunoDTO>(0);
            string validacao = Valida(aluno);
            if (!string.IsNullOrEmpty(validacao))
            {
                return Results.BadRequest(validacao);
            }

            return await next(context);
        }

        public string Valida(AlunoDTO aluno)
        {
            if (string.IsNullOrEmpty(aluno.Nome))
            {
                return "Nome é obrigatório";
            }
            if (string.IsNullOrEmpty(aluno.Usuario))
            {
                return "Usuário é obrigatório";
            }
            if (string.IsNullOrEmpty(aluno.Senha))
            {
                return "Senha é obrigatória";
            }
            if (!Regex.IsMatch(aluno.Senha, "^(?=.*[A-Z])(?=.*[!#@$%&])(?=.*[0-9])(?=.*[a-z]).{6,15}$"))
            {
                return "Senha fraca, tente outra senha";
            }

            return string.Empty;
        }
    }
}
