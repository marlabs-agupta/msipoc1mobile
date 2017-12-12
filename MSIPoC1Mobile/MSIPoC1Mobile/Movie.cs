using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace MSIPoC1Mobile
{
    public class Movie
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "genre")]
        public string Genre { get; set; }

        [JsonProperty(PropertyName = "created")]
        public DateTime Created { get; }
    }
}