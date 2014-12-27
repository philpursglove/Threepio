using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace Threepio
{
    public class Planet : Item
    {
        public string Name { get; set; }
        public int Diameter { get; set; }
        public string Climate { get; set; }
        [JsonProperty("orbital_period")]
        public string OrbitalPeriod { get; set; }
        public string Population { get; set; }
        [JsonProperty("rotation_period")]
        public string RotationPeriod { get; set; }
        [JsonProperty("surface_water")]
        public string SurfaceWaterPercentage { get; set; }
        public string Terrain { get; set; }
        public List<Uri> Films { get; set; }
        public List<Uri> Residents { get; set; }

        public static Planet Get(int id)
        {
            string data;
            using (WebClient client = WebClientFactory.GetClient())
            {
                data = client.DownloadString(string.Format("{0}/planets/{1}/", Settings.RootUrl, id));
            }
            TextReader textreader = new StringReader(data);
            JsonReader reader = new JsonTextReader(textreader);
            return JsonSerializer.Create().Deserialize<Planet>(reader);
        }

        public static List<Planet> GetPage(int pageNumber = 1)
        {
            string data;
            using (WebClient client = WebClientFactory.GetClient())
            {
                data = client.DownloadString(string.Format("{0}/planets/?page={1}", Settings.RootUrl, pageNumber));
            }
            StringReader stringreader = new StringReader(data);
            JsonReader jsonReader = new JsonTextReader(stringreader);
            List<Planet> planets = JsonSerializer.Create().Deserialize<BulkGet<Planet>>(jsonReader).items;

            return planets;
        }
    }
}
