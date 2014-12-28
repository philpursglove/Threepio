using System;

namespace Threepio
{
    public abstract class Item
    {
        public DateTime Created { get; set; }
        public DateTime Edited { get; set; }

        internal static int ParseLink(Uri uri)
        {
            string link = uri.AbsoluteUri;

            link = link.Replace(Settings.RootUrl, string.Empty);
            link = link.Replace("/", string.Empty);
            link = link.Replace("films", string.Empty);
            link = link.Replace("people", string.Empty);
            link = link.Replace("species", string.Empty);
            link = link.Replace("planets", string.Empty);
            link = link.Replace("starships", string.Empty);
            link = link.Replace("vehicles", string.Empty);

            int result;
            int.TryParse(link, out result);

            return result;
        }
    }
}
