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
        [JsonProperty("Opening_Crawl")]
        public string Crawl { get; set; }
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
            string data;
            using (WebClient client = WebClientFactory.GetClient())
            {
                data = client.DownloadString(string.Format("{0}/films/{1}/", Settings.RootUrl, id));
            }
            TextReader textreader = new StringReader(data);
            JsonReader reader = new JsonTextReader(textreader);
            return JsonSerializer.Create().Deserialize<Film>(reader);
        }

        public static List<Film> GetAll(int pageSize = 6, int pageNumber = 1)
        {
            string data;
            using (WebClient client = WebClientFactory.GetClient())
            {
                data = client.DownloadString(string.Format("{0}/films/", Settings.RootUrl));
            }

            // Paging algorthim
            int startRecord = ((pageNumber - 1) * pageSize) + 1;
            int endRecord = pageNumber * pageSize;

            return new List<Film>();
        }
    }
}
