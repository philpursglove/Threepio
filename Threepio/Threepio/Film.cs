using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Threepio
{
    public class Film : Item
    {
        public string Title { get; set; }
        public string Director { get; set; }
        public string Producer { get; set; }
        [JsonProperty("Opening_Crawl")]
        public string Crawl { get; set; }
        public List<int> Characters { get; private set; }
        public List<int> Planets { get; set; }

        public List<int> Species { get; set; }

        public List<int> Starships { get; set; }

        public List<int> Vehicles { get; set; }
        [JsonProperty("episode_id")]
        public int Episode { get; set; }

        public Film()
        {
            Characters = new List<int>();
            Planets = new List<int>();
            Species = new List<int>();
            Starships = new List<int>();
            Vehicles = new List<int>();
        }

        public static async Task<Film> Get(int id)
        {
            string data;
            using (HttpClient client = WebClientFactory.GetClient())
            {
                data = await client.GetStringAsync(string.Format("{0}/films/{1}/", Settings.RootUrl, id));
            }
            TextReader textreader = new StringReader(data);
            JsonReader reader = new JsonTextReader(textreader);
            Film film = JsonSerializer.Create().Deserialize<Film>(reader);

            film.ExtractIds();

            return film;
        }

        private void ExtractIds()
        {
            foreach (Uri characterUri in CharacterUris)
            {
                Characters.Add(extractId(characterUri));
            }
            foreach (Uri planetUri in PlanetUris)
            {
                Planets.Add(extractId(planetUri));
            }
            foreach (Uri speciesUri in SpeciesUris)
            {
                Species.Add(extractId(speciesUri));
            }
            foreach (Uri starshipUri in StarshipUris)
            {
                Starships.Add(extractId(starshipUri));
            }
            foreach (Uri vehicleUri in VehicleUris)
            {
                Vehicles.Add(extractId(vehicleUri));
            }
        }

        public static async Task<List<Film>> GetPage(int pageNumber = 1)
        {
            Uri nextPageUri = new Uri(string.Format("{0}/films/?page={1}", Settings.RootUrl, pageNumber));

            BulkGet<Film> films = new BulkGet<Film>();
            string data;
            StringReader stringreader;
            JsonReader jsonReader;
            using (HttpClient client = WebClientFactory.GetClient())
            {
                data = await client.GetStringAsync(nextPageUri);

                stringreader = new StringReader(data);
                jsonReader = new JsonTextReader(stringreader);
                films = JsonSerializer.Create().Deserialize<BulkGet<Film>>(jsonReader);
            }
            foreach (Film film in films.items)
            {
                film.ExtractIds();
            }

            return films.items;
        }

        // Convenience methods to find the individual films
        public static Task<Film> Episode1()
        {
            return Get(4);
        }

        public static Task<Film> Episode2()
        {
            return Get(5);
        }

        public static Task<Film> Episode3()
        {
            return Get(6);
        }

        public static Task<Film> Episode4()
        {
            return Get(1);
        }

        public static Task<Film> Episode5()
        {
            return Get(2);
        }

        public static Task<Film> Episode6()
        {
            return Get(3);
        }
    }
}
