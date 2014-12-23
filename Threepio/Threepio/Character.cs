using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace Threepio
{
    public class Character : Item
    {
        public string Name { get; set; }
        public Uri Homeworld { get; set; }
        public List<Uri> Films { get; set; }
        public Uri Species { get; set; }
        public List<Uri> Ships { get; set; }
        public List<Uri> Vehicles { get; set; }
        [JsonProperty("birth_year")]
        public string BirthYear { get; set; }

        public static Character Get(int id)
        {
            try
            {
                string data;
                using (WebClient client = new WebClient())
                {
                    client.Headers.Add(HttpRequestHeader.UserAgent, "Threepio .Net library");
                    data = client.DownloadString(string.Format("{0}/people/{1}/", Settings.RootUrl, id));
                }
                TextReader textreader = new StringReader(data);
                JsonReader reader = new JsonTextReader(textreader);
                return JsonSerializer.Create().Deserialize<Character>(reader);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
