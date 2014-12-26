using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;

namespace Threepio
{
    public class Starship : Item
    {
        public string Name { get; set; }
        public string Model { get; set; }
        [JsonProperty("starship_class")]
        public string Class { get; set; }
        [JsonProperty("cost_in_credits")]
        public string Cost { get; set; }
        public float Length { get; set; }
        public int Crew { get; set; }
        public int Passengers { get; set; }
        [JsonProperty("hyperdrive_rating")]
        public string HyperdriveRating { get; set; }
        [JsonProperty("cargo_capacity")]
        public string CargoCapacity { get; set; }

        public List<Uri> Films { get; set; }
        public List<Uri> Pilots { get; set; }

        public static Starship Get(int id)
        {
            string data;
            using (WebClient client = WebClientFactory.GetClient())
            {
                data = client.DownloadString(string.Format("{0}/starships/{1}/", Settings.RootUrl, id));
            }
            TextReader textreader = new StringReader(data);
            JsonReader reader = new JsonTextReader(textreader);
            return JsonSerializer.Create().Deserialize<Starship>(reader);
        }

        public static List<Starship> GetAll(int pageSize = 20, int pageNumber = 1)
        {
            string data;
            using (WebClient client = WebClientFactory.GetClient())
            {
                data = client.DownloadString(string.Format("{0}/starships/", Settings.RootUrl));
            }
            StringReader stringreader = new StringReader(data);
            JsonReader jsonReader = new JsonTextReader(stringreader);
            List<Starship> starships = JsonSerializer.Create().Deserialize<BulkGet<Starship>>(jsonReader).items;

            // Paging algorthim
            int startRecord = ((pageNumber - 1) * pageSize) + 1;

            return starships.Skip(startRecord).Take(pageSize).ToList();
        }
    }
}
