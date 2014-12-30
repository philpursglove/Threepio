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
        public List<int> Films { get; set; }
        public List<int> Species { get; set; }
        public List<int> Ships { get; set; }
        public List<int> Vehicles { get; set; }
        [JsonProperty("films")]
        internal List<Uri> FilmUris { get; set; }
        [JsonProperty("species")]
        internal List<Uri> SpeciesUris { get; set; }
        [JsonProperty("starships")]
        internal List<Uri> ShipUris { get; set; }
        [JsonProperty("vehicles")]
        internal List<Uri> VehicleUris { get; set; }
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
            Character character = JsonSerializer.Create().Deserialize<Character>(reader);

            foreach (Uri filmUri in character.FilmUris)
            {
                character.Films.Add(ParseLink(filmUri));
            }
            foreach (Uri speciesUri in character.SpeciesUris)
            {
                character.Species.Add(ParseLink(speciesUri));
            }
            foreach (Uri shipUri in character.ShipUris)
            {
                character.Ships.Add(ParseLink(shipUri));
            }
            foreach (Uri vehicleUri in character.VehicleUris)
            {
                character.Vehicles.Add(ParseLink(vehicleUri));
            }
            return character;
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
