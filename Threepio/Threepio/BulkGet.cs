using Newtonsoft.Json;
using System.Collections.Generic;

namespace Threepio
{
    internal class BulkGet<T>
    {
        [JsonProperty("results")]
        public List<T> items { get; set; }
    }
}
