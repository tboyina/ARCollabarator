using System.Data.SqlClient;
using ARCollabator.DALContracts;
using Dapper;

namespace ARCollabator.DAL
{
    public abstract class DALBase<T> where T : class
    {
        protected DapperContext _context;
        public DALBase(DapperContext context)
        {
            _context = context;

        }

    }
}
