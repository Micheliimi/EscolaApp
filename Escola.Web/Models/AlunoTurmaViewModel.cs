namespace Escola.Web.Models
{
    public class AlunoTurmaViewModel
    {
        public int AlunoTurmaId { get; set; }
        public int? AlunoId { get; set; }
        public string? AlunoNome { get; set; }
        public int? TurmaId { get; set; }
        public string? TurmaNome { get; set; }
    }
}
