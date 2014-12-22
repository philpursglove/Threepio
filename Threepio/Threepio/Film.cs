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
        public string Opening_Crawl { get; set; }
        public IEnumerable<Uri> Characters { get; private set; }

        public static Film Get(int id)
        {
            try
            {
                WebClient client = new WebClient();
                String data = client.DownloadString(string.Format("{0}/films/{1}/", Settings.RootUrl, id));
                TextReader textreader = new StringReader(data);
                JsonReader reader = new JsonTextReader(textreader);
                return JsonSerializer.Create().Deserialize<Film>(reader);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
