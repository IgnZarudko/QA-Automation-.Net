using System;

namespace Api.Tests.Models
{
    public class Geo {
        public string lat { get; set; } 
        public string lng { get; set; }

        protected bool Equals(Geo other)
        {
            return lat == other.lat && lng == other.lng;
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
            return HashCode.Combine(lat, lng);
        }
    }
}