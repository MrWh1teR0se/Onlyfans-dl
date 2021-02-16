using System.IO;
using OnlyFansDL.Requests;
using RestSharp;

namespace OnlyFansDL.Core
{
    public class Requester
    {
        public RestClient Client { get; set; }

        public Requester(string baseUrl)
        {
            Client = new RestClient(baseUrl);
            Client.UserAgent = "Mozilla/5.0 (Macintosh; Intel Mac OS X 11_2_1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/88.0.4324.150 Safari/537.36";
        }

        public dynamic SendRequest<T>(T request) where T : IRestRequest
        {
            return Client.Get<dynamic>(request).Data;
        }

        public void DownloadContent<T>(T request, string filePath, string fileName) where T : IRestRequest
        {
            
            var tempFile = filePath + $"/{fileName}";
            using var writer = File.OpenWrite(tempFile);
            
            request.ResponseWriter = responseStream =>
            {
                using (responseStream)
                {
                    responseStream.CopyTo(writer);
                }
            };
            
            
            Client.DownloadData(request);
        }
        
    }
}