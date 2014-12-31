using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

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

        public static Film Get(int id)
        {
            string data;
            using (WebClient client = WebClientFactory.GetClient())
            {
                data = client.DownloadString(string.Format("{0}/films/{1}/", Settings.RootUrl, id));
            }
            TextReader textreader = new StringReader(data);
            JsonReader reader = new JsonTextReader(textreader);
            Film film = JsonSerializer.Create().Deserialize<Film>(reader);

            foreach (Uri characterUri in film.CharacterUris)
            {
                film.Characters.Add(extractId(characterUri));
            }
            foreach (Uri planetUri in film.PlanetUris)
            {
                film.Planets.Add(extractId(planetUri));
            }
            foreach (Uri speciesUri in film.SpeciesUris)
            {
                film.Species.Add(extractId(speciesUri));
            }
            foreach (Uri starshipUri in film.StarshipUris)
            {
                film.Starships.Add(extractId(starshipUri));
            }
            foreach (Uri vehicleUri in film.VehicleUris)
            {
                film.Vehicles.Add(extractId(vehicleUri));
            }

            return film;
        }

        public static List<Film> GetPage(int pageNumber = 1)
        {
            Uri nextPageUri = new Uri(string.Format("{0}/films/?page={1}", Settings.RootUrl, pageNumber));

            BulkGet<Film> films = new BulkGet<Film>();
            string data;
            StringReader stringreader;
            JsonReader jsonReader;
            using (WebClient client = WebClientFactory.GetClient())
            {
                data = client.DownloadString(nextPageUri);

                stringreader = new StringReader(data);
                jsonReader = new JsonTextReader(stringreader);
                films = JsonSerializer.Create().Deserialize<BulkGet<Film>>(jsonReader);
            }
            return films.items;
        }

        // Convenience methods to find the individual films
        public static Film Episode1()
        {
            return Get(4);
        }

        public static Film Episode2()
        {
            return Get(5);
        }

        public static Film Episode3()
        {
            return Get(6);
        }

        public static Film Episode4()
        {
            return Get(1);
        }

        public static Film Episode5()
        {
            return Get(2);
        }

        public static Film Episode6()
        {
            return Get(3);
        }
    }
}
