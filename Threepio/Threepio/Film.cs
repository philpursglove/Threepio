using System;
using System.IO;
using System.Net;
using Newtonsoft.Json;

namespace Threepio
{
    public class Film
    {
        public DateTime Created { get; set; }
        public DateTime Edited { get; set; }
        public string Title { get; set; }
        public string Director { get; set; }
        public string Producer { get; set; }
        public string Opening_Crawl { get; set; }

        public static Film Get(int id)
        {
            //return new Film();
            WebClient client = new WebClient();
            String data = client.DownloadString(string.Format("http://swapi.co/api/films/{0}/", id));
            TextReader textreader = new StringReader(data);
            JsonReader reader = new JsonTextReader(textreader);
            return JsonSerializer.Create().Deserialize<Film>(reader);
        }
    }
}
