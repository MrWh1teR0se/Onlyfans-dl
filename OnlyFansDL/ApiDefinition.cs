
namespace OnlyFansDL
{
    public class ApiDefinition
    {
        public string Endpoint { get; set; } = "https://onlyfans.com/api2/v2/";
        public string AppToken { get; set; } = "33d57ade8c02dbc5a333db99ff9ae26a";
        public string UserEndpoint { get; set; } = "users/";
        public string ProfileEndpoint { get; set; } = "me";

        public override string ToString()
        {
            return $"{Endpoint}";
        }

        
        public string GetPhotosUrl(string userId)
        {
            return $"{UserEndpoint}{userId}/posts/photos?app-token={AppToken}";
        }

        public string GetVideosUrl(string userId)
        {
            return $"{UserEndpoint}{userId}/posts/videos?app-token={AppToken}";
        }

        public string GetUserProfileUrl()
        {
            return $"{UserEndpoint}{ProfileEndpoint}?app-token={AppToken}";;
        }

        public string GetSpecificUserUrl(string userName)
        {
            return $"{UserEndpoint}{userName}?app-token={AppToken}";
        }
    }
}