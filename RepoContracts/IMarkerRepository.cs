namespace ARCollabator.RepoContracts
{
    public interface IMarkerRepository
    {
        Task<IEnumerable<Models.RoomMarker>> GetMarkers(string roomName = "");

        Task<bool> SetMarkers(Models.RoomMarker roomMarker);
    }
}
