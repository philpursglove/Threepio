using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace Threepio
{
    public class Species : Item
    {
        public string Name { get; set; }
        public string Classification { get; set; }
        public string Designation { get; set; }
        public string Language { get; set; }
        public Uri Homeworld { get; set; }
        public List<int> Films { get; set; }
        public List<int> Members { get; set; }
        [JsonProperty("films")]
        internal List<Uri> FilmUris { get; set; }
        [JsonProperty("people")]
        internal List<Uri> MemberUris { get; set; }

        public static Species Get(int id)
        {
            string data;
            using (WebClient client = WebClientFactory.GetClient())
            {
                data = client.DownloadString(string.Format("{0}/species/{1}/", Settings.RootUrl, id));
            }
            TextReader textreader = new StringReader(data);
            JsonReader reader = new JsonTextReader(textreader);
            return JsonSerializer.Create().Deserialize<Species>(reader);
        }

        public static List<Species> GetPage(int pageNumber = 1)
        {
            string data;
            using (WebClient client = WebClientFactory.GetClient())
            {
                data = client.DownloadString(string.Format("{0}/species/?page={1}", Settings.RootUrl, pageNumber));
            }
            StringReader stringreader = new StringReader(data);
            JsonReader jsonReader = new JsonTextReader(stringreader);
            List<Species> species = JsonSerializer.Create().Deserialize<BulkGet<Species>>(jsonReader).items;

            return species;
        }
    }
}
