﻿using Newtonsoft.Json;
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
        [JsonProperty("characters")]
        public List<Uri> Characters { get; private set; }
        [JsonProperty("planets")]
        public List<Uri> Planets { get; private set; }
        [JsonProperty("species")]
        public List<Uri> Species { get; private set; }
        [JsonProperty("starships")]
        public List<Uri> Starships { get; private set; }
        [JsonProperty("vehicles")]
        public List<Uri> Vehicles { get; private set; }
        [JsonProperty("episode_id")]
        public int Episode { get; set; }

        public static Film Get(int id)
        {
            string data;
            using (WebClient client = WebClientFactory.GetClient())
            {
                data = client.DownloadString(string.Format("{0}/films/{1}/", Settings.RootUrl, id));
            }
            TextReader textreader = new StringReader(data);
            JsonReader reader = new JsonTextReader(textreader);
            return JsonSerializer.Create().Deserialize<Film>(reader);
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
