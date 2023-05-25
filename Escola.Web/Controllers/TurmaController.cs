using Escola.Web.Models;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Escola.Web.Controllers
{
    public class TurmaController : Controller
    {
        #region Propriedades
        private readonly string _endpoint = "http://localhost:52784/v1/turma/";
        private readonly HttpClient _httpClient = null;
        #endregion
        #region Construtores
        public TurmaController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(_endpoint);
        }
        #endregion
        #region Métodos Privados
        private async Task<TurmaViewModel> Pesquisar(int id)
        {
            try
            {
                TurmaViewModel turma = null;

                string url = $"{_endpoint}{id}";

                HttpResponseMessage response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    turma = JsonConvert.DeserializeObject<TurmaViewModel>(content);
                }

                return turma;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private ByteArrayContent Serialize(TurmaViewModel turma)
        {
            string json = JsonConvert.SerializeObject(turma);
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
                List<TurmaViewModel>? turmas = null;
                HttpResponseMessage response = await _httpClient.GetAsync(_endpoint);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    turmas = JsonConvert.DeserializeObject<List<TurmaViewModel>>(content);
                }
                else
                {
                    ModelState.AddModelError(null, "Erro ao processar a solicitação");
                }

                return View(turmas);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create([Bind("CursoId, Turma, Ano")] TurmaViewModel turma)
        {
            try
            {
                ByteArrayContent byteContent = Serialize(turma);

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
        public async Task<IActionResult> Edit(int id)
        {
            TurmaViewModel turma = await Pesquisar(id);

            return View(turma);
        }

        [HttpPut]
        public async Task<IActionResult> Edit([Bind("Id, CursoId, Turma, Ano")] TurmaViewModel turma)
        {
            try
            {
                ByteArrayContent byteContent = Serialize(turma);

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
        public async Task<IActionResult> Delete(int id)
        {
            TurmaViewModel turma = await Pesquisar(id);

            if (turma == null)
                return NotFound();

            return View(turma);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string idTurma)
        {
            try
            {
                int id = Int32.Parse(idTurma);
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
