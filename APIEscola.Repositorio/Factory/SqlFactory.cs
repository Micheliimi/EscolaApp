using Microsoft.Data.SqlClient;
using System.Data;

namespace APIEscola.Repositorio.Factory
{
    public class SqlFactory
    {
        public IDbConnection SqlConnection() 
        {
            return new SqlConnection("Data Source=DESKTOP-F91D3M7;Initial Catalog=db_escola;Integrated Security=False");
        }
    }
}
