using System;
using Newtonsoft.Json;

namespace Api.Tests.Models
{
    public class Address
    {
        
        [JsonProperty("street")]
        public string Street { get; set; } 

        [JsonProperty("suite")]
        public string Suite { get; set; } 

        [JsonProperty("city")]
        public string City { get; set; } 

        [JsonProperty("zipcode")]
        public string Zipcode { get; set; } 

        [JsonProperty("geo")]
        public Geo Geo { get; set; }

        protected bool Equals(Address other)
        {
            return Street == other.Street && Suite == other.Suite && City == other.City && Zipcode == other.Zipcode && Equals(Geo, other.Geo);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Address) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Street, Suite, City, Zipcode, Geo);
        }
    }
}