using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
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
        public int Cost { get; set; }
        public float Length { get; set; }
        public int Crew { get; set; }
        public int Passengers { get; set; }
        [JsonProperty("hyperdrive_rating")]
        public string HyperdriveRating { get; set; }
        [JsonProperty("cargo_capacity")]
        public int CargoCapacity { get; set; }

        public List<Uri> Films { get; set; }
        public List<Uri> Pilots { get; set; }

        public static Starship Get(int id)
        {
            try
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
            catch (Exception)
            {
                return null;
            }
        }
    }
}
