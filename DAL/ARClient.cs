using ARCollabator.DALContracts;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace ARCollabator.DAL
{
    public class ARClient : IARClient
    {
        public SqlConnection Connection { get; }

        public ARClient(IConfiguration configuration)
        {
            Connection = new SqlConnection(configuration["ConnectionStrings"]);

        }
    }
}
