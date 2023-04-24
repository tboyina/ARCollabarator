using ARCollabator.DALContracts;
using ARCollabator.RepoContracts;

namespace ARCollabator.Repo
{
    public class UserRepository : IUserRepository
    {
        private IUser user;
        public UserRepository(IUser user)
        {
            this.user = user;
        }
        public async Task<IEnumerable<Models.User>> GetUsers(string email)
        {
            return await user.GetUsers(email);
        }

        public async Task<bool> InsertUsers(Models.User userinfo)
        {
            return await user.InsertUsers(userinfo);
        }

        public async Task<bool> UpdateUser(Models.User userinfo)
        {
            return await user.UpdateUser(userinfo);
        }

        public async Task<IEnumerable<Models.User>> GetNearbyUsers(string email, string interests)
        {
            return await user.GetNearbyUsers(email,interests);
        }
    }
}
