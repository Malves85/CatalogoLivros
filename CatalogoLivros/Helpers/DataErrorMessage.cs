using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;

namespace CatalogoLivros.Helpers
{
    public class DataErrorMessage
    {
        [JsonProperty(PropertyName = "status", NullValueHandling = NullValueHandling.Ignore)]
        public string Status { get; set; }

        [JsonProperty(PropertyName = "resource_id", NullValueHandling = NullValueHandling.Ignore)]
        public int Resource_id { get; set; }
    }
}
