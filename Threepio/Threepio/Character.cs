using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
                using (WebClient client = WebClientFactory.GetClient())
                {
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

        public static List<Character> GetAll(int pageSize = 20, int pageNumber = 1)
        {
            string data;
            using (WebClient client = WebClientFactory.GetClient())
            {
                data = client.DownloadString(string.Format("{0}/people/", Settings.RootUrl));
            }
            StringReader stringreader = new StringReader(data);
            JsonReader jsonReader = new JsonTextReader(stringreader);
            List<Character> characters = JsonSerializer.Create().Deserialize<BulkGet<Character>>(jsonReader).items;

            // Paging algorthim
            int startRecord = ((pageNumber - 1) * pageSize) + 1;

            return characters.Skip(startRecord).Take(pageSize).ToList();
        }
    }
}
