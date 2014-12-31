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
        [JsonProperty("birth_year")]
        public string BirthYear { get; set; }

        public Character()
        {
            Films = new List<int>();
            Species = new List<int>();
            Ships = new List<int>();
            Vehicles = new List<int>();
        }
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
                character.Films.Add(extractId(filmUri));
            }
            foreach (Uri speciesUri in character.SpeciesUris)
            {
                character.Species.Add(extractId(speciesUri));
            }
            foreach (Uri shipUri in character.StarshipUris)
            {
                character.Ships.Add(extractId(shipUri));
            }
            foreach (Uri vehicleUri in character.VehicleUris)
            {
                character.Vehicles.Add(extractId(vehicleUri));
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
