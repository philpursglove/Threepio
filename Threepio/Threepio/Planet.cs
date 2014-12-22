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
        public int OrbitalPeriod { get; set; }
        public int Population { get; set; }
        [JsonProperty("rotation_period")]
        public int RotationPeriod { get; set; }
        [JsonProperty("surface_water")]
        public int SurfaceWaterPercentage { get; set; }
        public string Terrain { get; set; }
        public List<Uri> Films { get; set; }
        public List<Uri> Residents { get; set; }

        public static Planet Get(int id)
        {
            try
            {
                string data;
                using (WebClient client = new WebClient())
                {
                    data = client.DownloadString(string.Format("{0}/planets/{1}/", Settings.RootUrl, id));
                }
                TextReader textreader = new StringReader(data);
                JsonReader reader = new JsonTextReader(textreader);
                return JsonSerializer.Create().Deserialize<Planet>(reader);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
