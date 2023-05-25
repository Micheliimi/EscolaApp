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
    public class ValidaTurmaTeste
    {
        [TestMethod]
        public void ValidaTurma()
        {
            ValidaTurma validacoes = new ValidaTurma();
            TurmaDTO turma = new TurmaDTO();

            Assert.AreEqual("Nome da turma é obrigatório", validacoes.Valida(turma));
            turma.Turma = "A";

            Assert.AreEqual("O nome da turma precisa ter no mínimo dois caracteres", validacoes.Valida(turma));
            turma.Turma = "1A";
            turma.Ano = 2010;

            Assert.AreEqual("Não é possível cadastrar turmas com datas anteriores da atual", validacoes.Valida(turma));
            turma.Ano = 2024;

            Assert.AreEqual("", validacoes.Valida(turma));
        }
    }
}
