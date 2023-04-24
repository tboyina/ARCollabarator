using System.Data.SqlClient;

namespace ARCollabator.DALContracts
{
    public interface IARClient
    {
        SqlConnection Connection { get; }
    }
}
