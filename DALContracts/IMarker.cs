namespace ARCollabator.DALContracts
{
    public interface IMarker
    {
        Task<IEnumerable<Models.RoomMarker>> GetMarkers(string roomName = "");

        Task<bool> SetMarkers(Models.RoomMarker roomMarker);
    }
}
