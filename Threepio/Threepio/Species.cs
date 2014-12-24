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
        public List<Uri> Films { get; set; }
        [JsonProperty("people")]
        public List<Uri> Members { get; set; }

        public static Species Get(int id)
        {
            try
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
            catch (Exception)
            {
                return null;
            }

        }
    }
}
