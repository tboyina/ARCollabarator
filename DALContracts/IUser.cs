namespace ARCollabator.DALContracts
{
    public interface IUser
    {
        Task<IEnumerable<Models.User>> GetUsers(string email);

        Task<bool> InsertUsers(Models.User userinfo);
        Task<bool> UpdateUser(Models.User userinfo);
        Task<IEnumerable<Models.User>> GetNearbyUsers(string email, string interests);
    }
}
