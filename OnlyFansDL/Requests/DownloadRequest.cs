using System;
using System.IO;
using System.Text;
using RestSharp;

namespace OnlyFansDL.Requests
{
    public class DownloadRequest : BaseRequest
    {
        public DownloadRequest(string authToken,string uri = "" ) : base(uri, authToken)
        {
            AuthToken = authToken;
            
            Request = new RestRequest();
        }

        public IRestRequest Create()
        {
            return Request;
        }
        
        
    }
}