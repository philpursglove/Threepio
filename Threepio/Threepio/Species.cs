using System.Net.Http;
using System.Threading.Tasks;
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
        [JsonIgnore]
        public int Homeworld { get; set; }
        [JsonProperty("Homeworld")]
        public Uri HomeworldUri { get; set; }
        public List<int> Films { get; set; }
        public List<int> Members { get; set; }
        [JsonProperty("average_height")]
        public string AverageHeight { get; set; }
        [JsonProperty("average_lifespan")]
        public string AverageLifespan { get; set; }
        [JsonProperty("eye_colors")]
        public string EyeColours { get; set; }
        [JsonProperty("skin_colors")]
        public string SkinColours { get; set; }

        public Species()
        {
            Films = new List<int>();
            Members = new List<int>();
        }

        public static async Task<Species> Get(int id)
        {
            string data;
            using (HttpClient client = WebClientFactory.GetClient())
            {
                data = await client.GetStringAsync(string.Format("{0}/species/{1}/", Settings.RootUrl, id));
            }
            TextReader textreader = new StringReader(data);
            JsonReader reader = new JsonTextReader(textreader);
            Species species = JsonSerializer.Create().Deserialize<Species>(reader);

            species.Homeworld = extractId(species.HomeworldUri);

            species.extractIds();

            return species;
        }

        private void extractIds()
        {
            foreach (Uri filmUri in FilmUris)
            {
                Films.Add(extractId(filmUri));
            }
            foreach (Uri memberUri in MemberUris)
            {
                Members.Add(extractId(memberUri));
            }
        }

        public static async Task<List<Species>> GetPage(int pageNumber = 1)
        {
            string data;
            using (HttpClient client = WebClientFactory.GetClient())
            {
                data = await client.GetStringAsync(string.Format("{0}/species/?page={1}", Settings.RootUrl, pageNumber));
            }
            StringReader stringreader = new StringReader(data);
            JsonReader jsonReader = new JsonTextReader(stringreader);
            List<Species> species = JsonSerializer.Create().Deserialize<BulkGet<Species>>(jsonReader).items;

            foreach (Species specie in species)
            {
                specie.extractIds();
            }
            return species;
        }
    }
}
