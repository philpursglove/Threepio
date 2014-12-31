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
        public string Length { get; set; }
        public int Crew { get; set; }
        [JsonProperty("passengers")]
        public string PassengerCapacity { get; set; }
        [JsonProperty("cargo_capacity")]
        public string CargoCapacity { get; set; }
        public List<int> Films { get; set; }
        public List<int> Pilots { get; set; }

        public Vehicle()
        {
            Films = new List<int>();
            Pilots = new List<int>();
        }

        public static Vehicle Get(int id)
        {
            string data;
            using (WebClient client = WebClientFactory.GetClient())
            {
                data = client.DownloadString(string.Format("{0}/vehicles/{1}/", Settings.RootUrl, id));
            }
            TextReader textreader = new StringReader(data);
            JsonReader reader = new JsonTextReader(textreader);
            Vehicle vehicle = JsonSerializer.Create().Deserialize<Vehicle>(reader);

            vehicle.extractIds();

            return vehicle;
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

            foreach (Vehicle vehicle in vehicles)
            {
                vehicle.extractIds();
            }
            return vehicles;
        }
    }
}
