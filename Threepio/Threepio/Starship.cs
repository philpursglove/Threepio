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
        public string Cost { get; set; }
        public float Length { get; set; }
        public string Crew { get; set; }
        [JsonProperty("passengers")]
        public string PassengerCapacity { get; set; }
        [JsonProperty("hyperdrive_rating")]
        public string HyperdriveRating { get; set; }
        [JsonProperty("cargo_capacity")]
        public string CargoCapacity { get; set; }
        public List<int> Films { get; set; }
        public List<int> Pilots { get; set; }
        public string Consumables { get; set; }
        public string Manufacturer { get; set; }

        public Starship()
        {
            Films = new List<int>();
            Pilots = new List<int>();
        }
        public static Starship Get(int id)
        {
            string data;
            using (WebClient client = WebClientFactory.GetClient())
            {
                data = client.DownloadString(string.Format("{0}/starships/{1}/", Settings.RootUrl, id));
            }
            TextReader textreader = new StringReader(data);
            JsonReader reader = new JsonTextReader(textreader);
            Starship ship = JsonSerializer.Create().Deserialize<Starship>(reader);

            ship.extractIds();

            return ship;
        }

        private void extractIds()
        {
            foreach (Uri filmUri in FilmUris)
            {
                Films.Add(extractId(filmUri));
            }
            foreach (Uri pilotUri in PilotUris)
            {
                Pilots.Add(extractId(pilotUri));
            }
        }

        public static List<Starship> GetPage(int pageNumber = 1)
        {
            string data;
            using (WebClient client = WebClientFactory.GetClient())
            {
                data = client.DownloadString(string.Format("{0}/starships/?page={1}", Settings.RootUrl, pageNumber));
            }
            StringReader stringreader = new StringReader(data);
            JsonReader jsonReader = new JsonTextReader(stringreader);
            List<Starship> starships = JsonSerializer.Create().Deserialize<BulkGet<Starship>>(jsonReader).items;

            foreach (Starship starship in starships)
            {
                starship.extractIds();
            }
            return starships;
        }
    }
}
