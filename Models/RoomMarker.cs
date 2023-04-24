namespace ARCollabator.Models
{
    public class RoomMarker
    {
        public int RoomMarkerID { get; set; }
        public string RoomName { get; set; }
        public string MarkerID { get; set; }

        public string X { get; set; }
        public string Y { get; set; }
        public string Z { get; set; }

        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }

    }
}
