using ARCollabator.DALContracts;
using Dapper;
using System.Data;

namespace ARCollabator.DAL
{
    public class Marker : DALBase<Models.RoomMarker>, IMarker
    {

        private readonly DapperContext _context;
        public Marker(DapperContext context) : base(context)
        {
            _context = context;
        }



        public async Task<IEnumerable<Models.RoomMarker>> GetMarkers(string roomName = "")
        {

            var query = "GET_Markers";
            var parameters = new DynamicParameters();
            using (var connection = _context.CreateConnection())
            {
                parameters.Add("@RoomName", roomName);
                var markers = await connection.QueryAsync<Models.RoomMarker>(query, parameters, null, null, commandType: CommandType.StoredProcedure);
                return markers;
            }
        }

        public async Task<bool> SetMarkers(Models.RoomMarker roomMarker)
        {

            var query = "SetMarkers";
            var parameters = new DynamicParameters();
            using (var connection = _context.CreateConnection())
            {

                parameters.Add("@RoomMarkerID", roomMarker.RoomMarkerID);
                parameters.Add("@RoomName", roomMarker.RoomName);
                parameters.Add("@MarkerID", roomMarker.MarkerID);
                parameters.Add("@X", roomMarker.X);
                parameters.Add("@Y", roomMarker.Y);
                parameters.Add("@Z", roomMarker.Z);
               

                var markers = await connection.ExecuteAsync(query, parameters, null, null, commandType: CommandType.StoredProcedure);
                if (markers > 0)
                    return true;
                else
                    return false;

            }
        }


    }
}
