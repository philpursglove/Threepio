using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace Threepio
{
    public class Film : Item
    {
        public string Title { get; set; }
        public string Director { get; set; }
        public string Producer { get; set; }
        public string Opening_Crawl { get; set; }
        [JsonProperty("characters")]
        public List<Uri> Characters { get; private set; }
        [JsonProperty("planets")]
        public List<Uri> Planets { get; private set; }
        [JsonProperty("species")]
        public List<Uri> Species { get; private set; }
        [JsonProperty("starships")]
        public List<Uri> Starships { get; private set; }
        [JsonProperty("vehicles")]
        public List<Uri> Vehicles { get; private set; }

        public static Film Get(int id)
        {
            try
            {
                string data;
                using (WebClient client = new WebClient())
                {
                    data = client.DownloadString(string.Format("{0}/films/{1}/", Settings.RootUrl, id));
                }
                TextReader textreader = new StringReader(data);
                JsonReader reader = new JsonTextReader(textreader);
                return JsonSerializer.Create().Deserialize<Film>(reader);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
