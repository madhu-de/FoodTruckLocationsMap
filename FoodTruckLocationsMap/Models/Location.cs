using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace FoodTruckLocationsMap.Models
{
    public class Location
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("coordinates")]
        public List<double> Coordinates { get; private set; }

        public Location()
        {
            Coordinates = new List<double>();
        }
    }
}
