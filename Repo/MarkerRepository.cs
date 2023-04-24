using ARCollabator.DALContracts;
using ARCollabator.Models;
using ARCollabator.RepoContracts;

namespace ARCollabator.Repo
{
    public class MarkerRepository : IMarkerRepository
    {
        private IMarker marker;
        public MarkerRepository(IMarker marker)
        {
            this.marker = marker;
        }

        public Task<IEnumerable<RoomMarker>> GetMarkers(string roomName = "")
        {
            return marker.GetMarkers(roomName);
        }

        public Task<bool> SetMarkers(RoomMarker roomMarker)
        {
            return marker.SetMarkers(roomMarker);
        }
    }
}
