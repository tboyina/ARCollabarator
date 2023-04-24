using ARCollabator.DALContracts;
using System.Data.SqlClient;
using Dapper;
using System.Data;
using System.Text.RegularExpressions;

namespace ARCollabator.DAL
{
    public class User : DALBase<Models.User>, IUser
    {

        private readonly DapperContext _context;
        public User(DapperContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Models.User>> GetUsers(string email)
        {

            var query = "GET_USER_INFO";
            var parameters = new DynamicParameters();
            using (var connection = _context.CreateConnection())
            {
                parameters.Add("@email", email);
                var users = await connection.QueryAsync<Models.User>(query, parameters, null, null, commandType: CommandType.StoredProcedure);
                return users;
            }
        }

        public async Task<bool> InsertUsers(Models.User userinfo)
        {

            var query = "USER_INFO_Insert";
            var parameters = new DynamicParameters();
            using (var connection = _context.CreateConnection())
            {

                parameters.Add("@FirstName", userinfo.FirstName);
                parameters.Add("@MiddleName", userinfo.MiddleName);
                parameters.Add("@LastName", userinfo.LastName);
                parameters.Add("@PersonalEmail", userinfo.PersonalEmail);
                parameters.Add("@MobileNumber", userinfo.MobileNumber);
                parameters.Add("@City", userinfo.City);
                parameters.Add("@SocialInterests", userinfo.SocialInterests);
                parameters.Add("@TechnicalInterests", userinfo.TechnicalInterests);
                parameters.Add("@Address", userinfo.Address);
                parameters.Add("@Latitude", userinfo.Latitude);
                parameters.Add("@Longitude", userinfo.Longitude);

                var users = await connection.ExecuteAsync(query, parameters, null, null, commandType: CommandType.StoredProcedure);
                if (users > 0)
                    return true;
                else
                    return false;

            }
        }

        public async Task<bool> UpdateUser(Models.User userinfo)
        {

            var query = "USER_INFO_Update";
            var parameters = new DynamicParameters();
            using (var connection = _context.CreateConnection())
            {
                parameters.Add("@UserId", userinfo.UserId);
                parameters.Add("@FirstName", userinfo.FirstName);
                parameters.Add("@MiddleName", userinfo.MiddleName);
                parameters.Add("@LastName", userinfo.LastName);
                parameters.Add("@PersonalEmail", userinfo.PersonalEmail);
                parameters.Add("@MobileNumber", userinfo.MobileNumber);
                parameters.Add("@City", userinfo.City);
                parameters.Add("@SocialInterests", userinfo.SocialInterests);
                parameters.Add("@TechnicalInterests", userinfo.TechnicalInterests);
                parameters.Add("@Address", userinfo.Address);
                parameters.Add("@Latitude", userinfo.Latitude);
                parameters.Add("@Longitude", userinfo.Longitude);

                var users = await connection.ExecuteAsync(query, parameters, null, null, commandType: CommandType.StoredProcedure);
                if (users > 0)
                    return true;
                else
                    return false;

            }
        }

        public async Task<IEnumerable<Models.User>> GetNearbyUsers(string email, string interests)
        {
            if (interests.Contains("\"\""))
                interests = string.Empty;
            if (email.Contains("\"\""))
                email = string.Empty;
            var query = "GET_OtherUSER_INFO";
            var parameters = new DynamicParameters();
            List<Models.User> users = new List<Models.User>();
            using (var connection = _context.CreateConnection())
            {
                parameters.Add("@email", email);
                var reader = connection.QueryMultipleAsync(query, parameters, null, null, commandType: CommandType.StoredProcedure);
                users = reader.Result.Read<Models.User>().ToList();
            }

            var currentUser = await GetUsers(email);
            Logic l = new Logic();
            foreach (var user in users)
            {
                if (currentUser.Any())
                {
                    var sinterestscore = l.stringSimilarity(currentUser.FirstOrDefault().SocialInterests, user.SocialInterests);
                    var tinterests = l.stringSimilarity(currentUser.FirstOrDefault().TechnicalInterests, user.TechnicalInterests);
                    var lat1 = Convert.ToDouble(currentUser.FirstOrDefault().Latitude);
                    var long1 = Convert.ToDouble(currentUser.FirstOrDefault().Longitude);
                    var lat2 = Convert.ToDouble(user.Latitude);
                    var long2 = Convert.ToDouble(user.Longitude);
                    var distance = l.distance(lat1, long1, lat2, long2, 'M');

                    user.DistancefromLoggedUser = Math.Round(distance, 2);
                    user.SimilarityScore = Math.Round((tinterests + sinterestscore) / 2, 2);

                }
            }
            //interests = Regex.Replace(interests, @"\t|\n|\r", string.Empty);

            string ints = interests.Replace(Environment.NewLine, "");
            if (!string.IsNullOrEmpty(interests))
            {
                string[] k = interests.ToLower().Split(new char[] { '-', '/', '(', ')', ',' }, StringSplitOptions.RemoveEmptyEntries);
                List<Models.User> luser = new List<Models.User>();
                foreach (var item in users)
                {
                    var i = item.TechnicalInterests.ToLower().Split(new char[] { '-', '/', '(', ')', ',' }, StringSplitOptions.RemoveEmptyEntries);
                    var j = item.SocialInterests.ToLower().Split(new char[] { '-', '/', '(', ')', ',' }, StringSplitOptions.RemoveEmptyEntries);
                    i = i.Select(s => s.Trim()).ToArray();
                    j = j.Select(s => s.Trim()).ToArray();
                    var CommonList = i.Length > k.Length ? i.Intersect(k) : k.Intersect(i);
                    var CommonList2 = j.Length > k.Length ? j.Intersect(k) : k.Intersect(j);
                    if (CommonList.Count() > 0 || CommonList2.Count() > 0)
                    {
                        luser.Add(item);
                    }
                }

                return luser.OrderByDescending(x => x.SimilarityScore);
            }
            return users.OrderByDescending(x => x.SimilarityScore).OrderBy(x => x.DistancefromLoggedUser);
        }
    }
}
