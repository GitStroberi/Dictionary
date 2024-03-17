using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Dictionary.Models
{
    public class WordModel
    {
        [JsonPropertyName("category")]
        public string Category { get; set; }
        [JsonPropertyName("word")]
        public string Word { get; set; }
        [JsonPropertyName("definition")]
        public string Definition { get; set; }
        [JsonPropertyName("imageurl")]
        public string ImageUrl { get; set; }
    }
}
