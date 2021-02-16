using RestSharp;

namespace OnlyFansDL.Requests
{
    public class VideosRequest : BaseRequest
    {

        public int Limit { get; set; }
        
        public VideosRequest(string uri, string authToken) : base(uri, authToken)
        {
            AuthToken = authToken;
            Request = new RestRequest(uri);
        }
        
        public RestRequest Create(int limit)
        {
            this.Limit = limit;
            SetHeaders();
            SetQueryParams();
            return Request;
        }

        private void SetQueryParams()
        {
            //limit=6&order=publish_date_desc&skip_users=all&skip_users_dups=1&only_can_view=1&pinned_sort=0
            Request.AddQueryParameter("limit", Limit.ToString());
            Request.AddQueryParameter("order", "publish_date_desc");
            Request.AddQueryParameter("skip_users", "all");
            Request.AddQueryParameter("skip_users_dups", "1");
            Request.AddQueryParameter("only_can_view", "1");
            Request.AddQueryParameter("pinned_sort", "0");
        }
    }
}