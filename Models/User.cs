namespace ARCollabator.Models
{
    public class User
    {
        public int UserId { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }
        public string PersonalEmail { get; set; }

        public string MobileNumber { get; set; }

        public string City { get; set; }

        public string SocialInterests { get; set; }
        public string TechnicalInterests { get; set; }
        public string Address { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public double DistancefromLoggedUser { get; set; }
        public double SimilarityScore { get; set; }

        

    }
}
