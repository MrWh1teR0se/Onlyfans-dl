using System.Collections.Generic;
using RestSharp;

namespace OnlyFansDL.Requests
{
    public class ProfileRequest : BaseRequest
    {
        public ProfileRequest(string uri, string authToken) : base(uri, authToken)
        {
            AuthToken = authToken;
            Request = new RestRequest(uri);
        }


        public RestRequest Create()
        {
            SetHeaders();

            return Request;
        }

        private void SetHeaders()
        {
            Request.AddHeaders(new List<KeyValuePair<string, string>>()
            {
                new("Accept", " application/json, text/plain, */*"),
                new("Accept-Encoding", " gzip, deflate"),
                new("access-token", AuthToken),
            });
        }
    }
}