using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FoodTruckLocationsMap.Models
{
    public class FoodTruckLocationViewModel
    {
        [JsonPropertyName("latitude")]
        public string Latitude { get; set; }

        [JsonPropertyName("longitude")]
        public string Longitude { get; set; }

        [JsonPropertyName("foodTruckLocations")]
        public IEnumerable<FoodTruckLocation> FoodTruckLocations { get; set; } 
    }

    public class FoodTruckLocation
    {
        [JsonPropertyName("applicant")]
        public string Applicant { get; set; }

        [JsonPropertyName("facilitytype")]
        public string FacilityType { get; set; }

        [JsonPropertyName("locationdescription")]
        public string LocationDescription { get; set; }

        [JsonPropertyName("address")]
        public string Address { get; set; }

        [JsonPropertyName("block")]
        public string Block { get; set; }

        [JsonPropertyName("lot")]
        public string Lot { get; set; }

        [JsonPropertyName("fooditems")]
        public string FoodItems { get; set; }

        [JsonPropertyName("latitude")]
        public string Latitude { get; set; }

        [JsonPropertyName("longitude")]
        public string Longitude { get; set; }

        [JsonPropertyName("schedule")]
        public string Schedule { get; set; }

        public double DistanceFromCurrentLocation { get; set; }
    }
}
