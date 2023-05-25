using Microsoft.Data.SqlClient;
using System.Data;

namespace APIEscola.Repositorio.Factory
{
    public class SqlFactory
    {
        public IDbConnection SqlConnection() 
        {
            return new SqlConnection("Server=localhost:1433;Database=EscolaDb;User Id=sa; Password=yourStrong(!)Password");
        }
    }
}
