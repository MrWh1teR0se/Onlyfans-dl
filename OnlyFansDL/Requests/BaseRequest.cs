using System.Collections.Generic;
using RestSharp;

namespace OnlyFansDL.Requests
{
    public class BaseRequest
    {
        public string AuthToken { get; set; }
        public RestRequest Request { get; set; }

        public BaseRequest(string uri, string authToken)
        {
            AuthToken = authToken;
            Request = new RestRequest(uri);
        }
        
        public void SetHeaders()
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