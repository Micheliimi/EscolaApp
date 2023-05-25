using APIEscola.Model.DTO;
using APIEscola.Model.Models;
using APIEscola.Repositorio.Interfaces;
using APIEscola.Service.Interfaces;
using APIEscola.Service.Services;
using Moq;

namespace APIEscola.Service.Teste
{
    [TestClass]
    public class AlunoServiceTeste
    {
        private IAlunoService? _service;
        private Mock<IRepositorioAluno>? _repositorio;

        [TestInitialize]
        public void SetUp()
        {
            _repositorio = new Mock<IRepositorioAluno>();
            _service = new AlunoService(_repositorio.Object);
        }

        [TestMethod]
        public async Task TestaSalvaAlunoSucesso()
        {
            AlunoDTO alunoDto = new AlunoDTO()
            {
                Nome = "Michele",
                Usuario = "user",
                Senha = "Mih123&"
            };

            _repositorio!.Setup(r => r.BuscaAlunoId(It.IsAny<int>()));
            _repositorio!.Setup(r => r.SalvaAluno(It.IsAny<AlunoModel>())).Returns(Task.FromResult(true));

            bool resposta = await _service!.SalvaAluno(alunoDto);

            Assert.AreEqual(true, resposta);
        }

        [TestMethod]
        public async Task TestaSalvaAlunoRepetido()
        {
            AlunoDTO alunoDto = new AlunoDTO()
            {
                Nome = "Michele",
                Usuario = "user",
                Senha = "Mih123&"
            };
            AlunoModel alunoModel = new AlunoModel()
            {
                Nome = "Michele",
                Usuario = "user",
                Senha = "Mih123&"
            };
            _repositorio!.Setup(r => r.BuscaAlunoId(It.IsAny<int>())).Returns(Task.FromResult(alunoModel));

            bool resposta = await _service!.SalvaAluno(alunoDto);

            Assert.AreEqual(false, resposta);
        }

        [TestMethod]
        public async Task TestaAtualizaAlunoSucesso()
        {
            AlunoDTO alunoDto = new AlunoDTO()
            {
                Nome = "Michele",
                Usuario = "user",
                Senha = "Mih123&"
            };

            AlunoModel alunoModel = new AlunoModel()
            {
                Nome = "Michele",
                Usuario = "user",
                Senha = "Mih123&"
            };

            _repositorio!.Setup(r => r.BuscaAlunoId(It.IsAny<int>())).Returns(Task.FromResult(alunoModel));
            _repositorio!.Setup(r => r.AtualizaAluno(It.IsAny<AlunoModel>())).Returns(Task.FromResult(true));

            bool resposta = await _service!.AtualizaAluno(alunoDto);

            Assert.AreEqual(true, resposta);
        }

        [TestMethod]
        public async Task TestaAtualizaAlunoNaoCadastrado()
        {
            AlunoDTO alunoDto = new AlunoDTO()
            {
                Nome = "Michele",
                Usuario = "user",
                Senha = "Mih123&"
            };

            _repositorio!.Setup(r => r.BuscaAlunoId(It.IsAny<int>()));
            _repositorio!.Setup(r => r.AtualizaAluno(It.IsAny<AlunoModel>())).Returns(Task.FromResult(false));

            bool resposta = await _service!.AtualizaAluno(alunoDto);

            Assert.AreEqual(false, resposta);
        }
    }
}