using Microsoft.AspNetCore.Mvc.Rendering;

namespace Escola.Web.Models
{
    public class AlunoTurmaDropdownList
    {
        public int Id { get; set; }
        public SelectList Alunos { get; set; }
        public SelectList Turmas { get; set; }
    }
}
