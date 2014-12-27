using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace Threepio
{
    public class Vehicle : Item
    {
        public string Name { get; set; }
        public string Model { get; set; }
        [JsonProperty("vehicle_class")]
        public string Class { get; set; }
        [JsonProperty("cost_in_credits")]
        public string Cost { get; set; }
        public float Length { get; set; }
        public int Crew { get; set; }
        public int Passengers { get; set; }
        [JsonProperty("cargo_capacity")]
        public string CargoCapacity { get; set; }

        public List<Uri> Films { get; set; }
        public List<Uri> Pilots { get; set; }

        public static Vehicle Get(int id)
        {
            string data;
            using (WebClient client = WebClientFactory.GetClient())
            {
                data = client.DownloadString(string.Format("{0}/vehicles/{1}/", Settings.RootUrl, id));
            }
            TextReader textreader = new StringReader(data);
            JsonReader reader = new JsonTextReader(textreader);
            return JsonSerializer.Create().Deserialize<Vehicle>(reader);
        }

        public static List<Vehicle> GetPage(int pageNumber = 1)
        {
            string data;
            using (WebClient client = WebClientFactory.GetClient())
            {
                data = client.DownloadString(string.Format("{0}/vehicles/?page={1}", Settings.RootUrl, pageNumber));
            }
            StringReader stringreader = new StringReader(data);
            JsonReader jsonReader = new JsonTextReader(stringreader);
            List<Vehicle> vehicles = JsonSerializer.Create().Deserialize<BulkGet<Vehicle>>(jsonReader).items;

            return vehicles;
        }
    }
}
