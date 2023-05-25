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
    public class TurmaServiceTeste
    {    
        private ITurmaService? _service;
        private Mock<IRepositorioTurma>? _repositorio;

        [TestInitialize]
        public void SetUp()
        {
            _repositorio = new Mock<IRepositorioTurma>();
            _service = new TurmaService(_repositorio.Object);
        }

        [TestMethod]
        public async Task TestaSalvaTurmaSucesso()
        {
            TurmaDTO turmaDto = new TurmaDTO()
            {
                CursoId = 1,
                Turma = "A",
                Ano = 2023,
            };

            _repositorio!.Setup(r => r.BuscaTurmaId(It.IsAny<int>()));
            _repositorio!.Setup(r => r.SalvaTurma(It.IsAny<TurmaModel>())).Returns(Task.FromResult(true));

            bool resposta = await _service!.SalvaTurma(turmaDto);

            Assert.AreEqual(true, resposta);
        }

        [TestMethod]
        public async Task TestaSalvaTurmaRepetida()
        {
            TurmaDTO turmaDto = new TurmaDTO()
            {
                CursoId = 1,
                Turma = "A",
                Ano = 2023,
            };
            TurmaModel turmaModel = new TurmaModel()
            {
                CursoId = 1,
                Turma = "A",
                Ano = 2023,
            };
            _repositorio!.Setup(r => r.BuscaTurmaId(It.IsAny<int>())).Returns(Task.FromResult(turmaModel));

            bool resposta = await _service!.SalvaTurma(turmaDto);

            Assert.AreEqual(false, resposta);
        }

        [TestMethod]
        public async Task TestaAtualizaTurmaSucesso()
        {
            TurmaDTO turmaDto = new TurmaDTO()
            {
                CursoId = 1,
                Turma = "A",
                Ano = 2023,
            };

            TurmaModel turmaModel = new TurmaModel()
            {
                CursoId = 1,
                Turma = "A",
                Ano = 2023,
            };

            _repositorio!.Setup(r => r.BuscaTurmaId(It.IsAny<int>())).Returns(Task.FromResult(turmaModel));
            _repositorio!.Setup(r => r.AtualizaTurma(It.IsAny<TurmaModel>())).Returns(Task.FromResult(true));

            bool resposta = await _service!.AtualizaTurma(turmaDto);

            Assert.AreEqual(true, resposta);
        }

        [TestMethod]
        public async Task TestaAtualizaTurmaNaoCadastrado()
        {
            TurmaDTO turmaDto = new TurmaDTO()
            {
                CursoId = 1,
                Turma = "A",
                Ano = 2023,
            };

            _repositorio!.Setup(r => r.BuscaTurmaId(It.IsAny<int>()));
            _repositorio!.Setup(r => r.AtualizaTurma(It.IsAny<TurmaModel>())).Returns(Task.FromResult(false));

            bool resposta = await _service!.AtualizaTurma(turmaDto);

            Assert.AreEqual(false, resposta);
        }
     }
}
