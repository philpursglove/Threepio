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
        public List<Uri> Species { get; set; }
        public List<Uri> Ships { get; set; }
        public List<Uri> Vehicles { get; set; }
        [JsonProperty("birth_year")]
        public string BirthYear { get; set; }

        public static Character Get(int id)
        {
            string data;
            using (WebClient client = WebClientFactory.GetClient())
            {
                data = client.DownloadString(string.Format("{0}/people/{1}/", Settings.RootUrl, id));
            }
            TextReader textreader = new StringReader(data);
            JsonReader reader = new JsonTextReader(textreader);
            return JsonSerializer.Create().Deserialize<Character>(reader);
        }

        public static List<Character> GetPage(int pageNumber = 1)
        {
            string data;
            StringReader stringreader;
            JsonReader jsonReader;
            BulkGet<Character> characters = new BulkGet<Character>();

            Uri nextPageUri = new Uri(string.Format("{0}/people/?page={1}", Settings.RootUrl, pageNumber));

            using (WebClient client = WebClientFactory.GetClient())
            {
                data = client.DownloadString(nextPageUri);

                stringreader = new StringReader(data);
                jsonReader = new JsonTextReader(stringreader);
                characters = JsonSerializer.Create().Deserialize<BulkGet<Character>>(jsonReader);
            }
            return characters.items;
        }
    }
}
