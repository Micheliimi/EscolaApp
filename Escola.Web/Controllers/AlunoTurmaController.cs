using Escola.Web.Models;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Escola.Web.Controllers
{
    public class AlunoTurmaController : Controller
    {
        #region Propriedades
        private readonly string _endpoint = "http://localhost:5206/v1/alunoturma/";
        private readonly HttpClient _httpClient = null;
        #endregion
        #region Construtores
        public AlunoTurmaController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(_endpoint);
        }
        #endregion
        #region Métodos Privados
        private async Task<AlunoTurmaViewModel> PesquisarAlunoTurma(int id)
        {
            try
            {
                AlunoTurmaViewModel alunoTurma = null;
                string url = $"{_endpoint}{id}";

                HttpResponseMessage response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    alunoTurma = JsonConvert.DeserializeObject<AlunoTurmaViewModel>(content);
                }

                return alunoTurma;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async Task<List<AlunoViewModel>> PesquisarAlunos()
        {
            try
            {
                List<AlunoViewModel> alunos = null;

                string url = $"http://localhost:52784/v1/aluno/";

                HttpResponseMessage response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    alunos = JsonConvert.DeserializeObject<List<AlunoViewModel>>(content);
                }

                return alunos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async Task<List<TurmaViewModel>> PesquisarTurmas()
        {
            try
            {
                List<TurmaViewModel> turmas = null;

                string url = $"http://localhost:52784/v1/turma/";

                HttpResponseMessage response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    turmas = JsonConvert.DeserializeObject<List<TurmaViewModel>>(content);
                }

                return turmas;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private ByteArrayContent Serialize(AlunoTurmaViewModel alunoTurma)
        {
            string json = JsonConvert.SerializeObject(alunoTurma);
            byte[] buffer = Encoding.UTF8.GetBytes(json);
            ByteArrayContent byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            return byteContent;
        }

        #endregion
        #region Actions
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            try
            {
                List<AlunoTurmaViewModel>? alunoTurmas = null;
                HttpResponseMessage response = await _httpClient.GetAsync(_endpoint);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    alunoTurmas = JsonConvert.DeserializeObject<List<AlunoTurmaViewModel>>(content);
                }
                else
                {
                    ModelState.AddModelError(null, "Erro ao processar a solicitação");
                }

                return View(alunoTurmas);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IActionResult> Create()
        {
            List<AlunoViewModel> alunos = await PesquisarAlunos();
            List<TurmaViewModel> turmas = await PesquisarTurmas();

            AlunoTurmaDropdownList drop = new AlunoTurmaDropdownList();
            drop.Alunos = new SelectList(alunos, "AlunoId", "AlunoNome");
            drop.Turmas = new SelectList(turmas, "TurmaId", "TurmaNome");

            return View(drop);
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("AlunoId, TurmaId")] AlunoTurmaViewModel alunoTurma)
        {
            try
            {
                ByteArrayContent byteContent = Serialize(alunoTurma);

                HttpResponseMessage response = await _httpClient.PostAsync(_endpoint, byteContent);

                if (!response.IsSuccessStatusCode)
                    ModelState.AddModelError(null, "Erro ao processar a solicitação");

                return RedirectToAction("List");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int alunoAlunoId)
        {
            List<AlunoViewModel> alunos = await PesquisarAlunos();
            List<TurmaViewModel> turmas = await PesquisarTurmas();
            AlunoTurmaViewModel alunoTurmaView = await PesquisarAlunoTurma(alunoAlunoId);

            AlunoTurmaDropdownList drop = new AlunoTurmaDropdownList();

            drop.Id = alunoAlunoId;
            drop.Alunos = new SelectList(alunos, "AlunoId", "AlunoNome", alunoTurmaView.AlunoId);
            drop.Turmas = new SelectList(turmas, "TurmaId", "TurmaNome", alunoTurmaView.TurmaId);

            return View(drop);
        }

        [HttpPut]
        public async Task<IActionResult> Edit([Bind("Id, AlunoId, TurmaId")] AlunoTurmaViewModel alunoTurma)
        {
            try
            {
                ByteArrayContent byteContent = Serialize(alunoTurma);

                HttpResponseMessage response = await _httpClient.PutAsync(_endpoint, byteContent);

                if (!response.IsSuccessStatusCode)
                    ModelState.AddModelError(null, "Erro ao processar a solicitação");

                return RedirectToAction("List");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int alunoTurmaId)
        {
            AlunoTurmaViewModel alunoTurma = await PesquisarAlunoTurma(alunoTurmaId);

            if (alunoTurma == null)
                return NotFound();

            return View(alunoTurma);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string alunoTurmaId)
        {
            try
            {
                int id = Int32.Parse(alunoTurmaId);

                string url = $"{_endpoint}{id}";

                HttpResponseMessage response = await _httpClient.DeleteAsync(url);

                if (!response.IsSuccessStatusCode)
                    ModelState.AddModelError(null, "Erro ao processar a solicitação");

                return RedirectToAction("List");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion    
    }
}
