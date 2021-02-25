using System;

namespace Api.Tests.Models
{
    public class Address {
        public string street { get; set; } 
        public string suite { get; set; } 
        public string city { get; set; } 
        public string zipcode { get; set; } 
        public Geo geo { get; set; }

        protected bool Equals(Address other)
        {
            return street == other.street && suite == other.suite && city == other.city && zipcode == other.zipcode && Equals(geo, other.geo);
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
            return HashCode.Combine(street, suite, city, zipcode, geo);
        }
    }
}