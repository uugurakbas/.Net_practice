using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace IBB.Models
{
    public class IsparkModel
    {
        [JsonPropertyName("_id")]
        public int Id { get; set; }

        [JsonPropertyName("PARK_NAME")]
        public string ParkName { get; set; }

        [JsonPropertyName("LATITUDE")]
        public double Latitude { get; set; }

        [JsonPropertyName("LONGITUDE")]
        public double Longitude { get; set; }

        [JsonPropertyName("CAPACITY")]
        public int Capacity { get; set; }

        [JsonPropertyName("EMPTY_CAPACITY")]
        public int EmptyCapacity { get; set; }
    }

    public class IsparkResponse
    {
        [JsonPropertyName("success")]
        public bool Success { get; set; }

        [JsonPropertyName("result")]
        public IsparkResult Result { get; set; }
    }

    public class IsparkResult
    {
        [JsonPropertyName("records")]
        public List<IsparkModel> Records { get; set; }
    }
} 