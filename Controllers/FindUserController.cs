using ARCollabator.RepoContracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ARCollabator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FindUserController : ControllerBase
    {
        private IUserRepository userRepository;
        public FindUserController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <param name="interests"></param>
        /// <returns></returns>
        [HttpPost(Name = "GetNearestUsers/{email}/{interests?}")]
        public async Task<IEnumerable<Models.User>> GetNearestUsers(string email,string interests="")
        {
            return await userRepository.GetNearbyUsers(email, interests);
        }
    }
}
