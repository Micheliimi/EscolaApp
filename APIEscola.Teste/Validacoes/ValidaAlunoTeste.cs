using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APIEscola.Model.DTO;
using Escola.API.Validacoes;

namespace APIEscola.Teste.Validacoes
{
    [TestClass]
    public class ValidaAlunoTeste
    {
        [TestMethod]
        public void ValidaAluno()
        {
            ValidaAluno validacoes = new ValidaAluno();
            AlunoDTO aluno = new AlunoDTO();

            Assert.AreEqual("Nome é obrigatório", validacoes.Valida(aluno));
            aluno.Nome = "Michele";

            Assert.AreEqual("Usuário é obrigatório", validacoes.Valida(aluno));
            aluno.Usuario = "user";

            Assert.AreEqual("Senha é obrigatória", validacoes.Valida(aluno));
            aluno.Senha = "password";

            Assert.AreEqual("Senha fraca, tente outra senha", validacoes.Valida(aluno));
            aluno.Senha = "Password123@";

            Assert.AreEqual("", validacoes.Valida(aluno));
        }
    }
}
