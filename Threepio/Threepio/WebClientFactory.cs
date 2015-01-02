using System.Net;
using System.Net.Http;

namespace Threepio
{
    public static class WebClientFactory
    {
        public static HttpClient GetClient()
        {
            HttpClient client = new HttpClient(new HttpClientHandler{AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip});

            // Have to specify a User-Agent or the server returns http 500
            client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "Threepio .Net library");

            return client;
        }
    }
}
