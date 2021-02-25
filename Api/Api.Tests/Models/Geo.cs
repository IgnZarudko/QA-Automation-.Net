using System;
using Newtonsoft.Json;

namespace Api.Tests.Models
{
    public class Geo 
    {
        [JsonProperty("lat")]
        public string Lat { get; set; } 

        [JsonProperty("lng")]
        public string Lng { get; set; }

        protected bool Equals(Geo other)
        {
            return Lat == other.Lat && Lng == other.Lng;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Geo) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Lat, Lng);
        }
    }
}