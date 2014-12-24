using System.Net;

namespace Threepio
{
    public static class WebClientFactory
    {
        public static WebClient GetClient()
        {
            WebClient client = new WebClient();

            // Have to specify a User-Agent or the server returns http 500
            client.Headers.Add(HttpRequestHeader.UserAgent, "Threepio .Net library");

            return client;
        }
    }
}
