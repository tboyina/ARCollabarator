using ARCollabator.Repo;
using ARCollabator.RepoContracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ARCollabator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarkerController : ControllerBase
    {
        private IMarkerRepository markerRepository;
        public MarkerController(IMarkerRepository markerRepository)
        {
            this.markerRepository = markerRepository;

        }
        /// <summary>
        /// Get All the Markers
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetMarkers")]
        public async Task<IEnumerable<Models.RoomMarker>> GetMarkers()
        {
            return await markerRepository.GetMarkers();
        }


        /// <summary>
        /// Get Markers based on Room Name
        /// </summary>
        /// <param name="roomName"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetMarkersByRoomName")]
        public async Task<IEnumerable<Models.RoomMarker>> GetMarkers(string roomName)
        {
            return await markerRepository.GetMarkers(roomName);
        }

        [HttpPost]
        [Route("SetMarkers")]
        public async Task<bool> SetMarkers(Models.RoomMarker roomMarker)
        {
            return await markerRepository.SetMarkers(roomMarker);

        }

    }
}
