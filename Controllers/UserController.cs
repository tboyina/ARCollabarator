using ARCollabator.RepoContracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ARCollabator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserRepository userRepository;
        public UserController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;

        }

        [HttpGet(Name = "GetUsers/{email}")]
        public async Task<IEnumerable<Models.User>> GetUsers(string email)
        {
           return await userRepository.GetUsers(email);
        }

        [HttpPost(Name = "InsertUserInformation")]
        public async Task<bool> InsertUserInformation(Models.User UserInfo)
        {
            if (UserInfo.UserId > 0)
                return await userRepository.UpdateUser(UserInfo);
            else
                return await userRepository.InsertUsers(UserInfo);
        }

        
    }
}
