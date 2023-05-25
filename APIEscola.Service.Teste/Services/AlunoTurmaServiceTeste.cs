using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APIEscola.Model.DTO;
using APIEscola.Model.Models;
using APIEscola.Repositorio.Interfaces;
using APIEscola.Service.Interfaces;
using APIEscola.Service.Services;
using Moq;

namespace APIEscola.Service.Teste.Services
{
    [TestClass]
    public class AlunoTurmaServiceTeste
    {
        private IAlunoTurmaService? _service;
        private Mock<IRepositorioAlunoTurma>? _repositorio;

        [TestInitialize]
        public void SetUp()
        {
            _repositorio = new Mock<IRepositorioAlunoTurma>();
            _service = new AlunoTurmaService(_repositorio.Object);
        }

        [TestMethod]
        public async Task TestaSalvaAlunoTurmaSucesso()
        {
            AlunoTurmaDTO alunoTurmaDto = new AlunoTurmaDTO()
            {
                AlunoId = 2,
                AlunoNome = "Michele",
                TurmaId = 3,
                TurmaNome = "A"
            };

            _repositorio!.Setup(r => r.BuscaAlunosTurmasId(It.IsAny<int>()));
            _repositorio!.Setup(r => r.SalvaAlunoTurma(It.IsAny<AlunoTurmaModel>())).Returns(Task.FromResult(true));

            bool resposta = await _service!.SalvaAlunoTurma(alunoTurmaDto);

            Assert.AreEqual(true, resposta);
        }

        [TestMethod]
        public async Task TestaSalvaTurmaRepetida()
        {
            AlunoTurmaDTO alunoTurmaDto = new AlunoTurmaDTO()
            {
               AlunoId = 2,
               AlunoNome = "Michele",
               TurmaId = 3,
               TurmaNome = "A"
            };
            AlunoTurmaModel turmaModel = new AlunoTurmaModel()
            {
                AlunoId = 2,
                AlunoNome = "Michele",
                TurmaId = 3,
                TurmaNome = "A"
            };
            _repositorio!.Setup(r => r.BuscaAlunosTurmasId(It.IsAny<int>())).Returns(Task.FromResult(turmaModel));

            bool resposta = await _service!.SalvaAlunoTurma(alunoTurmaDto);

            Assert.AreEqual(false, resposta);
        }

        [TestMethod]
        public async Task TestaAtualizaTurmaSucesso()
        {
            AlunoTurmaDTO alunoTurmaDto = new AlunoTurmaDTO()
            {
                AlunoId = 2,
                AlunoNome = "Michele",
                TurmaId = 3,
                TurmaNome = "A"
            };

            AlunoTurmaModel turmaModel = new AlunoTurmaModel()
            {
                AlunoId = 2,
                AlunoNome = "Michele",
                TurmaId = 3,
                TurmaNome = "A"
            };

            _repositorio!.Setup(r => r.BuscaAlunosTurmasId(It.IsAny<int>())).Returns(Task.FromResult(turmaModel));
            _repositorio!.Setup(r => r.AtualizaAlunoTurma(It.IsAny<AlunoTurmaModel>())).Returns(Task.FromResult(true));

            bool resposta = await _service!.AtualizaAlunoTurma(alunoTurmaDto);

            Assert.AreEqual(true, resposta);
        }

        [TestMethod]
        public async Task TestaAtualizaTurmaNaoCadastrado()
        {
            AlunoTurmaDTO alunoTurmaDto = new AlunoTurmaDTO()
            {
                AlunoId = 2,
                AlunoNome = "Michele",
                TurmaId = 3,
                TurmaNome = "A"
            };

            _repositorio!.Setup(r => r.BuscaAlunosTurmasId(It.IsAny<int>()));
            _repositorio!.Setup(r => r.AtualizaAlunoTurma(It.IsAny<AlunoTurmaModel>())).Returns(Task.FromResult(false));

            bool resposta = await _service!.AtualizaAlunoTurma(alunoTurmaDto);

            Assert.AreEqual(false, resposta);
        }
    }
}
