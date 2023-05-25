using Escola.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace Escola.Web.Controllers
{
    public class AlunoController : Controller
    {
        #region Propriedades
        private readonly string _endpoint = "http://localhost:52784/v1/aluno/";
        private readonly HttpClient _httpClient = null;

        #endregion
        #region Construtores
        public AlunoController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(_endpoint);
        }

        #endregion
        #region Métodos Privados
        private async Task<AlunoViewModel> Pesquisar(int id)
        {
            try
            {
                AlunoViewModel aluno = null;
                string url = $"{_endpoint}{id}";

                HttpResponseMessage response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    aluno = JsonConvert.DeserializeObject<AlunoViewModel>(content);
                }

                return aluno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private ByteArrayContent Serialize(AlunoViewModel aluno)
        {
            string json = JsonConvert.SerializeObject(aluno);
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
        public async Task<IActionResult> Listar()
        {
            try
            {
                List<AlunoViewModel>? alunos = null;
                HttpResponseMessage response = await _httpClient.GetAsync(_endpoint);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    alunos = JsonConvert.DeserializeObject<List<AlunoViewModel>>(content);
                }
                else
                {
                    ModelState.AddModelError(null, "Erro ao processar a solicitação");
                }

                return View(alunos);
            }
            catch (Exception ex)
            {
                throw ex;
            }     
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Nome, Usuario, Senha")] AlunoViewModel aluno)
        {
            try
            {
                ByteArrayContent byteContent = Serialize(aluno);

                HttpResponseMessage response = await _httpClient.PostAsync(_endpoint, byteContent);

                if (!response.IsSuccessStatusCode)
                    ModelState.AddModelError(null, "Erro ao processar a solicitação");

                return RedirectToAction("Listar");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            AlunoViewModel aluno = await Pesquisar(id);

            return View(aluno);
        }

        [HttpPut]
        public async Task<IActionResult> Edit([Bind("Id, Nome, Usuario, Senha")] AlunoViewModel aluno)
        {
            try
            {
                ByteArrayContent byteContent = Serialize(aluno);

                HttpResponseMessage response = await _httpClient.PutAsync(_endpoint, byteContent);

                if (!response.IsSuccessStatusCode)
                    ModelState.AddModelError(null, "Erro ao processar a solicitação");

                return RedirectToAction("Listar");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            AlunoViewModel aluno = await Pesquisar(id);

            if (aluno == null)
                return NotFound();

            return View(aluno);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string idAluno)
        {
            try
            {
                int id = Int32.Parse(idAluno);
                string url = $"{_endpoint}{id}";

                HttpResponseMessage response = await _httpClient.DeleteAsync(url);

                if (!response.IsSuccessStatusCode)
                    ModelState.AddModelError(null, "Erro ao processar a solicitação");
                
                return RedirectToAction("Listar");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
